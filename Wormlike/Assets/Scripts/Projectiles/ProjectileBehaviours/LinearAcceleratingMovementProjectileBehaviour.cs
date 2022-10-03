using UnityEngine;

namespace Projectiles.ProjectileBehaviours
{
	public sealed class LinearAcceleratingMovementProjectileBehaviour : ProjectileBehaviour
	{

		private float _speed = 2f;
		private float age = 0;
		
		public override void GameUpdate(Projectile projectile)
		{
			age += Time.deltaTime;
			// if (age < 1f)
			// {
			// 	projectile.transform.localPosition += projectile.direction * ((_speed * Time.deltaTime));
			// }
			// else
			// {
			// 	projectile.transform.localPosition += projectile.direction * ((_speed + age * 3f)  * Time.deltaTime);
			//
			// }
			if (age > 1f)
			{
				projectile.transform.localPosition += projectile.direction * ((_speed + age * 3f) * Time.deltaTime);
			}

		}

		public override ProjectileBehaviourType BehaviorType => ProjectileBehaviourType.LinearAcceleratingMovement;

		public override void Initialize(Projectile projectile, float charge, float speed)
		{
			//Debug.Log("adding force in this direction " + projectile.direction * 15f);
			projectile.rb.AddRelativeForce(projectile.direction * 20f,ForceMode.Impulse);
			_speed = speed;
		}

		public override void Recycle()
		{
			
			age = 0f;
			ProjectileBehaviorPool<LinearAcceleratingMovementProjectileBehaviour>.Reclaim(this);
		}
	}
}
