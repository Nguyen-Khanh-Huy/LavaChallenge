using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private bool isMoving = false;
    [SerializeField] private Vector3 _startPos;
    private void Awake()
    {
        _startPos = transform.position;
    }

    private void OnEnable()
    {
        transform.position = _startPos;
    }

    private void Update()
    {
        if (!isMoving)
        {
            float checkLength = 1.2f;
            Vector3 startRaycast = transform.position + Vector3.up * (GetComponent<Collider>().bounds.extents.y);

            CheckDirAround(startRaycast, Vector3.left, checkLength);
            CheckDirAround(startRaycast, Vector3.right, checkLength);
            CheckDirAround(startRaycast, Vector3.forward, checkLength);
            CheckDirAround(startRaycast, Vector3.back, checkLength);

            CheckDirDown(startRaycast, 2.1f);
        }
    }

    private void CheckDirAround(Vector3 startPos, Vector3 direction, float length)
    {
        //Debug.DrawRay(startPos, direction * length, Color.red);
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

    private void CheckDirDown(Vector3 startPos, float length)
    {
        //Debug.DrawRay(startPos, Vector3.down * length, Color.red);
        int groundMask = LayerMask.GetMask("BGDown", "BG", "BG5", "Box");
        if (!Physics.Raycast(startPos, Vector3.down, length, groundMask))
            transform.Translate(Vector3.down * 2);
    }
}
