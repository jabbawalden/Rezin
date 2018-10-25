using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    private HealthComponent _healthComponent;
    private EnemyBasic _enemyOnetest;
    Rigidbody2D rb;
    public float rotationSpeed;
    public int _damage;
    private PlayerShoot _playerShoot;
    private PlayerMain _playerMain;
    public int projectileLife;
    //private CircleCollider2D _reboundCol;
    public float duration;
    
    [SerializeField]
    public enum ProjectileType {Player, Enemy}
    
    public ProjectileType projType;
    

    private void Start()
    {
        StartCoroutine(ProjectileLifetime());
        rb = GetComponent<Rigidbody2D>();
        //_reboundCol = GetComponent<CircleCollider2D>();
        _playerShoot = GameObject.Find("Player").GetComponent<PlayerShoot>();
        _playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();

        //if (projType == ProjectileType.Player)
        //{
        //    //set collision to true if reboundUpgrade achieved
        //    if (_playerShoot.blastUpgrade)
        //    {
        //        _reboundCol.enabled = true;

        //        //check if canRebound is activated, if so, set trigger false, else, is set to trigger to act as normal
        //        if (canRebound)
        //            _reboundCol.isTrigger = false;
        //        else
        //            Destroy(_reboundCol);
        //    }
        //    else
        //    {
        //        _reboundCol.enabled = false;
        //    }
        //}
        //else { Destroy(_reboundCol); }

    }

    IEnumerator ProjectileLifetime()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

    private void Update()
    {
        //alter rotation
        //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, angle);

        //if (_playerShoot.blastUpgrade && projType == ProjectileType.Player)
        //{
        //    if (projectileLife <= 0)
        //        Destroy(gameObject);
        //} 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && projType == ProjectileType.Enemy)
        {
            if (!_playerMain.invulnerable)
            {
                _healthComponent = collision.GetComponent<HealthComponent>();
                _healthComponent.health -= _damage;
                _playerMain.PlayerDamageBehaviour();
                Destroy(gameObject);
            }
        }

        if (collision.CompareTag("Enemy") && projType == ProjectileType.Player)
        {
            //Debug.Log("HitTarget");
            _healthComponent = collision.GetComponent<HealthComponent>();
            _healthComponent.health -= _damage;
            //_enemyOnetest = collision.GetComponent<EnemyBasic>();
            //_enemyOnetest.DamageBehaviour();
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

        if (collision.CompareTag("Ground")) 
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
