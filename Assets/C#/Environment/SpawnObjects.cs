using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour {

    public GameObject objectToSpawn;
    public float minRate;
    public float maxRate;
    public float nextSpawn;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnObj();
	}

    void SpawnObj()
    {
        float r = Random.Range(minRate, maxRate);
        
        if (nextSpawn < Time.time)
        {
            Instantiate(objectToSpawn, transform.position, objectToSpawn.transform.rotation);
            nextSpawn = Time.time + r;
        }
    }
}
