using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.4f;
    [SerializeField] private float jumpDistance = 2f;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float startTime;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 moveDir;
    [SerializeField] private Transform model;

    private Vector3 desiredMoveDir = Vector3.zero;
    private float desiredTargetZRotation = 0f;
    private bool canMoveUpLeft = true;
    private bool canMoveUpRight = true;
    private bool canMoveUpForward = true;
    private bool canMoveUpBack = true;

    private void Update()
    {
        GetInput();
        CheckEnvironment();
        Moving();
    }

    private void GetInput()
    {
        desiredMoveDir = Vector3.zero;
        desiredTargetZRotation = 0f;

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            desiredMoveDir = Vector3.forward;
            desiredTargetZRotation = 90f;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            desiredMoveDir = Vector3.back;
            desiredTargetZRotation = -90f;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            desiredMoveDir = Vector3.left;
            desiredTargetZRotation = 0f;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            desiredMoveDir = Vector3.right;
            desiredTargetZRotation = 180f;
        }
    }

    private void CheckEnvironment()
    {
        canMoveUpLeft = CanMoveUp(Vector3.left);
        canMoveUpRight = CanMoveUp(Vector3.right);
        canMoveUpForward = CanMoveUp(Vector3.forward);
        canMoveUpBack = CanMoveUp(Vector3.back);

        if (!isJumping && desiredMoveDir != Vector3.zero)
        {
            if (!CanMoveDown(desiredMoveDir) || !CanMoveUpDirection(desiredMoveDir))
            {
                desiredMoveDir = Vector3.zero;
            }
        }
    }

    private bool CanMoveDown(Vector3 direction)
    {
        Vector3 normalizedDirection = direction.normalized;
        Vector3 rightAxis = Vector3.Cross(normalizedDirection, Vector3.up).normalized;
        if (rightAxis == Vector3.zero)
            rightAxis = Vector3.right;

        Quaternion rotation = Quaternion.AngleAxis(-45f, rightAxis);
        Vector3 finalDirection = rotation * normalizedDirection;
        Debug.DrawRay(transform.position, finalDirection * 2f, Color.green);

        if (Physics.Raycast(transform.position, finalDirection, out RaycastHit hit, 2f))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Tree"))
            {
                return false;
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Box"))
            {
                return true;
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Gem"))
            {
                return true;
            }
            else if (hit.collider.gameObject.layer != LayerMask.NameToLayer("BG"))
            {
                return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanMoveUpDirection(Vector3 direction)
    {
        if (direction == Vector3.left) return canMoveUpLeft;
        if (direction == Vector3.right) return canMoveUpRight;
        if (direction == Vector3.forward) return canMoveUpForward;
        if (direction == Vector3.back) return canMoveUpBack;
        return true;
    }

    private bool CanMoveUp(Vector3 direction)
    {
        Debug.DrawRay(transform.position, direction * 4f, Color.red);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, 4f);
        return hits.Length < 2;
    }

    private void Moving()
    {
        if (!isJumping && desiredMoveDir != Vector3.zero)
        {
            if (CanMoveDown(desiredMoveDir) && CanMoveUpDirection(desiredMoveDir))
            {
                isJumping = true;
                startTime = Time.time;
                startPos = transform.position;
                targetPos = startPos + desiredMoveDir * jumpDistance;
                moveDir = desiredMoveDir;
                model.rotation = Quaternion.Euler(-90f, 0f, desiredTargetZRotation);
            }
        }

        if (isJumping)
        {
            float t = Mathf.Clamp01((Time.time - startTime) / jumpDuration);
            float yOffset = jumpHeight * 4 * t * (1 - t);
            transform.position = Vector3.Lerp(startPos, targetPos, t) + Vector3.up * yOffset;

            if (t >= 1f) isJumping = false;
        }
        else if (desiredMoveDir == Vector3.zero)
        {
            moveDir = Vector3.zero;
        }
    }
}