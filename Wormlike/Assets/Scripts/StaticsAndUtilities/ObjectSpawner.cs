using Projectiles;
using UnityEngine;

namespace StaticsAndUtilities
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] public ProjectileFactory projectileFactory = default;
        [SerializeField] public ImpactEffectFactory impactEffectFactory = default;
        public Projectile GetProjectile(ProjectileModelIndex modelIndex)
        {
            Debug.Log("model index = " + modelIndex);
            return projectileFactory.Get(ProjectileModelMethods.GetIndex(modelIndex));
        }
        public ImpactEffectFactory GetImpactEffectFactory()
        {
            return impactEffectFactory;
        }
    }
}
