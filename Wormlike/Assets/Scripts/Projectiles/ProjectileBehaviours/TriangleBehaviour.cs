using UnityEngine;

namespace Projectiles.ProjectileBehaviours
{
	public sealed class TriangleBehaviour : ProjectileBehaviour
	{
		public float InitialHeight { get; set; }
		private float _peakHeight;
		private bool _reachedPeak = false;
		private bool _initialized = false;
		float _age = 0;
		private float _speed = 15f;
		public override ProjectileBehaviourType BehaviorType => ProjectileBehaviourType.TriangleMovement;

		public override void GameUpdate(Projectile projectile) {
			if (!_initialized)
			{
				Initialize(projectile);
			}
			_age += Time.deltaTime;
			Vector3 p = projectile.transform.localPosition;
			p.x += projectile.Direction.x * (_speed * Time.deltaTime);
			p.z += projectile.Direction.z * (_speed * Time.deltaTime);
			if (!_reachedPeak && p.y >= _peakHeight)
			{
				_reachedPeak = true;
			}
			p.y += !_reachedPeak ? _age * Time.deltaTime : -_age * Time.deltaTime;
			projectile.Rb.MovePosition(p);
		}

		private void Initialize(Projectile projectile)
		{
			InitialHeight = projectile.transform.localPosition.y;
			_peakHeight = InitialHeight + 5f;
			_initialized = true;
		}
		public override void Initialize(Projectile projectile, float charge, float speed)
		{
			_speed = speed;
		}
		public override void Recycle()
		{
			_age = 0;
			_initialized = false;
			_reachedPeak = false;
			ProjectileBehaviorPool<TriangleBehaviour>.Reclaim(this);
		}
	}
}

