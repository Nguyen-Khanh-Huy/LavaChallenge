using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new(10f, 12f, -10f);
    public float smoothSpeed = 0.125f;

    private void Start()
    {
        transform.position = player.position + offset * 2f;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        desiredPosition.y = offset.y;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //transform.LookAt(player);
    }
}
