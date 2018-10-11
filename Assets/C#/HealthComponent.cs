using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int health;

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
