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
		private float _age = 0;
		private bool _hasImpacted = false;
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
				case ProjectileBehaviourType.LinearAcceleratingMovement:
					return AddProjectileBehavior<LinearAcceleratingMovementProjectileBehaviour>();
				case ProjectileBehaviourType.TriangleMovement:
					return AddProjectileBehavior<TriangleBehaviour>();
				case ProjectileBehaviourType.LobMovement:
					return AddProjectileBehavior<LobBehaviour>();
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
			if (!_hasImpacted)
			{
				_hasImpacted = true;
				for (int i = 0; i < _impactBehaviourList.Count; i++)
				{
					_impactBehaviourList[i].Trigger(this);
				}
				Recycle();
			}
		}
		public void Reset()
		{
			_hasImpacted = false;
			_age = 0;
			direction = Vector3.zero;
			rb.velocity = Vector3.zero;
			rb.rotation = Quaternion.identity;
		}
		public void Recycle()
		{
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
		public void Update()
		{
			_age += Time.deltaTime;
			if (_age > 50f)
			{
				Recycle();
				return;
			}
		}
		public void FixedUpdate()
		{
			for (int i = 0; i < projectileBehaviorList.Count; i++) {
				//Debug.Log("Going through projectile behaviour list");
				projectileBehaviorList[i].GameUpdate(this);
			}		
		}
	}
}
