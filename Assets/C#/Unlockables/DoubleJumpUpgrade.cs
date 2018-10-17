using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpUpgrade : UnlockableBaseClass {

    public bool doubleJumpUpgrade;

    public void LoadData()
    {
        doubleJumpUpgrade = JsonData.gameData.doubleJumpUpgrade;

        if (doubleJumpUpgrade)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (playerisHere)
        {
            Upgrade();
            doubleJumpUpgrade = true;
            Destroy(gameObject);
            //Particle FX
            //trigger UI 
        }

        //float up and down
    }


    void Upgrade()
    {
        _playerMain.doubleJumpUpgrade = true;
        _playerMain.jumpCount = 2;
    }
}
