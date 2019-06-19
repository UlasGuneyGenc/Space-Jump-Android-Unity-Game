using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed=0.125f;
    public Vector3 desiredPos;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        
            desiredPos = new Vector3(transform.position.x, player.position.y, -20);
            Vector3 smoothedPos = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, 0.1f);
            transform.position = smoothedPos;
        
        
    }

}
