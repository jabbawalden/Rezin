using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHopMovement : MonoBehaviour {

    public float hopForce;
    public float forwardForce;
    private float _nextHop;
    public float hopRate;
    private Transform _playerTarget;
    public float aggressionDistance;
    Rigidbody2D _rb;
    public BoxCollider2D physicsCollider;
    HealthComponent _healthComponent;

	// Use this for initialization
	void Start ()
    {
        hopRate = Random.Range(1.7f, 2.25f);
        _playerTarget = GameObject.Find("Player").transform;
        _healthComponent = GetComponent<HealthComponent>();
        _rb = GetComponent<Rigidbody2D>();
	}

    private float GetDistanceFromPlayer()
    {
        float distance = Vector2.Distance(transform.position, _playerTarget.position);
        return distance;
    }

    private Vector2 GetDirectionToPlayer()
    {
        Vector2 direction = new Vector2(transform.position.x - _playerTarget.position.x, transform.position.y - _playerTarget.position.y);
        return direction;
    }

    private void SetEnemyDirection()
    {
        if (GetDirectionToPlayer().x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            MoveBehaviour(forwardForce, hopForce);
        }
        else if (GetDirectionToPlayer().x > 0)
        {
            transform.localScale = new Vector2(1, 1);
            MoveBehaviour(-forwardForce, hopForce);
        }
    }

    public void MoveBehaviour(float forwardForce, float hopForce)
    {
        if (GetDistanceFromPlayer() <= aggressionDistance)
        {
            if (_nextHop < Time.time)
            {
                hopRate = Random.Range(1.7f, 2.24f);
                _nextHop = Time.time + hopRate;
                _rb.AddForce(new Vector2 (forwardForce, hopForce));
            }
               
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (!_healthComponent.IsAlive())
        {
            Destroy(gameObject);
        }
        else
        {
            SetEnemyDirection();
            //ignore player collision
            Physics2D.IgnoreCollision(_playerTarget.GetComponent<Collider2D>(), physicsCollider);
        }
 
	}
}
