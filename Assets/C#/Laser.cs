using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    HealthComponent healthComponent;

    Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("HitTarget");
            healthComponent = collision.GetComponent<HealthComponent>();
            healthComponent.health -= player.laserDamage;
            Destroy(gameObject);
        }

        //destroy if hits enemy projectile
    }

    private void OnBecameInvisible()
    {
        print("ProjectileDestroyed");
        Destroy(gameObject);
    }
}
