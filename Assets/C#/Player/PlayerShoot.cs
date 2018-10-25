using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {


    GameData gameData = new GameData();
    [Header("Player Shoot")]
    public int laserDamage;
    public float laserSpeed;
    public GameObject projectile;
    public float fireRate;
    private float _nextFire;
    public Transform shotOrigin;
    float distance;
    private Laser _laser;
    public int projectileLife;
    public float propelForce;

    HealthComponent _healthComponent;
    PlayerMain _playerMain;
    public bool blastUpgrade;
    bool canRecoilPlayer;

    // Use this for initialization
    void Start ()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _playerMain = GetComponent<PlayerMain>();
    }

    public void LoadData()
    {
        blastUpgrade = JsonData.gameData.blastUpgrade;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_healthComponent.IsAlive())
        {
            //Shoot(GetFireRateFromDistance());
            Shoot();
        }

    }

    //public float GetFireRateFromDistance()
    //{
    //    distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
    //    float newFireRate = distance / fireRateDivider;
    //    float clampedRate = Mathf.Clamp(newFireRate, 0.1f, 0.6f);
    //    if (newFireRate <= 0.1f)
    //    {
    //        newFireRate = 0.1f;
    //    }
    //    print(clampedRate);
    //    return clampedRate;

    //}

    private void FixedUpdate()
    {
        float direction;

        if (_playerMain.facingPositive)
            direction = -1;
        else
            direction = 1;

        if (canRecoilPlayer)
            _playerMain.RecoilBehaviour(direction);
    }

    IEnumerator RecoilTime()
    {
        canRecoilPlayer = true;
        _playerMain.rb.velocity = Vector2.up * propelForce;
        yield return new WaitForSeconds(0.3f);
        canRecoilPlayer = false;
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.L) && blastUpgrade && _nextFire < Time.time) 
        {
            _nextFire = Time.time + fireRate;
            GameObject shot = Instantiate(projectile, shotOrigin.position, projectile.transform.rotation);
            

            if (_playerMain.facingPositive)
            {
                shot.transform.rotation = Quaternion.LookRotation(Vector3.forward);
                shot.GetComponent<Rigidbody2D>().velocity = new Vector2(laserSpeed, 0);

            }
            else
            {
                shot.transform.rotation = Quaternion.Euler(0, 0, 180);
                shot.GetComponent<Rigidbody2D>().velocity = new Vector2(-laserSpeed, 0);

            }

            _laser = shot.GetComponent<Laser>();
            _laser._damage = laserDamage;
            //_playerMain.rb.velocity = Vector2.right * 600;

            if (_playerMain.haveJumped)
            {
                StartCoroutine(RecoilTime());
            }

        }

        /*
        if (Input.GetKey(KeyCode.Mouse0) && _nextFire < Time.time && _playerMain.currentEnergy >= 1)
        {
            _nextFire = Time.time + fireRate;

            //crazy maths stuff to rotate sprite on its axis
            //float angle = Mathf.Atan2(_playerMain.GetMouseDirection().y, _playerMain.GetMouseDirection().x) * Mathf.Rad2Deg;

            float angle = Mathf.Atan2(0, 0) * Mathf.Rad2Deg;
            GameObject shot = Instantiate(projectile, shotOrigin.position, Quaternion.AngleAxis(angle, Vector3.forward));
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(_playerMain.GetMouseDirection().x * laserSpeed, _playerMain.GetMouseDirection().y * laserSpeed);

            _laser = shot.GetComponent<Laser>();

            ////Damage shift based on speed
            //if (GetFireRateFromDistance() == 0.1f)
            //    laserDamage = maxLaserDamage - 7;
            //else if (GetFireRateFromDistance() < 0.2f)
            //    laserDamage = maxLaserDamage - 5;
            //else if (GetFireRateFromDistance() < 0.3f)
            //    laserDamage = maxLaserDamage - 3;
            //else if (GetFireRateFromDistance() < 0.4f)
            //    laserDamage = maxLaserDamage - 1;
            //else if (GetFireRateFromDistance() < 0.5f)
            //    laserDamage = maxLaserDamage;
            //else if (GetFireRateFromDistance() > 5)
            //    laserDamage = maxLaserDamage + 1;

            Debug.Log(laserDamage);
            _laser._damage = laserDamage;
            _laser.projectileLife = projectileLife;
        }
        */
    }
}
