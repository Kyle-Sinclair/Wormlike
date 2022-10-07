using Projectiles.ImpactBehaviours;
using Projectiles.ImpactEffect;
using UnityEngine;
using UnityEngine.Serialization;

namespace StaticsAndUtilities
{
    [CreateAssetMenu]
    public class ImpactEffectFactory : ScriptableObject
    {
        [FormerlySerializedAs("explosionPrefab")] public Explosion ExplosionPrefab = default;
        public T Get<T> (T prefab) where T : ImpactEffect {
            T instance = Instantiate(prefab);
            instance.OriginFactory = this;
            return instance;
        }
        public ImpactEffect GetImpactEffect(ImpactBehaviourType behaviourType) {
            return behaviourType switch {
                ImpactBehaviourType.ExplodeOnImpact =>
                    Get(ExplosionPrefab),
                _ => null
            };
        }
        public void Reclaim (ImpactEffect entity) {
            Debug.Assert(entity.OriginFactory == this, "Wrong factory reclaimed!");
            Destroy(entity.gameObject);
        }
    }
}

