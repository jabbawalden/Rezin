using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundUpgrade : UnlockableBaseClass {

    public bool reboundUpgrade;

    public void LoadData()
    {
        reboundUpgrade = JsonData.gameData.reboundUpgrade;

        if (reboundUpgrade)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (playerisHere)
        {
            Upgrade();
            reboundUpgrade = true;
            Destroy(upgradeObject);
            //Particle FX
            //trigger UI
        }

        //float up and down
    }

    void Upgrade()
    {
        _playerMain.reboundUpgrade = true;
    }
}
