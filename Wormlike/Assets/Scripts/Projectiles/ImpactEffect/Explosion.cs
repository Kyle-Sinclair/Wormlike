using UIScripts;
using UnityEngine;

namespace Projectiles.ImpactEffect
{
    public class Explosion : ImpactEffect
    {
        private float _damage = 5f;
        static int _colorPropertyID = Shader.PropertyToID("_Color");
        private static MaterialPropertyBlock _propertyBlock;
        [SerializeField, Range(0f, 1f)]
        float duration = 0.5f;
        [SerializeField]
        private Color color;
        [SerializeField]
        AnimationCurve opacityCurve = default;
        [SerializeField]
        AnimationCurve scaleCurve = default;
        float _age;
        static Collider[] _targetsBuffer = new Collider[100];
        private LayerMask _layerMask = 1<<3;
        float _scale;

        MeshRenderer _meshRenderer;

        void Awake () {
            Debug.Log("Initalizing explosion");
            _meshRenderer = GetComponent<MeshRenderer>();
            Debug.Assert(_meshRenderer != null, "Explosion without renderer!");
            //_layerMask = LayerMask.NameToLayer("Players");
        }
        public override void Initialize (Vector3 position, float blastRadius, float damage)
        {
            _age = 0f;
            transform.localPosition = position;
            _scale =  (2f * blastRadius);
            this._damage = damage;
            DealDamage(blastRadius,damage);
        }

        private void DealDamage(float blastRadius, float damage)
        {
     
            Collider[] targets = Physics.OverlapSphere(
                transform.localPosition, blastRadius,_layerMask
            ); 
            Debug.Log("This number of targets found " + targets.Length);

            if (targets.Length <= 0) return;
        
            foreach (var target in targets)
            {
            
                //Debug.Log(target.name);
                HealthComponent playerHealth = target.GetComponentInChildren<HealthComponent>();
                var closestPoint = target.ClosestPoint(transform.localPosition);
                var distance = Vector3.Distance(closestPoint,transform.position);
                var explosionDamageDropOff = Mathf.InverseLerp(blastRadius, 0, distance);
                playerHealth.TakeDamage((int)(damage * explosionDamageDropOff));
            }
        }
        public void Update()
        {
            if (_propertyBlock == null) {
                _propertyBlock = new MaterialPropertyBlock();
            }
            float t = _age / duration;
            Color c = Color.clear;
            c.a = opacityCurve.Evaluate(t);
            _propertyBlock.SetColor(_colorPropertyID, c);
            _meshRenderer.SetPropertyBlock(_propertyBlock);
            transform.localScale = Vector3.one * (_scale * scaleCurve.Evaluate(t));
            _age += Time.deltaTime;
            if (_age >= duration) {
                Destroy(this.gameObject);
            }
        }
    }
}

