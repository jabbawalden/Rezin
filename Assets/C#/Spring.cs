using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

    PlayerMain _player;

    private void Start()
    {
        _player = GetComponentInParent<PlayerMain>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (_player.slamConcussion)
                _player.SpringBehaviour();
        }
    }
}
