using UnityEngine;

public class BGTop4 : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 1f;
    private Vector3 startPos;
    private Vector3 targetPos;
    private bool isMoving = false;
    private bool isPlayerAttached = false;
    private Transform playerTransform;
    private LayerMask playerLayer;

    private void Start()
    {
        startPos = transform.position;
        targetPos = startPos;
        playerLayer = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.up * 1f, Color.red);

        if (!isMoving && !isPlayerAttached 
            && Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 1f, playerLayer) 
            && hit.collider.GetComponent<Player>() is Player playerScript && !playerScript.IsJumping)
        {
            targetPos = startPos + Vector3.up * moveDistance;
            isMoving = true;
            isPlayerAttached = true;
            playerTransform = hit.collider.transform;
            playerTransform.SetParent(transform);
            UIManager.Ins.Player.CanInput = false;
        }

        if (isMoving && isPlayerAttached)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            if (transform.position == targetPos)
            {
                isMoving = false;
                playerTransform.SetParent(null);
                playerTransform = null;
                UIManager.Ins.Player.CanInput = true;
            }
        }

        if (!isMoving && isPlayerAttached && !Physics.Raycast(transform.position, Vector3.up, 1f, playerLayer))
        {
            targetPos = startPos;
            isMoving = true;
            isPlayerAttached = false;
        }

        if (isMoving && !isPlayerAttached)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            if (transform.position == startPos)
            {
                isMoving = false;
            }
        }
    }
}