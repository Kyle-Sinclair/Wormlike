using Projectiles.ProjectileBehaviours;
using UnityEngine;

namespace Projectiles.ImpactBehaviours
{
    public abstract class ImpactBehaviour
#if UNITY_EDITOR
        : ScriptableObject
#endif
    {
#if UNITY_EDITOR
        public bool IsReclaimed { get; set; }
        public ImpactEffectFactory factory;
        void OnEnable() {
            if (IsReclaimed) {
                Recycle();
            }

        }
#endif
        public abstract void Recycle();
        public abstract void GameUpdate(Projectile projectile);
        public abstract void Initialize(float damage, float force, float range, ImpactEffectFactory _impactEffectFactory);
        public abstract ImpactBehaviourType BehaviorType { get; }

        public abstract void Trigger(Projectile projectile);

    }
}
