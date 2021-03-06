﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUpgrade : UnlockableBaseClass {

    // Update is called once per frame
    public bool dashUpgrade;
    
    public void LoadData()
    {
        dashUpgrade = JsonData.gameData.dashUpgrade;

        if (dashUpgrade)
        {
            Destroy(gameObject); 
        }
    }

    void Update ()
    {
        if (playerisHere)
        {
            Upgrade();
            dashUpgrade = true;
            Destroy(upgradeObject);
            //Particle FX
            //trigger UI
        }

        //float up and down
    }

    void Upgrade()
    {
        _playerMain.dashUpgrade = true;
    }
}
