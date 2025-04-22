using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.4f;
    [SerializeField] private float jumpDistance = 2f;
    public bool IsJumping;
    [SerializeField] private float startTime;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 targetPos;
    public Transform Model;

    private Vector3 dir = Vector3.zero;
    private float rotate = 0f;
    private bool canMoveUpLeft = true;
    private bool canMoveUpRight = true;
    private bool canMoveUpForward = true;
    private bool canMoveUpBack = true;

    private Vector3 pendingMoveDir = Vector3.zero;
    private float pendingRotationZ = 0f;
    public int MoveCount = 0;

    private void OnDisable()
    {
        MoveCount = 0;
        IsJumping = false;
    }
    private void Update()
    {
        GetInput();
        CheckEnvironment();
        Moving();
    }

    private void GetInput()
    {
        dir = Vector3.zero;
        rotate = 0f;

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            dir = Vector3.forward;
            rotate = 90f;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            dir = Vector3.back;
            rotate = -90f;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            dir = Vector3.left;
            rotate = 0f;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            dir = Vector3.right;
            rotate = 180f;
        }

        if (dir == Vector3.zero && pendingMoveDir != Vector3.zero)
        {
            dir = pendingMoveDir;
            rotate = pendingRotationZ;
            pendingMoveDir = Vector3.zero;
        }
    }

    private void CheckEnvironment()
    {
        canMoveUpLeft = CanMoveUp(Vector3.left);
        canMoveUpRight = CanMoveUp(Vector3.right);
        canMoveUpForward = CanMoveUp(Vector3.forward);
        canMoveUpBack = CanMoveUp(Vector3.back);

        if (!IsJumping && dir != Vector3.zero)
        {
            if (!CanMoveDown(dir) || !CanMoveUpDirection(dir))
                dir = Vector3.zero;
        }
    }

    private bool CanMoveDown(Vector3 direction)
    {
        Vector3 normalizedDirection = direction.normalized;
        Vector3 rightAxis = Vector3.Cross(normalizedDirection, Vector3.up).normalized;
        Quaternion rotation = Quaternion.AngleAxis(-60f, rightAxis);
        Vector3 finalDirection = rotation * normalizedDirection;

        //Debug.DrawRay(transform.position + Vector3.down * 0.5f, direction * 2f, Color.blue);
        if (Physics.Raycast(transform.position + Vector3.down * 0.5f, direction, 2f, LayerMask.GetMask("BG5")))
            return false;

        //Debug.DrawRay(transform.position + Vector3.up * 1f, finalDirection * 4f, Color.green);
        RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up * 1f, finalDirection, 4f);
        if (hits.Any(hit => hit.collider.gameObject.layer == LayerMask.NameToLayer("Tree") ||
                            hit.collider.gameObject.layer == LayerMask.NameToLayer("BG2") ||
                            hit.collider.gameObject.layer == LayerMask.NameToLayer("BG3"))) return false;

        return hits.Any(hit => hit.collider.gameObject.layer == LayerMask.NameToLayer("Box") ||
                               hit.collider.gameObject.layer == LayerMask.NameToLayer("Gem") ||
                               hit.collider.gameObject.layer == LayerMask.NameToLayer("BG") ||
                               hit.collider.gameObject.layer == LayerMask.NameToLayer("BG4") ||
                               hit.collider.gameObject.layer == LayerMask.NameToLayer("BG5") ||
                               hit.collider.gameObject.layer == LayerMask.NameToLayer("Soul"));
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
        Debug.DrawRay(transform.position + Vector3.down * 0.5f, direction * 4f, Color.red);
        RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.down * 0.5f, direction, 4f);
        hits = hits.Where(hit =>
        hit.collider.gameObject.layer != LayerMask.NameToLayer("Gem") &&
        hit.collider.gameObject.layer != LayerMask.NameToLayer("BG3") &&
        hit.collider.gameObject.layer != LayerMask.NameToLayer("Soul")).ToArray();
        return hits.Length < 2;
    }

    private void Moving()
    {
        if (!IsJumping && dir != Vector3.zero)
        {
            if (CanMoveDown(dir) && CanMoveUpDirection(dir))
            {
                IsJumping = true;
                startTime = Time.time;
                startPos = transform.position;
                targetPos = startPos + dir * jumpDistance;
                Model.rotation = Quaternion.Euler(-90f, 0f, rotate);
            }
        }

        if (IsJumping)
        {
            float t = Mathf.Clamp01((Time.time - startTime) / jumpDuration);
            float yOffset = jumpHeight * 4 * t * (1 - t);
            transform.position = Vector3.Lerp(startPos, targetPos, t) + Vector3.up * yOffset;
            if (t >= 1f) IsJumping = false;
        }
    }

    public void MoveFromUIButton(Vector3 direction, float rotationZ)
    {
        pendingMoveDir = direction;
        pendingRotationZ = rotationZ;
    }
}