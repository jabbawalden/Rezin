using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcussionObject : MonoBehaviour {

    Rigidbody2D rbEnemy;
    Vector2 direction;
    bool haveExploded;
    [SerializeField] private List<GameObject> enemies;
    HealthComponent _healthComponent;
    PlayerMain _playerMain;
    EnemyOneTest enemyOnetest;
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
        yield return new WaitForSeconds(0.3f);
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
            enemyOnetest = enemy.GetComponent<EnemyOneTest>();
            enemyOnetest.stun = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !haveExploded)
        {
            print("Concussion Hit Success");
            enemies.Add(collision.gameObject);
            Debug.Log(enemies);
        }
    }
}
