using System;
using System.Collections;
using System.Collections.Generic;
using Projectiles;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] public ProjectileFactory _projectileFactory = default;
    [SerializeField] public ImpactEffectFactory _impactEffectFactory = default;
    
    ObjectSpawner instance;

    public void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public ImpactEffectFactory GetImpactEffectFactory()
    {
        return _impactEffectFactory;
    }
    

}
