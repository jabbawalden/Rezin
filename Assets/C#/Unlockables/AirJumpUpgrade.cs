using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJumpUpgrade : UnlockableBaseClass {

    public bool airJumpUpgrade;

    public void LoadData()
    {
        airJumpUpgrade = JsonData.gameData.airJumpUpgrade;

        if (airJumpUpgrade)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (playerisHere)
        {
            Upgrade();
            airJumpUpgrade = true;
            Destroy(gameObject);
            //Particle FX
            //trigger UI 
        }

        //float up and down
    }


    void Upgrade()
    {
        _playerMain.airJumpUpgrade = true;
        _playerMain.jumpCount = 2;
    }
}
