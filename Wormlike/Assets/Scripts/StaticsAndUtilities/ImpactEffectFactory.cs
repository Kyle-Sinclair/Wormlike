using System;
using System.Collections;
using System.Collections.Generic;
using Projectiles.ImpactBehaviours;
using UnityEngine;


    [CreateAssetMenu]
    public class ImpactEffectFactory : ScriptableObject
    {
        public Explosion explosionPrefab = default;
        public T Get<T> (T prefab) where T : ImpactEffect {
            T instance = Instantiate(prefab);
            instance.OriginFactory = this;
            return instance;
        }

        public ImpactEffect GetImpactEffect(ImpactBehaviourType behaviourType)
        {
            //Debug.Log("Attempting to instantiate effect");
            switch (behaviourType)
            {
                case ImpactBehaviourType.ExplodeOnImpact :
                    //Debug.Log("Attempting to instantiate explosion effect");

                    return Get(explosionPrefab); 
                default :    return null;
                            
                   
            }

        }
        public void Reclaim (ImpactEffect entity) {
            Debug.Assert(entity.OriginFactory == this, "Wrong factory reclaimed!");
            Destroy(entity.gameObject);
        }
    }

