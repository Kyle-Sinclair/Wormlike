using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ProjectileFactory : ScriptableObject
{


    [SerializeField]
    Projectile[] prefabs;
    List<Projectile>[] pools;
    [SerializeField]
    bool recycle;

    public Projectile Get(int projectileId) {

        Projectile instance;
        if (recycle) {
            if (pools == null) {
                CreatePools();
            }
            List<Projectile> pool = pools[projectileId];
            int lastIndex = pool.Count - 1;
            if (lastIndex >= 0) {
                instance = pool[lastIndex];
                instance.gameObject.SetActive(true);
                pool.RemoveAt(lastIndex);
            }
            else {
                instance = Instantiate(prefabs[projectileId]);
                instance.OriginFactory = this;

                instance.ProjectileId = projectileId;
            }
        }
        else {
            instance = Instantiate(prefabs[projectileId]);
            instance.ProjectileId = projectileId;
        }
       
        return instance;
    }

    public Projectile GetRandom() {
        return Get(Random.Range(0, prefabs.Length));
    }


    void CreatePools() {
        pools = new List<Projectile>[prefabs.Length];
        for (int i = 0; i < pools.Length; i++) {
            pools[i] = new List<Projectile>();
        }

    }

    public void Reclaim(Projectile projectileToRecycle) {
        if (projectileToRecycle.OriginFactory != this) {
            Debug.LogError("Tried to reclaim shape with wrong factory.");
            return;
        }
        if (recycle) {
            if (pools == null) {
                CreatePools();
            }
            pools[projectileToRecycle.ProjectileId].Add(projectileToRecycle);
            projectileToRecycle.gameObject.SetActive(false);
        }
        else {
            Destroy(projectileToRecycle.gameObject);
        }
    }
}

