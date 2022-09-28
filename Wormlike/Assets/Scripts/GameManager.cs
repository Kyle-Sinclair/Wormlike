using System.Collections;
using System.Collections.Generic;
using Projectiles;
using Projectiles.ProjectileBehaviours;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<Projectile> projectiles;
    public ProjectileFactory projectileFactory;

    public KeyCode createKey = KeyCode.C;
    public KeyCode destroyKey = KeyCode.X;


    public void Awake() {
        projectiles = new List<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(createKey)) {
            CreateShape();
        }
        else if (Input.GetKeyDown(destroyKey)) {
            DestroyShape();
        }
    }

    void CreateShape() {
        Projectile instance = projectileFactory.GetRandom();
        var movement = instance.AddProjectileBehavior<LinearMovementProjectileBehaviour>();
        //movement.Velocity = Random.insideUnitCircle * Random.Range(1f, 5f);

        Transform t = instance.transform;
        t.localPosition = Random.insideUnitSphere * 5f;
        t.localRotation = Random.rotation;
        t.localScale = Vector3.one * Random.Range(0.1f, 1f);
        projectiles.Add(instance);
    }

    void DestroyShape() {
        if (projectiles.Count > 0) {
            int index = Random.Range(0, projectiles.Count);
            projectiles[index].Recycle();
            int lastIndex = projectiles.Count - 1;
            projectiles[index] = projectiles[lastIndex];
            projectiles.RemoveAt(lastIndex);
        }
    }
}
