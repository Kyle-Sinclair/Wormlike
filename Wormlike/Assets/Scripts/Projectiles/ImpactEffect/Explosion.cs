using UIScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Projectiles.ImpactEffect
{
    public class Explosion : ImpactEffect
    {
        [FormerlySerializedAs("duration")] [SerializeField, Range(0f, 1f)]
        private float _duration = 0.5f;
        [SerializeField]
        AnimationCurve opacityCurve = default;
        [SerializeField]
        AnimationCurve scaleCurve = default;
        
        private float _damage = 5f;
        private static int _colorPropertyID = Shader.PropertyToID("_Color");
        private static MaterialPropertyBlock _propertyBlock;
        private LayerMask _layerMask = 1<<3;
        private float _scale;
        private float _age;
        private MeshRenderer _meshRenderer;

        void Awake () {
            //Debug.Log("Initalizing explosion");
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
            if (targets.Length <= 0) return;
            foreach (var target in targets)
            {
                var playerHealth = target.GetComponentInChildren<HealthComponent>();
                var closestPoint = target.ClosestPoint(transform.localPosition);
                var distance = Vector3.Distance(closestPoint,transform.position);
                var explosionDamageDropOff = Mathf.InverseLerp(blastRadius, 0, distance);
                playerHealth.TakeDamage((int)(damage * explosionDamageDropOff));
            }
        }
        public void Update()
        {
            _propertyBlock ??= new MaterialPropertyBlock();
            var t = _age / _duration;
            var c = Color.clear;
            c.a = opacityCurve.Evaluate(t);
            _propertyBlock.SetColor(_colorPropertyID, c);
            _meshRenderer.SetPropertyBlock(_propertyBlock);
            transform.localScale = Vector3.one * (_scale * scaleCurve.Evaluate(t));
            _age += Time.deltaTime;
            if (_age >= _duration) {
                Destroy(this.gameObject);
            }
        }
    }
}

