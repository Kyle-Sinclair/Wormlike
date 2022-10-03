using Projectiles;
using Projectiles.ImpactBehaviours;
using Projectiles.ProjectileBehaviours;
using StaticsAndUtilities;
using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "Weapon",  menuName = "ScriptableObjects/WeaponSO")]
    public class WeaponSO : ScriptableObject
    {
        public bool Chargable;
        public string weaponName;
        public GameObject weaponModel;
        public ProjectileModelIndex ProjectileModel;
        public ProjectileBehaviourType ProjectileBehaviour;
        public ImpactBehaviourType ImpactBehaviourType;
        public float speed;
        public float damage;
        public float Force;
        public float Range;

    }
}
