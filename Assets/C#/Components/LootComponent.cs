﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootComponent : MonoBehaviour {

    private PlayerMain _playerMain;
    private UIManager _uiManager;
    public int essenceWorth;
    private Rigidbody2D _rb;
    bool canPull;

    private void Start()
    {
        _playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(Vector2.up * 350);
        StartCoroutine(canPullTimer());
        canPull = false;
    }

    private void Update()
    {
        Vector2 target = _playerMain.transform.position;

        float distance = Vector2.Distance(transform.position, target);

        float deltaPos = 11 * Time.deltaTime;

        if (distance < 5.5f && canPull)
        {
            print("move towards");
            transform.position = Vector2.MoveTowards(transform.position, target, deltaPos);
        }
    }

    IEnumerator canPullTimer()
    {
        yield return new WaitForSeconds(0.8f);
        canPull = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _playerMain.essence += essenceWorth;
            _playerMain.collisionCount--;
            _uiManager.UpdateEssence();
            Destroy(gameObject);
        }
    }

}
