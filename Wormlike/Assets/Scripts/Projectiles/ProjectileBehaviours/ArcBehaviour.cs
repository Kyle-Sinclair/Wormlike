using UnityEngine;

namespace Projectiles.ProjectileBehaviours
{
	public sealed class ArcBehaviour : ProjectileBehaviour
	{
		public Vector3 launchPoint { get; set; }	
		public Vector3 launchVelocity { get; set; }
		float age = 0;
		private float _speed = 15f;
		public override ProjectileBehaviourType BehaviorType
		{
			get {
				return ProjectileBehaviourType.ArcMovement;
			}
		}
		public override void GameUpdate(Projectile projectile) {
			
			projectile.transform.localPosition += projectile.direction * (_speed * Time.deltaTime);
			age += Time.deltaTime;
			Vector3 p = projectile.transform.localPosition;
			p.x += projectile.direction.x * (_speed * Time.deltaTime);
			p.y -= 0.25f * 9.81f * age * age;
			projectile.transform.localPosition = p;
		}

		public override void Initialize()
		{
			
		}

		public override void Recycle() {
			ProjectileBehaviorPool<ArcBehaviour>.Reclaim(this);
		}
	}
}

