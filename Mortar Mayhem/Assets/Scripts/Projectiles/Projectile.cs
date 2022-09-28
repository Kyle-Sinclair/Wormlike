using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	List<ProjectileBehaviour> behaviorList = new List<ProjectileBehaviour>();

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


	public T AddBehavior<T>() where T : ProjectileBehaviour, new() {
		T behavior = ProjectileBehaviorPool<T>.Get();

		behaviorList.Add(behavior);
		return behavior;
	}

	public void GameUpdate() {
		for (int i = 0; i < behaviorList.Count; i++) {
			behaviorList[i].GameUpdate(this);
		}
	}

	public void Recycle() {
		for (int i = 0; i < behaviorList.Count; i++) {
			behaviorList[i].Recycle();
		}
		behaviorList.Clear();
		OriginFactory.Reclaim(this);
	}

    public void Update() {
		GameUpdate();
    }

	
}
