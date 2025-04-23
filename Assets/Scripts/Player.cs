using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.4f;
    [SerializeField] private float jumpDistance = 2f;
    [SerializeField] private float startTime;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 targetPos;

    [SerializeField] private Vector3 dir = Vector3.zero;
    [SerializeField] private float rotate = 0f;

    public Transform Model;
    public bool IsJumping;
    public int MoveCount = 0;
    public bool CanInput = true;

    private void OnDisable()
    {
        MoveCount = 0;
        IsJumping = false;
        CanInput = true;
        dir = Vector3.zero;
    }

    private void Update()
    {
        GetInput();
        Moving();
        CheckGround();
    }

    private void CheckGround()
    {
        //Debug.DrawRay(transform.position, Vector3.down * 1.5f, Color.green);
        if (IsJumping) return;
        int groundMask = LayerMask.GetMask("BG", "BG4", "BG5", "Box");
        if (!Physics.Raycast(transform.position, Vector3.down, 1.5f, groundMask))
        {
            CanInput = false;
            transform.position += Vector3.down * 1f;
            CanInput = true;
        }
    }

    private void GetInput()
    {
        if (!CanInput || IsJumping) return;
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
    }

    private bool CanMoveDown(Vector3 direction)
    {
        Vector3 normalizedDirection = direction.normalized;
        Vector3 rightAxis = Vector3.Cross(normalizedDirection, Vector3.up).normalized;
        Vector3 finalDirection = Quaternion.AngleAxis(-60f, rightAxis) * normalizedDirection;
        int bg5Mask = LayerMask.GetMask("BG5");
        int treeBg2Bg3Mask = LayerMask.GetMask("Tree", "BG2", "BG3");
        int canMoveToMask = LayerMask.GetMask("Box", "Gem", "BG", "BG4", "BG5", "Soul");
        //Debug.DrawRay(transform.position + Vector3.down * 0.5f, direction * 2f, Color.blue);
        if (Physics.Raycast(transform.position + Vector3.down * 0.5f, direction, 2f, bg5Mask))
            return false;

        RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up * 1f, finalDirection, 4f);
        if (hits.Any(hit => (treeBg2Bg3Mask & (1 << hit.collider.gameObject.layer)) != 0))
            return false;

        return hits.Any(hit => (canMoveToMask & (1 << hit.collider.gameObject.layer)) != 0);
    }

    private bool CanMoveUp(Vector3 direction)
    {
        int gemMask = LayerMask.GetMask("Gem");
        int ignoreMask = LayerMask.GetMask("Soul");
        //Debug.DrawRay(transform.position + Vector3.down * 0.5f, direction * 2f, Color.red);
        if (Physics.Raycast(transform.position + Vector3.down * 0.5f, direction, 2f, gemMask))
            return true;

        //Debug.DrawRay(transform.position + Vector3.down * 0.5f, direction * 4f, Color.red);
        RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.down * 0.5f, direction, 4f);
        hits = hits.Where(hit => (ignoreMask & (1 << hit.collider.gameObject.layer)) == 0).ToArray();
        return hits.Length < 2;
    }

    private void Moving()
    {
        if (!IsJumping && dir != Vector3.zero)
        {
            if (CanMoveDown(dir) && CanMoveUp(dir))
            {
                IsJumping = true;
                startTime = Time.time;
                startPos = transform.position;
                targetPos = startPos + dir * jumpDistance;
                Model.rotation = Quaternion.Euler(-90f, 0f, rotate);
            }
            else dir = Vector3.zero;
        }

        if (IsJumping)
        {
            float t = Mathf.Clamp01((Time.time - startTime) / jumpDuration);
            float yOffset = jumpHeight * 4 * t * (1 - t);
            transform.position = Vector3.Lerp(startPos, targetPos, t) + Vector3.up * yOffset;
            if (t < 1f) return;
            IsJumping = false;
            dir = Vector3.zero;
        }
    }

    public void MoveFromUIButton(Vector3 direction, float rotationZ)
    {
        if (!CanInput || IsJumping) return;
        dir = direction;
        rotate = rotationZ;
    }
}