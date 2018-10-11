using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneTest : MonoBehaviour {

    HealthComponent healthComp;

	// Use this for initialization
	void Start ()
    {
        healthComp = GetComponent<HealthComponent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (healthComp.health <= 0)
            Destroy(gameObject);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Hit");
        }
    }
}
