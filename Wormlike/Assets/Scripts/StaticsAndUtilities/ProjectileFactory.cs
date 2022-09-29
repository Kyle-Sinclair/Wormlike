using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Projectiles
{
    [CreateAssetMenu]
    public class ProjectileFactory : ScriptableObject
    {


        [SerializeField]
        Projectile[] prefabs;
        List<Projectile>[] pools;
        [SerializeField]

        public Projectile Get(int projectileId) {

            Projectile instance;
                if (pools == null) {
                    CreatePools();
                }
                Debug.Log(projectileId);
                
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
                instance.Reset();
                instance.rb.velocity = Vector3.zero;
                return instance;
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
            if (pools == null) {
                CreatePools();
            }
            pools[projectileToRecycle.ProjectileId].Add(projectileToRecycle);
            //Debug.Log("Adding projectile with id " + projectileToRecycle.ProjectileId + " to pools");
            projectileToRecycle.gameObject.SetActive(false);
        }
    }
}

