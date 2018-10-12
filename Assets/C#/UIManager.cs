using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    private Player _player;
    private HealthComponent _healthComponent;

    private GameObject _restartPanel;

    public Text currentHealth;
    public Text currentEnergy;

    // set variables
    private void Awake()
    {
        _restartPanel = GameObject.Find("RestartPanel");
        _player = GameObject.Find("Player").GetComponent<Player>();
        _healthComponent = GameObject.Find("Player").GetComponent<HealthComponent>();
    }

    // Use this for initialization
    void Start ()
    {
        currentHealth.text = "" + _healthComponent.health;
        currentEnergy.text = "" + _player.currentEnergy;
        _restartPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentEnergy.text = "" + _player.currentEnergy;
    }

    // called by player on death
    public void ShowRestartPanel()
    {
        if (_restartPanel != null)
            _restartPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    // called by player on damagebeahviour
    public void UpdateHealth()
    {
        //update health and ensure that it does not go into negative
        if (_healthComponent != null)
        {
            if (_healthComponent.health > 0)
                currentHealth.text = "" + _healthComponent.health;
            else
                currentHealth.text = "0";
        }
            
    }

}
