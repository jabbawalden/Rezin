using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    public float fireRate;
    private float _nextFire;
    public GameObject eProj; 
    public Transform shotOrigin;
    public float laserSpeed;
    Laser laser;
    public int damage;
    private Transform _playerTarget;

    private void Start()
    {
        _playerTarget = GameObject.Find("Player").transform;
    }

    public void EnemyFire()
    {
        if (_nextFire < Time.time)
        {
            _nextFire = Time.time + fireRate;
            Vector2 direction = new Vector2(transform.position.x - _playerTarget.transform.position.x, transform.position.y - _playerTarget.transform.position.y).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            GameObject shot = Instantiate(eProj, shotOrigin.position, Quaternion.AngleAxis(angle, Vector3.forward));

            if (shot.GetComponent<Rigidbody2D>() != null)
            {
                shot.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * -laserSpeed, direction.y * -laserSpeed);
            }
            
            laser = shot.GetComponent<Laser>();
            laser._damage = damage;
        }
    }
}
