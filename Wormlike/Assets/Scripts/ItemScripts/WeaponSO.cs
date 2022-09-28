using Projectiles;
using Projectiles.ImpactBehaviours;
using Projectiles.ProjectileBehaviours;
using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "Weapon",  menuName = "ScriptableObjects/WeaponSO")]
    public class WeaponSO : ScriptableObject
    {
        [SerializeField]
        public bool Chargable;

        public string weaponName;
        [SerializeField] public GameObject weaponModel;

        [SerializeField] public Projectile bulletPrefab;

        [SerializeField] public ProjectileBehaviourType ProjectileBehaviour;
        [SerializeField] public ImpactBehaviourType ImpactBehaviourType;
        [SerializeField] public float speed;

        [SerializeField] public float Damage;
        [SerializeField] public float Force;
        [SerializeField] public float Range;

    }
}
