using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeeleBasic : MonoBehaviour {

    private float _nextAttack;
    public float attackRate;
    public int damage;
    private PlayerMain _playerMain;
    private HealthComponent _healthComponent;
    bool _attackPlayer;
	// Use this for initialization
	void Start ()
    {
        _playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_attackPlayer && _nextAttack < Time.time)
        {
            _nextAttack = Time.time + attackRate;
            if (!_healthComponent.invincible)
            {
                _healthComponent.health -= damage;
                _playerMain.PlayerDamageBehaviour();
            }
      
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!_playerMain.invulnerable)
            {
                print("Spider Hit Player");
                _healthComponent = collision.GetComponent<HealthComponent>();
                _attackPlayer = true;
            }
        
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _attackPlayer = false;
        }
    }
}
