using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcussionUpgrade : UnlockableBaseClass
{

    // Update is called once per frame
    public bool concussionUpgrade;

    public void LoadData()
    {
        concussionUpgrade = JsonData.gameData.concussionUpgrade;

        if (concussionUpgrade)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (playerisHere)
        {
            Upgrade();
            concussionUpgrade = true;
            Destroy(upgradeObject);
            //Particle FX
            //trigger UI
        }

        //float up and down
    }

    void Upgrade()
    {
        _playerMain.concussionUpgrade = true;
    }
}
