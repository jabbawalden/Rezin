using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastUpgrade : UnlockableBaseClass {

    public bool blastUpgrade;

    public void LoadData()
    {
        blastUpgrade = JsonData.gameData.blastUpgrade;

        if (blastUpgrade)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (playerisHere)
        {
            Upgrade();
            blastUpgrade = true;
            Destroy(upgradeObject);
            //Particle FX
            //trigger UI
        }

        //float up and down
    }

    void Upgrade()
    {
        _playerShoot.blastUpgrade = true;
    }
}
