using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class LinearMovementProjectileBehaviour : ProjectileBehaviour
{
	public Vector3 Velocity { get; set; }

	public override void GameUpdate(Projectile projectile) {
		projectile.transform.localPosition += Velocity * Time.deltaTime;
	}

	public override ProjectileBehaviourType BehaviorType
	{
		get {
			return ProjectileBehaviourType.LinearMovement;
		}
	}
	public override void Recycle() {
		ProjectileBehaviorPool<LinearMovementProjectileBehaviour>.Reclaim(this);
	}
}
