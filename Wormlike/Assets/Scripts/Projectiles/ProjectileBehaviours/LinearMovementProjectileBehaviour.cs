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
			projectile.transform.localPosition += projectile.direction * (_speed * Time.deltaTime);
		}

		public override ProjectileBehaviourType BehaviorType
		{
			get {
				return ProjectileBehaviourType.LinearMovement;
			}
		}

		public override void Initialize()
		{
		}

		public override void Recycle() {
			//Debug.Log("Recycling projectile behavior");
			ProjectileBehaviorPool<LinearMovementProjectileBehaviour>.Reclaim(this);
		}
	}
}
