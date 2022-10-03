using UnityEngine;

namespace Projectiles.ProjectileBehaviours
{
    public class LobBehaviour : ProjectileBehaviour
    {
        public LobBehaviour()
        {
        }
        public override void Initialize(Projectile projectile, float charge, float speed)
        {
            projectile.rb.AddForce(projectile.direction * charge * 500f);
        }
        public override void Recycle()
        {
            ProjectileBehaviorPool<LobBehaviour>.Reclaim(this);
        }
        public override ProjectileBehaviourType BehaviorType => ProjectileBehaviourType.LobMovement;

        public override void GameUpdate(Projectile projectile)
        {
            Vector3 velocity = projectile.rb.velocity;
            velocity.y -= 9.8f * Time.deltaTime;
            projectile.rb.velocity = velocity;
        }

    }
}
