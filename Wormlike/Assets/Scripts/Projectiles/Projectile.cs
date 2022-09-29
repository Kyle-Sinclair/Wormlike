using System;
using System.Collections.Generic;
using Projectiles.ImpactBehaviours;
using Projectiles.ProjectileBehaviours;
using Unity.VisualScripting;
using UnityEngine;

namespace Projectiles
{
	public class Projectile : MonoBehaviour
	{

		List<ProjectileBehaviour> projectileBehaviorList = new List<ProjectileBehaviour>();
		List<ImpactBehaviour> _impactBehaviourList = new List<ImpactBehaviour>();
		public Vector3 direction;
		private bool hasImpacted = false;
		public int ProjectileId
		{
			get {
				return projectileId;
			}
			set {
				if (projectileId == int.MinValue && value != int.MinValue) {
					projectileId = value;
				}
			}
		}
		int projectileId = int.MinValue;
		[SerializeField]
		public Rigidbody rb;
		public ProjectileFactory OriginFactory
		{
			get {
				return originFactory;
			}
			set {
				if (originFactory == null) {
					originFactory = value;
				}
				else {
					Debug.LogError("Not allowed to change origin factory.");
				}
			}
		}

		ProjectileFactory originFactory;
		public ProjectileBehaviour AddBehavior (ProjectileBehaviourType type) {
			switch (type) {
				case ProjectileBehaviourType.LinearMovement:
					return AddProjectileBehavior<LinearMovementProjectileBehaviour>();
				case ProjectileBehaviourType.ArcMovement:
					return AddProjectileBehavior<ArcBehaviour>();
			}
			Debug.LogError("Forgot to support " + type);
			return null;
		}

		

		public ImpactBehaviour AddBehavior (ImpactBehaviourType type) {
			switch (type) {
				case ImpactBehaviourType.ExplodeOnImpact:
					return AddImpactBehavior<ExplodeOnImpactBehaviour>();
			}
			Debug.LogError("Forgot to support " + type);
			return null;
		}
		public T AddProjectileBehavior<T>() where T : ProjectileBehaviour, new() {
			T behavior = ProjectileBehaviorPool<T>.Get();
			projectileBehaviorList.Add(behavior);
			return behavior;
		}
		public T AddImpactBehavior<T>() where T : ImpactBehaviour, new() {
			T behavior = ImpactBehaviourPool<T>.Get();
			_impactBehaviourList.Add(behavior);
			return behavior;
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (!hasImpacted)
			{
				hasImpacted = true;

				for (int i = 0; i < _impactBehaviourList.Count; i++)
				{
					//Debug.Log("Going through behaviour list");
					_impactBehaviourList[i].Trigger(this);
				}

				Recycle();
			}
		}

		public void Reset()
		{
			hasImpacted = false;
			direction = Vector3.zero;
		}
		public void GameUpdate() {
			for (int i = 0; i < projectileBehaviorList.Count; i++) {
				//Debug.Log("Going through projectile behaviour list");
				projectileBehaviorList[i].GameUpdate(this);
			}
			
		}

		public void Recycle()
		{
			Debug.Log("Calling recycle on this projectile");
			for (int i = 0; i < projectileBehaviorList.Count; i++) {
				projectileBehaviorList[i].Recycle();
			}
			projectileBehaviorList.Clear();
			for (int i = 0; i < projectileBehaviorList.Count; i++) {
				_impactBehaviourList[i].Recycle();
			}
			_impactBehaviourList.Clear();

			OriginFactory.Reclaim(this);
		}

		public void Update() {
			
			GameUpdate();
		}
	
	
	}
}
