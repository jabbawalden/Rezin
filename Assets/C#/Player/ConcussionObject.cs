﻿using System.Collections;
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
    MomentumComponent _momentumComponent;
    EnemyBasic enemyOnetest;
    EnemyHopMovement _enemyHopMovement;
    public int concussionDamage = 0;
    AddOns _addOns;

    // Use this for initialization
    void Start ()
    {
        _playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
        _momentumComponent = GameObject.Find("Player").GetComponent<MomentumComponent>();
        _addOns = GameObject.Find("Player").GetComponent<AddOns>();

        if (_playerMain.isSlamming)
        {
            StartCoroutine(DestroyConcussionSlam());
        }
        else if (_playerMain.isDashing)
        {
            StartCoroutine(DestroyConcussionDash());
        }
        else
        {
            StartCoroutine(NormalConcussionKill());
        }

        //if (_addOns.damageIncreaseSplinter)
        //    concussionDamage = _addOns.ConcussionIncreaseDamage();
        //else
        //    concussionDamage = 0;
    }

    IEnumerator NormalConcussionKill()
    {
        yield return new WaitForSeconds(0.08f);
        AddConcussion();
        yield return new WaitForSeconds(0.18f);
        Destroy(gameObject);
    }

    IEnumerator DestroyConcussionSlam()
    {
        yield return new WaitForSeconds(0.08f);
        AddConcussion();
        yield return new WaitForSeconds(0.18f);
        Destroy(gameObject);
        _playerMain.isSlamming = false;
    }


    IEnumerator DestroyConcussionDash()
    {
        yield return new WaitForSeconds(0.01f);
        AddConcussion();
        yield return new WaitForSeconds(0.18f);
        Destroy(gameObject);
        _playerMain.isDashing = false;
    }

    void ConcussionBehaviour(Vector2 direction)
    {
        rbEnemy.AddForce(direction * 3, ForceMode2D.Impulse);
        rbEnemy.AddForce(new Vector2(0, 150));
    }

    void AddConcussion()
    {

        foreach (GameObject enemy in enemies)
        {

            /*
            if (enemy.gameObject != null)
                if (enemy.GetComponent<Rigidbody2D>() != null)
                    rbEnemy = enemy.GetComponent<Rigidbody2D>();

            direction = new Vector2(enemy.transform.position.x - transform.position.x, enemy.transform.position.y - transform.position.y).normalized;
            ConcussionBehaviour(direction);
            */

            if (enemy.GetComponent<HealthComponent>() != null)
            {
                _healthComponent = enemy.GetComponent<HealthComponent>();
                _healthComponent.health -= concussionDamage;
            }
         
            if (enemy.GetComponent<EnemyBasic>() != null)
            {
                enemyOnetest = enemy.GetComponent<EnemyBasic>();
                enemyOnetest.stun = true;
            }
            _addOns.ConcussionAdditionalLifeSteal();
            _momentumComponent.AddMomentum(4);
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
            enemies.Add(collision.gameObject);
        }
        if (collision.CompareTag("eProj") && !haveExploded)
        {
            eProj.Add(collision.gameObject);
        }
    }
}
