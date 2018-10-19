﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObstacle : MonoBehaviour {

    [Header("Obstacle Movement")]
    public GameObject[] points;
    public float speed;
    public int destination;
    private Rigidbody2D _rb;
    private PlayerMain _playerMain;

    private HealthComponent _healthComponent;

    private float _nextFire;
    [Space(12)]
    [Header("Damage Player")]
    public int damage;
    public float fireRate;

    // Use this for initialization
    void Start()
    {
        _playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
        if (points.Length > 0)
            transform.position = points[0].transform.position;

        destination = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLocation();
        if (points.Length > 0)
        {
            MoveObstacle(points[destination - 1].transform.position);
        }
    }
    
    void MoveObstacle(Vector2 location)
    {
        float deltaSpeed = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, location , deltaSpeed); 
    }

    void CheckLocation()
    {
        if (points.Length > 0)
        {
            if (transform.position == points[destination - 1].transform.position)
            {
                if (destination == points.Length)
                {
                    destination = 1;
                }
                else
                {
                    destination++;
                }
            }
        }
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("hitting player");

            if (collision.GetComponent<HealthComponent>() != null)
            {
                _healthComponent = collision.GetComponent<HealthComponent>();

                if (!_healthComponent.invincible)
                {
                    DamagePlayer();
                }

            }

        }
    }

    void DamagePlayer()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;

            _healthComponent.health -= damage;
            _playerMain.PlayerDamageBehaviour();
        }
   
    }

}
