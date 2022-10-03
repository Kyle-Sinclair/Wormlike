using Projectiles.ImpactBehaviours;
using Projectiles.ProjectileBehaviours;
using StaticsAndUtilities;
using UnityEngine;

namespace WeaponSOs
{
    [CreateAssetMenu(fileName = "Weapon",  menuName = "ScriptableObjects/WeaponSO")]
    public class WeaponSO : ScriptableObject
    {
        public bool chargable;
        public string weaponName;
        public GameObject weaponModel;
        public ProjectileModelIndex projectileModel;
        public ProjectileBehaviourType projectileBehaviour;
        public ImpactBehaviourType impactBehaviourType;
        public float speed;
        public float damage;
        public float force;
        public float range;

    }
}
