using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableBaseClass : MonoBehaviour {

    private PlayerMain _playerMain;
    public bool playerisHere;

    private void Start()
    {
        _playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerisHere = true;
        }
    }
}
