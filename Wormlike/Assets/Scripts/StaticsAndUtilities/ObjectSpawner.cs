using ManagerScripts;
using Projectiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace StaticsAndUtilities
{
    public class ObjectSpawner : MonoBehaviour, IGameService
    {
        [FormerlySerializedAs("projectileFactory")] 
        [SerializeField] public ProjectileFactory ProjectileFactory = default;
        [FormerlySerializedAs("impactEffectFactory")] 
        [SerializeField] public ImpactEffectFactory ImpactEffectFactory = default;

        public void Awake()
        {
            ServiceLocator.Current.Register(this);
        }
        public Projectile GetProjectile(ProjectileModelIndex modelIndex)
        {
            return ProjectileFactory.Get(ProjectileModelMethods.GetIndex(modelIndex));
        }
    }
}
