using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles.ProjectileBehaviours
{
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