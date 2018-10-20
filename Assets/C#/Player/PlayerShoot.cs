using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {


    public bool shootRebound;

    GameData gameData = new GameData();
    [Header("Player Shoot")]
    public int laserDamage;
    public int maxLaserDamage;
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
        float clampedRate = Mathf.Clamp(newFireRate, 0.1f, 0.6f);
        //if (newFireRate <= 0.1f)
        //{
        //    newFireRate = 0.1f;
        //}
        //print(clampedRate);
        return clampedRate;
        
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

            //Damage shift based on speed
            if (GetFireRateFromDistance() == 0.1f)
                laserDamage = maxLaserDamage - 7;
            else if (GetFireRateFromDistance() < 0.2f)
                laserDamage = maxLaserDamage - 5;
            else if (GetFireRateFromDistance() < 0.3f)
                laserDamage = maxLaserDamage - 3;
            else if (GetFireRateFromDistance() < 0.4f)
                laserDamage = maxLaserDamage - 1;
            else if (GetFireRateFromDistance() < 0.5f)
                laserDamage = maxLaserDamage;
            else if (GetFireRateFromDistance() > 5)
                laserDamage = maxLaserDamage + 1;

            Debug.Log(laserDamage);
            _laser._damage = laserDamage;
            _laser.projectileLife = projectileLife;
        }

    }
}
