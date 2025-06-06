using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box2 : Box
{
    private bool CheckHoz;

    private void Start()
    {
        CheckHoz = transform.rotation.y == 0f ? true : false;
    }
    private void Update()
    {
        if (isMoving) return;
        float checkLength = 1.2f;
        if (CheckHoz)
        {
            Vector3 startRaycast1 = transform.position + Vector3.up * 0.8f + Vector3.left;
            Vector3 startRaycast2 = transform.position + Vector3.up * 0.8f + Vector3.right;

            CheckDirAround(startRaycast1, Vector3.left, checkLength);
            CheckDirAround(startRaycast2, Vector3.right, checkLength);

            CheckDirAround(startRaycast1, Vector3.forward, checkLength);
            CheckDirAround(startRaycast2, Vector3.forward, checkLength);

            CheckDirAround(startRaycast1, Vector3.back, checkLength);
            CheckDirAround(startRaycast2, Vector3.back, checkLength);

            CheckDirDown(startRaycast1, startRaycast2);
        }
        else
        {
            Vector3 startRaycast1 = transform.position + Vector3.up * 0.8f + Vector3.forward;
            Vector3 startRaycast2 = transform.position + Vector3.up * 0.8f + Vector3.back;

            CheckDirAround(startRaycast1, Vector3.left, checkLength);
            CheckDirAround(startRaycast2, Vector3.left, checkLength);

            CheckDirAround(startRaycast1, Vector3.right, checkLength);
            CheckDirAround(startRaycast2, Vector3.right, checkLength);

            CheckDirAround(startRaycast1, Vector3.forward, checkLength);
            CheckDirAround(startRaycast2, Vector3.back, checkLength);

            CheckDirDown(startRaycast1, startRaycast2);
        }
    }

    private void CheckDirDown(Vector3 startPos1, Vector3 startPos2)
    {
        //Debug.DrawRay(startPos1, Vector3.down * 2f, Color.green);
        //Debug.DrawRay(startPos2, Vector3.down * 2f, Color.green);

        int groundMask = LayerMask.GetMask("BGDown", "BG", "BG2", "Box", "Box2", "Box3");

        bool raycast1Hit = Physics.Raycast(startPos1, Vector3.down, 2f, groundMask);
        bool raycast2Hit = Physics.Raycast(startPos2, Vector3.down, 2f, groundMask);

        if (!raycast1Hit && !raycast2Hit)
            transform.Translate(Vector3.down * 2);
    }
}
