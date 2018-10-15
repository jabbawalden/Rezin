﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    [Header("Player Shoot")]
    public int laserDamage;
    public float laserSpeed;
    public GameObject projectile;
    public float fireRateDivider;
    private float _nextFire;
    public Transform shotOrigin;
    float distance;
    private Laser _laser;
    public int projectileLife;

    HealthComponent _healthComponent;
    PlayerMain _playerMain;
    // Use this for initialization
    void Start ()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _playerMain = GetComponent<PlayerMain>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_healthComponent.IsAlive())
        {
            Shoot(GetFireRateFromDistance());
        }
        
    }

    public float GetFireRateFromDistance()
    {
        distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
        float newFireRate = distance / fireRateDivider;
        return newFireRate;
    }

    void Shoot(float newFireRate)
    {
        if (Input.GetKey(KeyCode.Mouse0) && _nextFire < Time.time && _playerMain.currentEnergy >= 1)
        {
            _nextFire = Time.time + newFireRate;

            //crazy maths stuff to rotate sprite on its axis
            float angle = Mathf.Atan2(_playerMain.GetMouseDirection().y, _playerMain.GetMouseDirection().x) * Mathf.Rad2Deg;

            GameObject shot = Instantiate(projectile, shotOrigin.position, Quaternion.AngleAxis(angle, Vector3.forward));
            shot.GetComponent<Rigidbody2D>().velocity =
                new Vector2(
                    Mathf.Clamp(_playerMain.GetMouseDirection().x * laserSpeed, _playerMain.GetMouseDirection().x * 2, _playerMain.GetMouseDirection().x * 3),
                    Mathf.Clamp(_playerMain.GetMouseDirection().y * laserSpeed, _playerMain.GetMouseDirection().y * 2, _playerMain.GetMouseDirection().y * 3));

            _laser = shot.GetComponent<Laser>();
            _laser._damage = laserDamage;
            _laser.projectileLife = projectileLife;
        }

    }
}