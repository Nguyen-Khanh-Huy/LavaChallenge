using UnityEngine;

public class BGTop4 : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 1f;
    private Vector3 initialPosition;
    private Vector3 targetPositionUp;
    private bool isMoving = false;
    private bool isPlayerAttached = false;
    private Transform playerTransform;
    private LayerMask playerLayer;

    private void Start()
    {
        initialPosition = transform.position;
        targetPositionUp = initialPosition;
        playerLayer = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.up * 1f, Color.red);

        if (!isMoving && !isPlayerAttached && Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 1f, playerLayer) && hit.collider.GetComponent<Player>() is Player playerScript && !playerScript.IsJumping)
        {
            targetPositionUp = initialPosition + Vector3.up * moveDistance;
            isMoving = true;
            isPlayerAttached = true;
            playerTransform = hit.collider.transform;
            playerTransform.SetParent(transform);
        }

        if (isMoving && isPlayerAttached)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPositionUp, moveSpeed * Time.deltaTime);
            if (transform.position == targetPositionUp)
            {
                isMoving = false;
                playerTransform.SetParent(null);
                playerTransform = null;
            }
        }

        if (!isMoving && isPlayerAttached && !Physics.Raycast(transform.position, Vector3.up, 1.5f, playerLayer))
        {
            targetPositionUp = initialPosition;
            isMoving = true;
            isPlayerAttached = false;
        }

        if (isMoving && !isPlayerAttached)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
            if (transform.position == initialPosition)
            {
                isMoving = false;
            }
        }
    }
}