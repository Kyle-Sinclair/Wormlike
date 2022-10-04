using ManagerScripts;
using Projectiles;
using UnityEngine;

namespace StaticsAndUtilities
{
    public class ObjectSpawner : MonoBehaviour, IGameService
    {
        [SerializeField] public ProjectileFactory projectileFactory = default;
        [SerializeField] public ImpactEffectFactory impactEffectFactory = default;

        public void Awake()
        {
            ServiceLocator.Current.Register(this);

        }
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
