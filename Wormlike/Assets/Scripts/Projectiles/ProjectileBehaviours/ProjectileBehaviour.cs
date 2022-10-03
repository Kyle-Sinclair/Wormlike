using UnityEngine;

namespace Projectiles.ProjectileBehaviours
{
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
        public abstract void Initialize(Projectile projectile,float charge, float speed);
        public abstract void Recycle();
        public abstract void GameUpdate(Projectile projectile);
        public abstract ProjectileBehaviourType BehaviorType { get; }
  
    }
}
