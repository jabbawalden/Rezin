using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneTest : MonoBehaviour {

    private HealthComponent _healthComp;
    private Rigidbody2D _rb;
    private Transform playerTarget;
    private Player _player;
    public float movementSpeed;
    [Space(5)]
    [Header("Enemy Shoot")]
    float _nextFire;
    public float fireRate;
    public GameObject eProj;
    public Transform shotOrigin;
    public float laserSpeed;
    Laser laser;
    public int damage;
    // Use this for initialization
    public Material eMat;
    [Space(5)]
    [Header("Enemy Aggro")]
    public float shootRange;
    public float distanceKept;
    public float distanceAggro;

    void Start ()
    {
        _healthComp = GetComponent<HealthComponent>(); 
        playerTarget = GameObject.Find("Player").transform;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _rb = GetComponent<Rigidbody2D>();
    }


    private float GetDistanceFromPlayer()
    {
        float distance = Vector2.Distance(transform.position, playerTarget.position);
        return distance;
    }

	// Update is called once per frame
	void Update ()
    {
        if (_healthComp.health <= 0)
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
                if (GetDistanceFromPlayer() <= distanceAggro && GetDistanceFromPlayer() >= 4)
                {
                    transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, deltaPosition);
                }
                else if (GetDistanceFromPlayer() <= distanceKept)
                {
                    transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, -deltaPosition);
                }

                if (GetDistanceFromPlayer() <= shootRange)
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
            Vector2 direction = new Vector2(transform.position.x - playerTarget.transform.position.x, transform.position.y - playerTarget.transform.position.y).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            GameObject shot = Instantiate(eProj, shotOrigin.position, Quaternion.AngleAxis(angle, Vector3.forward));
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * -laserSpeed, direction.y * -laserSpeed);

            laser = shot.GetComponent<Laser>();
            laser._damage = damage;
        }
    }

    void DamageBehaviour()
    {
        StartCoroutine(MaterialShift());
        _rb.AddForce(Vector2.up * 50);
    }

    IEnumerator MaterialShift()
    {
        eMat.color = new Color(0, 191, 156);
        yield return new WaitForSeconds(0.34f);
        eMat.color = new Color(80, 0, 255);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Hit");
        }
    }
}
