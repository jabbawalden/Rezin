using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootComponent : MonoBehaviour {

    private PlayerMain _playerMain;
    private UIManager _uiManager;
    public int essenceWorth;
    private Rigidbody2D _rb;

    private void Start()
    {
        _playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(Vector2.up * 250);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _playerMain.essence += essenceWorth;
            _uiManager.UpdateEssence();
            Destroy(gameObject);
        }
    }

}
