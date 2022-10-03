using System.Collections;
using System.Collections.Generic;
using Projectiles;
using Projectiles.ProjectileBehaviours;
using UnityEngine;

namespace StaticsAndUtilities
{
    public enum ProjectileModelIndex {
        Rocket,
        Bullets,
        Sprayer,
        Star
    }

    public static class ProjectileModelMethods
    {

        public static int GetIndex(ProjectileModelIndex type) {
            switch (type) {
                case ProjectileModelIndex.Rocket:
                    return 0;
                case ProjectileModelIndex.Bullets:
                    return 1;
                case ProjectileModelIndex.Sprayer:
                    return 2;
                case ProjectileModelIndex.Star:
                    return 3;
            }
            Debug.Log("Asking for an unsupported projectile prefab,defaulting to rocket");
            return 0;
        }
    }
}