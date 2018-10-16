using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

    HealthComponent _healthComponent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _healthComponent = collision.GetComponent<HealthComponent>();
            _healthComponent.health = 0;
        }
    }

}
