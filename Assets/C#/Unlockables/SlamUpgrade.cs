using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamUpgrade : UnlockableBaseClass {

    public bool slamUpgrade;

    public void LoadData()
    {
        slamUpgrade = JsonData.gameData.slamUpgrade;

        if (slamUpgrade)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (playerisHere)
        {
            Upgrade();
            slamUpgrade = true;
            Destroy(upgradeObject);
            //Particle FX
            //trigger UI
        }

        //float up and down
    }

    void Upgrade()
    {
        _playerMain.slamUpgrade = true;
    }
}
