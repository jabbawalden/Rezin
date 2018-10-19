using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

    PlayerMain _player;
    public float springForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = collision.GetComponent<PlayerMain>();
            _player.rb.velocity = (new Vector2(0, springForce));
            _player.SpringBehaviour();
        }
    }
}
