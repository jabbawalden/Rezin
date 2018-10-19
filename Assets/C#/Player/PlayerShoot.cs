using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {


    public bool shootRebound;

    GameData gameData = new GameData();
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

    public bool reboundUpgrade;
    // Use this for initialization
    void Start ()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _playerMain = GetComponent<PlayerMain>();
        shootRebound = false;
    }

    public void LoadData()
    {
        shootRebound = JsonData.gameData.ShootRebound;
        reboundUpgrade = JsonData.gameData.reboundUpgrade;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_healthComponent.IsAlive())
        {
            Shoot(GetFireRateFromDistance());
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shootRebound = false;
            print("Set Rebound Off");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shootRebound = true;
            print("Set Rebound On");
        }
    }

    public float GetFireRateFromDistance()
    {
        distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
        float newFireRate = distance / fireRateDivider;

        //if (newFireRate <= 0.1f)
        //{
        //    newFireRate = 0.1f;
        //}


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
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(_playerMain.GetMouseDirection().x * laserSpeed, _playerMain.GetMouseDirection().y * laserSpeed);
                  

            _laser = shot.GetComponent<Laser>();
            _laser._damage = laserDamage;
            _laser.projectileLife = projectileLife;
        }

    }
}
