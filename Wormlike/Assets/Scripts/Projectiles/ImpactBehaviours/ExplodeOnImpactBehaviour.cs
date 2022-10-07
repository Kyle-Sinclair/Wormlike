using System;
using StaticsAndUtilities;
using UnityEngine;

namespace Projectiles.ImpactBehaviours
{
    public sealed class ExplodeOnImpactBehaviour : ImpactBehaviour
    {
        private float Force { get; set; }
        private float Damage  { get; set; }
        private float Range  { get; set; }
        public ExplodeOnImpactBehaviour()
        {
            
        }
        public override void GameUpdate(Projectile projectile) {
            
        }
        public override void Initialize(float damage, float force, float range, ImpactEffectFactory impactEffectFactory)
        {
            this.Damage = damage;
            this.Force = force;
            this.Range = range;
            factory = impactEffectFactory;
        }

        public override ImpactBehaviourType BehaviorType => ImpactBehaviourType.ExplodeOnImpact;

        public override void Trigger(Projectile projectile)
        {
            var effect = factory.GetImpactEffect(BehaviorType);
            effect.Initialize(projectile.transform.position,Range,Damage);
        }
        public override void Recycle() {
            ImpactBehaviourPool<ExplodeOnImpactBehaviour>.Reclaim(this);
        }
    }
}

