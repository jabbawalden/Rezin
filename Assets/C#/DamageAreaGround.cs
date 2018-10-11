using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaGround : MonoBehaviour {

    HealthComponent healthComponent;
    Player player;
    bool playerDetected;
    private float _nextFire;
    public float fireRate;

    private void Update()
    {
        if (playerDetected)
            DamagePlayer();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            healthComponent = collision.GetComponent<HealthComponent>();
            player = collision.GetComponent<Player>();
        }
        else
            playerDetected = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;

        }
    }

    void DamagePlayer()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;
            healthComponent.health -= 2;
            Debug.Log(healthComponent.health);
            player.PlayerDamageBehaviour();
            
        }
    }

}
