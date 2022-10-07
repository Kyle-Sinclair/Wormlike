using Projectiles.ImpactBehaviours;
using Projectiles.ProjectileBehaviours;
using StaticsAndUtilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace WeaponSOs
{
    [CreateAssetMenu(fileName = "Weapon",  menuName = "ScriptableObjects/WeaponSO")]
    public class WeaponSO : ScriptableObject
    {
        public bool Chargable;
        public string WeaponName;
        public GameObject WeaponModel;
        public ProjectileModelIndex ProjectileModel;
        public ProjectileBehaviourType ProjectileBehaviour;
        public ImpactBehaviourType ImpactBehaviourType;
        public float Speed;
        public float Damage;
        public float Force;
        public float Range;

    }
}
