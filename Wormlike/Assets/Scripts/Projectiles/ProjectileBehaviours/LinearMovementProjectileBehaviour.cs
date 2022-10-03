using UnityEngine;

namespace Projectiles.ProjectileBehaviours
{
	public sealed class LinearMovementProjectileBehaviour : ProjectileBehaviour
	{

		private float _speed = 5f;
		public LinearMovementProjectileBehaviour()
		{
		}
		public override void GameUpdate(Projectile projectile) {
			Vector3 pos = projectile.transform.localPosition;
			projectile.rb.MovePosition(pos + projectile.direction * (_speed * Time.deltaTime));
		}
		public override ProjectileBehaviourType BehaviorType => ProjectileBehaviourType.LinearMovement;
		public override void Initialize(Projectile projectile, float charge, float speed)
		{
			_speed = speed;
		}
		public override void Recycle() {
			ProjectileBehaviorPool<LinearMovementProjectileBehaviour>.Reclaim(this);
		}
	}
}
