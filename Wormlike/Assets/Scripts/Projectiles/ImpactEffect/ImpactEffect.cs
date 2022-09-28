using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    ImpactEffectFactory originFactory;

    public ImpactEffectFactory OriginFactory {
        get => originFactory;
        set {
            Debug.Assert(originFactory == null, "Redefined origin factory!");
            originFactory = value;
        }
    }

    public virtual void Initialize(Vector3 position, float blastRadius, float damage)
    {
        
    }
    public void Recycle () {
        originFactory.Reclaim(this);
    }
}
