using System;
using UnityEngine;

namespace Projectiles.ImpactBehaviours
{
    public sealed class ExplodeOnImpactBehaviour : ImpactBehaviour
    {

        private float force { get; set; }
        private float damage  { get; set; }
        private float range  { get; set; }
        private ImpactEffectFactory _impactEffectFactory = default;
        public ExplodeOnImpactBehaviour()
        {
            
        }
        public override void GameUpdate(Projectile projectile) {
            
        }
        public override void Initialize(float damage, float force, float range, ImpactEffectFactory _impactEffectFactory)
        {
            this.damage = damage;
            this.force = force;
            this.range = range;
            factory = _impactEffectFactory;
        }

        public override ImpactBehaviourType BehaviorType
        {
            get {
                return ImpactBehaviourType.ExplodeOnImpact;
            }
        }

        public override void Trigger(Projectile projectile)
        {
            ImpactEffect effect = factory.GetImpactEffect(BehaviorType);
            effect.Initialize(projectile.transform.position,range,damage);
        }
        public override void Recycle() {
            ImpactBehaviourPool<ExplodeOnImpactBehaviour>.Reclaim(this);
        }
    }
}

