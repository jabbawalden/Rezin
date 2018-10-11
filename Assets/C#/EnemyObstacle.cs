using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObstacle : MonoBehaviour {

    [Header("Obstacle Movement")]
    public GameObject[] points;
    public float speed;
    public int destination;
    Rigidbody2D rb;
    Player player;

    HealthComponent healthComponent;

    private float _nextFire;
    [Space(12)]
    [Header("Damage Player")]
    public int damage;
    public float fireRate;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        transform.position = points[0].transform.position;
        destination = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLocation();
        MoveObstacle(points[destination - 1].transform.position); 

    }
    
    void MoveObstacle(Vector2 location)
    {
        float deltaSpeed = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, location , deltaSpeed); 
    }

    void CheckLocation()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            healthComponent = collision.GetComponent<HealthComponent>();
            DamagePlayer();
        }
    }

    void DamagePlayer()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;

            healthComponent.health -= damage;
            player.PlayerDamageBehaviour();
        }
   
    }

}
