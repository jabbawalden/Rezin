using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcussionObject : MonoBehaviour {

    Rigidbody2D rbEnemy;
    Vector2 direction;
    bool haveExploded;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<GameObject> eProj;
    HealthComponent _healthComponent;
    PlayerMain _playerMain;
    EnemyBasic enemyOnetest;
    EnemyHopMovement _enemyHopMovement; 
    // Use this for initialization
    void Start ()
    {
        _playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
        StartCoroutine(DestroyConcussion());
    }

    IEnumerator DestroyConcussion()
    {
        yield return new WaitForSeconds(0.01f);
        AddConcussion();
        yield return new WaitForSeconds(0.18f);
        Destroy(gameObject);  
    }

    void ConcussionBehaviour(Vector2 direction)
    {
        rbEnemy.AddForce(direction * 6, ForceMode2D.Impulse);
        rbEnemy.AddForce(new Vector2(0, 200));
    }

    void AddConcussion()
    {
        foreach(GameObject enemy in enemies)
        {
            rbEnemy = enemy.GetComponent<Rigidbody2D>();
            direction = new Vector2(enemy.transform.position.x - transform.position.x, enemy.transform.position.y - transform.position.y).normalized;
            ConcussionBehaviour(direction);
            _healthComponent = enemy.GetComponent<HealthComponent>();
            _healthComponent.health -= _playerMain.concussionDamage;
            if (enemy.GetComponent<EnemyBasic>() != null)
            {
                enemyOnetest = enemy.GetComponent<EnemyBasic>();
                enemyOnetest.stun = true;
            }

            //_enemyHopMovement = enemy.GetComponent<EnemyHopMovement>();
        }

        foreach(GameObject proj in eProj)
        {
            Destroy(proj);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !haveExploded)
        {
            print("Concussion Hit Success");
            enemies.Add(collision.gameObject);
        }
        if (collision.CompareTag("eProj") && !haveExploded)
        {
            print("Concussion Hit Success");
            eProj.Add(collision.gameObject);
            Debug.Log(enemies);
        }
    }
}
