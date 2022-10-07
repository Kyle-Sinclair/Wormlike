using System.Collections.Generic;
using Projectiles.ImpactBehaviours;
using Projectiles.ProjectileBehaviours;
using StaticsAndUtilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Projectiles
{
	public class Projectile : MonoBehaviour
	{
		private List<ProjectileBehaviour> _projectileBehaviorList = new List<ProjectileBehaviour>();
		private List<ImpactBehaviour> _impactBehaviourList = new List<ImpactBehaviour>();
		[FormerlySerializedAs("direction")] 
		public Vector3 Direction;
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
		[FormerlySerializedAs("rb")] [SerializeField]
		public Rigidbody Rb;
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
			ProjectileBehaviourTypeMethods.GetInstance(this, type);
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
			Direction = Vector3.zero;
			Rb.velocity = Vector3.zero;
			Rb.rotation = Quaternion.identity;
		}

		private void Recycle()
		{
			for (var i = 0; i < _projectileBehaviorList.Count; i++) {
				_projectileBehaviorList[i].Recycle();
			}
			_projectileBehaviorList.Clear();
			for (var i = 0; i < _projectileBehaviorList.Count; i++) {
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
