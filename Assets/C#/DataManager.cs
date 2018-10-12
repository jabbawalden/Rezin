using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

    private void Start()
    {
        //if file exists, load here
    }

    void Save()
    {
        //datacontainer variables = player
    }

    void Load()
    {
        //variables from player = datacontainer
    }
}

public class DataContainer : MonoBehaviour
{
    public Vector2 startPosition;
    public int currenthealth;
    public int maxHealth;
    public float energyRegenerate;
    public int jumpCount;
    public int jumpCountMax;
    public bool upgradeDoubleJump;
    public bool upgradeRebound;

}
