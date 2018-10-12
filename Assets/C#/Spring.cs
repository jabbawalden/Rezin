using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

    Player _player;
    public float springForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("playerspring");
            _player = collision.GetComponent<Player>();
            _player.rb.velocity = (new Vector2(0, springForce));
            _player.SpringBehaviour();
        }
    }
}
