using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : ImpactEffect
{
    private float damage = 5f;
    static int colorPropertyID = Shader.PropertyToID("_Color");
    private 
    static MaterialPropertyBlock _propertyBlock;
    [SerializeField, Range(0f, 1f)]
    float duration = 0.5f;
    [SerializeField]
    private Color color;
    [SerializeField]
    AnimationCurve opacityCurve = default;

    [SerializeField]
    AnimationCurve scaleCurve = default;
    float age;
    
    float scale;

    MeshRenderer meshRenderer;

    void Awake () {
        //Debug.Log("Initalizing explosion");
        meshRenderer = GetComponent<MeshRenderer>();
        Debug.Assert(meshRenderer != null, "Explosion without renderer!");
    }
    public override void Initialize (Vector3 position, float blastRadius, float damage)
    {
        age = 0f;
        transform.localPosition = position;
        scale =  (2f * blastRadius);
        this.damage = damage;
    }

    public void Update()
    {
        if (_propertyBlock == null) {
            _propertyBlock = new MaterialPropertyBlock();
        }
        float t = age / duration;
        Color c = Color.clear;
        c.a = opacityCurve.Evaluate(t);
        _propertyBlock.SetColor(colorPropertyID, c);
        meshRenderer.SetPropertyBlock(_propertyBlock);
        transform.localScale = Vector3.one * (scale * scaleCurve.Evaluate(t));
        age += Time.deltaTime;
        if (age >= duration) {
            Destroy(this.gameObject);
        }
    }
}

