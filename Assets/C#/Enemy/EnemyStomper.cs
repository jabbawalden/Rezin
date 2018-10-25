using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStomper : MonoBehaviour {

    public Transform p1, p2, p3;
    public float speed;
    public int destination;
    public bool moveToOriginal;
    public float startSeconds;
    public bool canMove;
    private HealthComponent _healthComponent;

    private void Start()
    {
        transform.position = p1.position;
        destination = 1;
        moveToOriginal = false;
        canMove = false;
        StartCoroutine(CanMoveTimer());
        _healthComponent = GameObject.Find("Player").GetComponent<HealthComponent>();
    }

    private void Update()
    {
        //distance between position and p3
        float distance = Vector2.Distance(transform.position, p3.position);

        if (canMove)
        {
            if (transform.position == p1.position)
            {
                speed = 0.5f;
                destination = 1;
                moveToOriginal = false;
            }
            else if (transform.position == p2.position && !moveToOriginal)
            {
                destination = 2;
                speed = 8;
            }
            else if (distance <= 0.1f && !moveToOriginal)
            {
                speed = 1.4f;
                destination = 0;
                moveToOriginal = true;
            }

            if (destination == 1)
                MoveObstacle(p2.position, false);
            else if (destination == 2)
                MoveObstacle(p3.position, false);
            else /*if (destination == 0)*/
                MoveObstacle(p1.position, false);

        }

    }

    IEnumerator CanMoveTimer()
    {
        yield return new WaitForSeconds(startSeconds);
        canMove = true;
    }

    void MoveObstacle(Vector2 location, bool lerp)
    {
        float deltaSpeed = speed * Time.deltaTime;

        if (lerp)
            transform.position = Vector2.Lerp(transform.position, location, deltaSpeed);
        else
            transform.position = Vector2.MoveTowards(transform.position, location, deltaSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _healthComponent.health = 0;
    }
}
