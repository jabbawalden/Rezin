﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    [SerializeField]
    public enum GameState
    {
        Play, Pause, Dead
    }

    public GameState gameState;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene(0);
	}
}
