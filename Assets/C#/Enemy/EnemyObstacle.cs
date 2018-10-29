using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObstacle : MonoBehaviour {

    [SerializeField]
    public enum MoveType {Movetowards, Lerp};

    public MoveType moveType;  

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
        if (GameObject.Find("Player") != null)
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

        if(moveType == MoveType.Movetowards)
            transform.position = Vector2.MoveTowards(transform.position, location , deltaSpeed); 
        else
            transform.position = Vector2.Lerp(transform.position, location, deltaSpeed);
    }

    float GetDistance()
    {
        float distance = Vector2.Distance(transform.position, points[destination - 1].transform.position);
        return distance;
    }

    void CheckLocation()
    {
        if (moveType == MoveType.Movetowards)
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
        else
        {
            if (points.Length > 0)
            {
                if (GetDistance() < 0.025f)
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
