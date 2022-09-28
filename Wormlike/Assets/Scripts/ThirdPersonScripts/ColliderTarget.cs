using UnityEngine;

namespace ThirdPersonScripts
{
    public class ColliderTarget : MonoBehaviour
    {
        public WormController Worm { get; private set; }
    
        void Awake () {
            Worm = transform.root.GetComponent<WormController>();
            Debug.Assert(Worm != null, "Collider target without Enemy root!", this);
        }
        public Vector3 Position => transform.position;

    }
}
