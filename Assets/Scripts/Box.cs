using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private bool isMoving = false;
    private void Update()
    {
        if (!isMoving)
        {
            float checkLength = 1.1f;
            Vector3 startRaycast = transform.position + Vector3.up * (GetComponent<Collider>().bounds.extents.y);

            CheckCollision(startRaycast, Vector3.left, checkLength);
            CheckCollision(startRaycast, Vector3.right, checkLength);
            CheckCollision(startRaycast, Vector3.forward, checkLength);
            CheckCollision(startRaycast, Vector3.back, checkLength);

            CheckDownCollision(startRaycast, 2.1f);
        }
    }

    private void CheckCollision(Vector3 startPos, Vector3 direction, float length)
    {
        Debug.DrawRay(startPos, direction * length, Color.red);
        if (Physics.Raycast(startPos, direction, out RaycastHit hit, length) && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player") && direction != Vector3.down)
            StartCoroutine(MoveBoxOppositeDirection(direction));
    }

    private IEnumerator MoveBoxOppositeDirection(Vector3 direction)
    {
        isMoving = true;
        Vector3 moveDirection = -direction;

        float moveDistance = 2f;
        float moveTime = 0.3f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + moveDirection * moveDistance;

        float elapsedTime = 0f;
        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private void CheckDownCollision(Vector3 startPos, float length)
    {
        Vector3 downDirection = Vector3.down;
        Debug.DrawRay(startPos, downDirection * length, Color.red);
        if (!Physics.Raycast(startPos, downDirection, length))
            transform.Translate(downDirection * 2);
    }
}
