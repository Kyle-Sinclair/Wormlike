using System;
using System.Collections;
using System.Collections.Generic;
using Projectiles;
using StaticsAndUtilities;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] public ProjectileFactory _projectileFactory = default;
    [SerializeField] public ImpactEffectFactory _impactEffectFactory = default;
    

    

    public Projectile GetProjectile(ProjectileModelIndex modelIndex)
    {
        return _projectileFactory.Get(ProjectileModelMethods.GetIndex(modelIndex));
    }
    public ImpactEffectFactory GetImpactEffectFactory()
    {
        return _impactEffectFactory;
    }
    

}
