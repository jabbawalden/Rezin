using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerObject : MonoBehaviour {

    private HealthComponent _healthComponent;
    private PlayerMain _playerMain;
    private UIManager _uiManager;
    public int damage;

	// Use this for initialization
	void Start ()
    {
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        StartCoroutine(LifeTime());
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _playerMain = collision.collider.GetComponent<PlayerMain>();
            _healthComponent = collision.collider.GetComponent<HealthComponent>();
            _healthComponent.health -= damage;
            _uiManager.UpdateHealth();
            _playerMain.PlayerDamageBehaviour();
        }

        if (collision.collider.CompareTag("Ground"))
            Destroy(gameObject);
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }


}
