using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentumComponent : MonoBehaviour {

    public bool isEngaged;
    public bool loseMomentum;
    public float timeRemaining;
    public float timeRep;
    public int momentum;
    public float lossRate;
    public float nextLoss;

    // Use this for initialization
    void Start()
    {

    }

    public void AddMomentum(int addMomentum)
    {
        isEngaged = true;
        momentum += addMomentum;
    }

    // Update is called once per frame
    void Update()
    {
        timeRep = Time.time;

        if (isEngaged == true)
        {
            isEngaged = false;
            loseMomentum = false;
            timeRemaining = Time.time + 6;
        }

        if (timeRemaining < Time.time)
            loseMomentum = true;


        if (loseMomentum)
        {
            if (nextLoss < Time.time && momentum > 0)
            {
                nextLoss = Time.time + lossRate;
                momentum--;
                Debug.Log(momentum);
            }
        }

    }
}
