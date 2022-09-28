using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour
#if UNITY_EDITOR
    : ScriptableObject
#endif
{
#if UNITY_EDITOR
    public bool IsReclaimed { get; set; }

    void OnEnable() {
        if (IsReclaimed) {
            Recycle();
        }
    }
#endif
    public abstract void Recycle();
    public abstract void GameUpdate(Projectile projectile);

    public abstract ProjectileBehaviourType BehaviorType { get; }
    public enum ProjectileBehaviourType
    {
        LinearMovement,
        ArcMovement
    }
    
    public static class ProjectileBehaviourTypeMethods
    {

        public static ProjectileBehaviour GetInstance(ProjectileBehaviourType type) {
            switch (type) {
                case ProjectileBehaviourType.LinearMovement:
                    return ProjectileBehaviorPool<LinearMovementProjectileBehaviour>.Get();
                case ProjectileBehaviourType.ArcMovement:
                    return ProjectileBehaviorPool<ArcBehaviour>.Get();
            }
            UnityEngine.Debug.Log("Forgot to support " + type);
            return null;
        }
    }
}
