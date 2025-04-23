using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box3 : Box
{
    private void Update()
    {
        if (isMoving) return;
        float checkLength = 1.2f;
        Vector3 startRaycast1 = transform.position + Vector3.up * 0.8f + Vector3.left + Vector3.forward;
        Vector3 startRaycast2 = transform.position + Vector3.up * 0.8f + Vector3.left + Vector3.back;

        Vector3 startRaycast3 = transform.position + Vector3.up * 0.8f + Vector3.right + Vector3.forward;
        Vector3 startRaycast4 = transform.position + Vector3.up * 0.8f + Vector3.right + Vector3.back;

        CheckDirAround(startRaycast1, Vector3.left, checkLength);
        CheckDirAround(startRaycast2, Vector3.left, checkLength);
        CheckDirAround(startRaycast1, Vector3.forward, checkLength);
        CheckDirAround(startRaycast2, Vector3.back, checkLength);

        CheckDirAround(startRaycast3, Vector3.right, checkLength);
        CheckDirAround(startRaycast4, Vector3.right, checkLength);
        CheckDirAround(startRaycast3, Vector3.forward, checkLength);
        CheckDirAround(startRaycast4, Vector3.back, checkLength);

        CheckDirDown(startRaycast1, startRaycast2, startRaycast3, startRaycast4);
    }

    private void CheckDirDown(Vector3 startPos1, Vector3 startPos2, Vector3 startPos3, Vector3 startPos4)
    {
        int groundMask = LayerMask.GetMask("BGDown", "BG", "BG5", "Box");

        bool raycast1Hit = Physics.Raycast(startPos1, Vector3.down, 2f, groundMask);
        bool raycast2Hit = Physics.Raycast(startPos2, Vector3.down, 2f, groundMask);
        bool raycast3Hit = Physics.Raycast(startPos3, Vector3.down, 2f, groundMask);
        bool raycast4Hit = Physics.Raycast(startPos4, Vector3.down, 2f, groundMask);

        if (!raycast1Hit && !raycast2Hit && !raycast3Hit && !raycast4Hit)
            transform.Translate(Vector3.down * 2);
    }
}
