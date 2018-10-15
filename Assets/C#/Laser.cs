using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    HealthComponent healthComponent;
    Rigidbody2D rb;
    public float rotationSpeed;
    public int _damage;
    private Player _player;
    public int projectileLife;
    private CircleCollider2D _reboundCol;
    public float duration;
    [SerializeField]
    public enum ProjectileType {Player, Enemy}
    
    public ProjectileType projType;


    private void Start()
    {
        StartCoroutine(ProjectileLifetime());
        rb = GetComponent<Rigidbody2D>();
        _reboundCol = GetComponent<CircleCollider2D>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (projType == ProjectileType.Player)
        {
            if (_player.rebound)
                _reboundCol.enabled = true;
            else
                _reboundCol.enabled = false;
        }
    }

    IEnumerator ProjectileLifetime()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void Update()
    {
        //alter rotation
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (_player.rebound && projType == ProjectileType.Player)
        {
            if (projectileLife <= 0)
                Destroy(gameObject);
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.collider.CompareTag("Enemy") && projType == ProjectileType.Player)
        //{
        //    Debug.Log("HitTarget");
        //    healthComponent = collision.collider.GetComponent<HealthComponent>();
        //    healthComponent.health -= _damage;
        //    Destroy(gameObject);
        //}

        if (collision.collider.CompareTag("Player") && projType == ProjectileType.Enemy)
        {
            Debug.Log("HitTarget");
            healthComponent = collision.collider.GetComponent<HealthComponent>();
            healthComponent.health -= _damage;
            _player.PlayerDamageBehaviour();
            Destroy(gameObject);
        }

       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && projType == ProjectileType.Player)
        {
            //Debug.Log("HitTarget");
            healthComponent = collision.GetComponent<HealthComponent>();
            healthComponent.health -= _damage;
            Destroy(gameObject);
        }

        if (collision.CompareTag("pProj") && projType == ProjectileType.Enemy)
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("eProj") && projType == ProjectileType.Player)
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground") && !_player.rebound) 
        {
            Destroy(gameObject);
        }
        else
        {
            projectileLife--;
        }

        if (collision.CompareTag("Ground") && projType == ProjectileType.Enemy)
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Shield") && projType == ProjectileType.Player)
        {
            Destroy(gameObject);
        }

        //if (collision.CompareTag("Player") && projType == ProjectileType.Enemy)
        //{
        //    Debug.Log("HitTarget");
        //    healthComponent = collision.GetComponent<HealthComponent>();
        //    healthComponent.health -= _damage;
        //    player.PlayerDamageBehaviour();
        //    Destroy(gameObject);
        //}


        //destroy if hits enemy projectile
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
