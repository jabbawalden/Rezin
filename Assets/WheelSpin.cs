using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour {

    public float rotateSpeed;
    public float direction;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		float deltaSpeed = rotateSpeed *  Time.deltaTime;
        transform.Rotate(Vector3.forward * deltaSpeed * direction);
	}
}
