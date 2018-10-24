using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimbUpgrade : UnlockableBaseClass {


    public bool wallClimbUpgrade;

    public void LoadData()
    {
        wallClimbUpgrade = JsonData.gameData.wallClimbUpgrade;

        if (wallClimbUpgrade)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (playerisHere)
        {
            Upgrade();
            wallClimbUpgrade = true;
            Destroy(upgradeObject);
            //Particle FX
            //trigger UI
        }

        //float up and down
    }

    void Upgrade()
    {
        _playerMain.wallClimbUpgrade = true;
    }
}
