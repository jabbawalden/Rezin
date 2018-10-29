using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSplinter : UnlockableBaseClass {

    public bool healSplinter;

    public void LoadData()
    {
        healSplinter = JsonData.gameData.healSplinter;

        if (healSplinter)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (playerisHere)
        {
            Upgrade();
            healSplinter = true;
            //Particle FX
            //trigger UI
            Destroy(gameObject);
        }

        //float up and down
    }

    void Upgrade()
    {
        addOns.healSplinter = true;
    }

}
