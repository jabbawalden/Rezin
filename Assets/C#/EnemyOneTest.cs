using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneTest : MonoBehaviour {

    HealthComponent healthComp;
    Transform playerTarget;
    private Player _player;
    public float movementSpeed;
    float _nextFire;
    public float fireRate;
    public GameObject eProj;
    public Transform shotOrigin;
    public float laserSpeed;
    Laser laser;
    public int damage;
    // Use this for initialization


    void Start ()
    {
        healthComp = GetComponent<HealthComponent>();
        playerTarget = GameObject.Find("Player").transform;
        _player = GameObject.Find("Player").GetComponent<Player>();
    }


    private float GetDistanceFromPlayer()
    {
        float distance = Vector2.Distance(transform.position, playerTarget.position);
        return distance;
    }

	// Update is called once per frame
	void Update ()
    {
        if (healthComp.health <= 0)
            Destroy(gameObject);
        EnemyBasicBehaviour();
        //Debug.Log(GetDistanceFromPlayer());

	}

    void EnemyBasicBehaviour()
    {
        float deltaPosition = movementSpeed * Time.deltaTime;
        if (_player != null)
        {
            if (!_player.dead)
            {
                if (GetDistanceFromPlayer() <= 10 && GetDistanceFromPlayer() >= 4)
                {
                    transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, deltaPosition);
                }
                else if (GetDistanceFromPlayer() <= 3.9)
                {
                    transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, -deltaPosition);
                }

                if (GetDistanceFromPlayer() <= 8)
                {
                    EnemyShoot();
                }
            }
          
        }


    }

    void EnemyShoot()
    {
        if (_nextFire < Time.time)
        {
            _nextFire = Time.time + fireRate;
            Vector2 direction = new Vector2(transform.position.x - playerTarget.transform.position.x, transform.position.y - playerTarget.transform.position.y + 0.5f).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            GameObject shot = Instantiate(eProj, shotOrigin.position, Quaternion.AngleAxis(angle, Vector3.forward));
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * -laserSpeed, direction.y * -laserSpeed);

            laser = shot.GetComponent<Laser>();
            laser._damage = damage;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Hit");
        }
    }
}
