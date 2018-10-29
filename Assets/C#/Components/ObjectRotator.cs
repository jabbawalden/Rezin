using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour {

    public float rotateSpeed;
	
	// Update is called once per frame
	void Update ()
    {
        float deltaPos = rotateSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, deltaPos));
	}
}
