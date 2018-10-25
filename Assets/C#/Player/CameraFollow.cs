using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target; 
    private Vector2 velocity;

    //public float smoothSpeed;
    //public Vector3 offset;

    [SerializeField] float smoothY, smoothX;

    private void FixedUpdate()  
    {
        //Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = smoothedPosition;

        //transform.LookAt(target);

        float posX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothX);
        float posY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothY);

        if (target != null)
            transform.position = new Vector3 (posX, posY, transform.position.z);
    }
}
