using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth
{
    public float Health;
    private float _maxHealth;

    public PlayerHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
    }
}
