using System;
using System.Collections.Generic;
using Projectiles.ImpactBehaviours;
using Projectiles.ProjectileBehaviours;
using StaticsAndUtilities;
using Unity.VisualScripting;
using UnityEngine;

namespace Projectiles
{
	public class Projectile : MonoBehaviour
	{
		private List<ProjectileBehaviour> _projectileBehaviorList = new List<ProjectileBehaviour>();
		private List<ImpactBehaviour> _impactBehaviourList = new List<ImpactBehaviour>();
		public Vector3 direction;
		private float _age = 0;
		private bool _hasImpacted = false;
		public int ProjectileId
		{
			get => _projectileId;
			set {
				if (_projectileId == int.MinValue && value != int.MinValue) {
					_projectileId = value;
				}
			}
		}
		private int _projectileId = int.MinValue;
		[SerializeField]
		public Rigidbody rb;
		public ProjectileFactory OriginFactory
		{
			get => _originFactory;
			set {
				if (_originFactory == null) {
					_originFactory = value;
				}
				else {
					Debug.LogError("Not allowed to change origin factory.");
				}
			}
		}
		private ProjectileFactory _originFactory;
		public ProjectileBehaviour AddBehavior (ProjectileBehaviourType type)
		{
			//ProjectileBehaviourTypeMethods.GetInstance(this, type);
			switch (type) {
				case ProjectileBehaviourType.LinearMovement:
					return AddProjectileBehavior<LinearMovementProjectileBehaviour>();
				case ProjectileBehaviourType.LinearAcceleratingMovement:
					return AddProjectileBehavior<LinearAcceleratingMovementProjectileBehaviour>();
				case ProjectileBehaviourType.TriangleMovement:
					return AddProjectileBehavior<TriangleBehaviour>();
				case ProjectileBehaviourType.LobMovement:
					return AddProjectileBehavior<LobBehaviour>();
				default : Debug.LogError("Forgot to support " + type);
					return AddProjectileBehavior<LinearMovementProjectileBehaviour>();
			}
		}
		public ImpactBehaviour AddBehavior (ImpactBehaviourType type) {
			switch (type) {
				case ImpactBehaviourType.ExplodeOnImpact:
					return AddImpactBehavior<ExplodeOnImpactBehaviour>();
				default:
					return AddImpactBehavior<ExplodeOnImpactBehaviour>();
			}
		}
		public T AddProjectileBehavior<T>() where T : ProjectileBehaviour, new() {
			var behavior = ProjectileBehaviorPool<T>.Get();
			_projectileBehaviorList.Add(behavior);
			return behavior;
		}

		private T AddImpactBehavior<T>() where T : ImpactBehaviour, new() {
			var behavior = ImpactBehaviourPool<T>.Get();
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
			for (int i = 0; i < _projectileBehaviorList.Count; i++) {
				_projectileBehaviorList[i].Recycle();
			}
			_projectileBehaviorList.Clear();
			for (int i = 0; i < _projectileBehaviorList.Count; i++) {
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
			}
		}
		public void FixedUpdate()
		{
			for (int i = 0; i < _projectileBehaviorList.Count; i++) {
				_projectileBehaviorList[i].GameUpdate(this);
			}		
		}
	}
}
