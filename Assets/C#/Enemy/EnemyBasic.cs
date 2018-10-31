using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour {

    private HealthComponent _healthComp;
    private Rigidbody2D _rb;
    private EnemyShoot _enemyShoot;
    private Transform _playerTarget;
    private PlayerMain _playerMain;
    [Header("Enemy Movement")]
    public float movementSpeed;
    public bool facingForward;
    [Space(5)]
    private Vector2 _startPos;

    [Space(5)]
    [Header("Enemy Aggro")]
    public float shootRange;
    public float distanceKept;
    public float distanceAggro;
    public bool aggro;

    public bool stun;
    void Start ()
    {
        _enemyShoot = GetComponent<EnemyShoot>();
        _healthComp = GetComponent<HealthComponent>(); 
        _playerTarget = GameObject.Find("Player").transform;
        _playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
        _rb = GetComponent<Rigidbody2D>();
        _startPos = transform.position;
        stun = false;
    }


    private float GetDistanceFromPlayer()
    {
        float distance = Vector2.Distance(transform.position, _playerTarget.position);
        return distance;
    }

	// Update is called once per frame
	void Update ()
    {
        if (_healthComp.health <= 0)
        {

            Destroy(gameObject);
        }
            

        if(!stun)
        {
            EnemyBasicBehaviour();
            SetDirection();
        }

        if (stun)
        {
            StartCoroutine(StunBehaviour(_playerMain.stunTime));
        }
      

	}

    void SetDirection()
    {
        if (_rb.velocity.x < 0 && !aggro)
        {
            facingForward = false;
        }
        else if (_rb.velocity.x > 0 && !aggro)
        {
            facingForward = true;
        }

        if (_playerTarget.position.x < transform.position.x && aggro)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingForward = false;
        }
        else if (_playerTarget.position.x > transform.position.x && aggro)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingForward = true;
        }
    }

    IEnumerator StunBehaviour(float seconds)
    {
        yield return new WaitForSeconds(1);
        stun = false;
    }

    void EnemyBasicBehaviour()
    {
        float deltaPosition = movementSpeed * Time.deltaTime;
        if (_playerMain != null)
        {
            if (!_playerMain.dead)
            {
                if (GetDistanceFromPlayer() <= distanceAggro && GetDistanceFromPlayer() >= distanceKept)
                {
                    transform.position = Vector2.MoveTowards(transform.position, _playerTarget.position, deltaPosition);
                    aggro = true;
                }
                else if (GetDistanceFromPlayer() <= distanceKept)
                {
                    transform.position = Vector2.MoveTowards(transform.position, _playerTarget.position, -deltaPosition);
                    aggro = true;
                }

                if (GetDistanceFromPlayer() > distanceAggro)
                {
                    transform.position = Vector2.MoveTowards(transform.position, _startPos, deltaPosition);
                }

                if (GetDistanceFromPlayer() <= shootRange)
                {
                    _enemyShoot.EnemyFire();
                }
            }
          
        }


    }

    public void DamageBehaviour()
    {
        _rb.AddForce(Vector2.up * 50);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Hit");
        }
    }
}
