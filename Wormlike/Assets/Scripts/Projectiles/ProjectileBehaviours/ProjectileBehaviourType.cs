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

        public static ProjectileBehaviour GetInstance(Projectile projectile, ProjectileBehaviourType type)
        {
            switch (type) {
                case ProjectileBehaviourType.LinearMovement:
                    return projectile.AddProjectileBehavior<LinearMovementProjectileBehaviour>();
                case ProjectileBehaviourType.LinearAcceleratingMovement:
                    return projectile.AddProjectileBehavior<LinearAcceleratingMovementProjectileBehaviour>();
                case ProjectileBehaviourType.TriangleMovement:
                    return projectile.AddProjectileBehavior<TriangleBehaviour>();
                case ProjectileBehaviourType.LobMovement:
                    return projectile.AddProjectileBehavior<LobBehaviour>();
            }       
            UnityEngine.Debug.Log("Forgot to support " + type);
            return null;
        }
    }
}