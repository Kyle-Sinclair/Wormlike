using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ArcBehaviour : ProjectileBehaviour
{
	public Vector3 Velocity { get; set; }

	public override ProjectileBehaviourType BehaviorType
	{
		get {
			return ProjectileBehaviourType.ArcMovement;
		}
	}
	public override void GameUpdate(Projectile projectile) {
		projectile.transform.localPosition += Velocity * Time.deltaTime;
	}
	public override void Recycle() {
		ProjectileBehaviorPool<ArcBehaviour>.Reclaim(this);
	}
}

