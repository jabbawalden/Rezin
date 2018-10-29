using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJumpOnUpgradeDestroyTest : MonoBehaviour {

    PlayerMain _playerMain;

	// Use this for initialization
	void Start ()
    {
        _playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (_playerMain.airJumpUpgrade)
        {
            Destroy(gameObject);
        }
	}
}
