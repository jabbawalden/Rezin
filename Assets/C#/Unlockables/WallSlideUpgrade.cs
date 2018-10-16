using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideUpgrade : UnlockableBaseClass {


    public bool wallSlideUpgrade;

    public void LoadData()
    {
        wallSlideUpgrade = JsonData.gameData.wallSlideUpgrade;

        if (wallSlideUpgrade)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (playerisHere)
        {
            Upgrade();
            wallSlideUpgrade = true;
            Destroy(upgradeObject);
            //Particle FX
            //trigger UI
        }

        //float up and down
    }

    void Upgrade()
    {
        _playerMain.wallSlideUpgrade = true;
    }
}
