using UnityEngine;

namespace Projectiles.ProjectileBehaviours
{
	public sealed class TriangleBehaviour : ProjectileBehaviour
	{
		public float _initialHeight { get; set; }
		private float _peakHeight;
		private bool reachedPeak = false;
		private bool initialized = false;
		float age = 0;
		private float _speed = 15f;
		public override ProjectileBehaviourType BehaviorType => ProjectileBehaviourType.TriangleMovement;

		public override void GameUpdate(Projectile projectile) {
			if (!initialized)
			{
				Initialize(projectile);
			}
			age += Time.deltaTime;
			Vector3 p = projectile.transform.localPosition;
			p.x += projectile.direction.x * (_speed * Time.deltaTime);
			p.z += projectile.direction.z * (_speed * Time.deltaTime);
			if (!reachedPeak && p.y >= _peakHeight)
			{
				reachedPeak = true;
			}
			p.y += !reachedPeak ? age * Time.deltaTime : -age * Time.deltaTime;
			projectile.rb.MovePosition(p);
		}

		private void Initialize(Projectile projectile)
		{
			_initialHeight = projectile.transform.localPosition.y;
			_peakHeight = _initialHeight + 5f;
			initialized = true;
		}
		public override void Initialize(Projectile projectile, float charge, float speed)
		{
			_speed = speed;
		}
		public override void Recycle()
		{
			age = 0;
			initialized = false;
			reachedPeak = false;
			ProjectileBehaviorPool<TriangleBehaviour>.Reclaim(this);
		}
	}
}

