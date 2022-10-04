using StaticsAndUtilities;
using UnityEngine;

namespace Projectiles.ImpactEffect
{
    public class ImpactEffect : MonoBehaviour
    {
        ImpactEffectFactory _originFactory;

        public ImpactEffectFactory OriginFactory {
            get => _originFactory;
            set {
                Debug.Assert(_originFactory == null, "Redefined origin factory!");
                _originFactory = value;
            }
        }

        public virtual void Initialize(Vector3 position, float blastRadius, float damage)
        {
        
        }
        public void Recycle () {
            _originFactory.Reclaim(this);
        }
    }
}
