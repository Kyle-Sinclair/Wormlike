using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles.ProjectileBehaviours
{
    public enum ProjectileBehaviourType
    {
        LinearMovement,
        LinearAcceleratingMovement,
        TriangleMovement,
        LobMovement
    }
    
    public static class ProjectileBehaviourTypeMethods
    {

        public static ProjectileBehaviour GetInstance(ProjectileBehaviourType type) {
            switch (type) {
                case ProjectileBehaviourType.LinearMovement:
                    return ProjectileBehaviorPool<LinearMovementProjectileBehaviour>.Get();
                case ProjectileBehaviourType.LinearAcceleratingMovement:
                    return ProjectileBehaviorPool<LinearAcceleratingMovementProjectileBehaviour>.Get();
                case ProjectileBehaviourType.TriangleMovement:
                    return ProjectileBehaviorPool<TriangleBehaviour>.Get();
                case ProjectileBehaviourType.LobMovement:
                    return ProjectileBehaviorPool<LobBehaviour>.Get();
            }
            UnityEngine.Debug.Log("Forgot to support " + type);
            return null;
        }
    }
}