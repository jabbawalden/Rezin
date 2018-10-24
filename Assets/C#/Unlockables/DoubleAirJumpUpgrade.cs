using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAirJumpUpgrade : UnlockableBaseClass {

    public bool doubleAirJumpUpgrade;

    public void LoadData()
    {
        doubleAirJumpUpgrade = JsonData.gameData.doubleAirJumpUpgrade;


        if (doubleAirJumpUpgrade)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (playerisHere)
        {
            Upgrade();
            doubleAirJumpUpgrade = true;
            Destroy(upgradeObject);
            //Particle FX
            //trigger UI
        }

        //float up and down
    }

    void Upgrade()
    {
        _playerMain.doubleAirJumpUpgrade = true;
        _playerMain.jumpMaxCount = 3;
    }
}
