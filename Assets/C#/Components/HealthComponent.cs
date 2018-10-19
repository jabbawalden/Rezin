using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public bool invincible;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        IsAlive();
    }

    public bool IsAlive()
    {
        if (health >= 1)
            return true;
        else
            return false;
        
    }
}
