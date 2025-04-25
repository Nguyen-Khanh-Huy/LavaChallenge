using System;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.Image;

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
        int groundMask = LayerMask.GetMask("BG", "BG2", "BG3", "BG4", "BG5", "Box");
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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position + Vector3.down + Vector3.forward * 2f + Vector3.left * 4f, 0.1f);
    //    Gizmos.DrawWireSphere(transform.position + Vector3.down + Vector3.back * 2f + Vector3.left * 4f, 0.1f);

    //    Gizmos.DrawWireSphere(transform.position + Vector3.down + Vector3.forward * 2f + Vector3.right * 4f, 0.1f);
    //    Gizmos.DrawWireSphere(transform.position + Vector3.down + Vector3.back * 2f + Vector3.right * 4f, 0.1f);

    //    Gizmos.DrawWireSphere(transform.position + Vector3.down + Vector3.left * 2f + Vector3.forward * 4f, 0.1f);
    //    Gizmos.DrawWireSphere(transform.position + Vector3.down + Vector3.right * 2f + Vector3.forward * 4f, 0.1f);

    //    Gizmos.DrawWireSphere(transform.position + Vector3.down + Vector3.left * 2f + Vector3.back * 4f, 0.1f);
    //    Gizmos.DrawWireSphere(transform.position + Vector3.down + Vector3.right * 2f + Vector3.back * 4f, 0.1f);

    //    Gizmos.DrawWireSphere(transform.position + Vector3.down + Vector3.left * 4f, 0.1f);
    //    Gizmos.DrawWireSphere(transform.position + Vector3.down + Vector3.right * 4f, 0.1f);
    //    Gizmos.DrawWireSphere(transform.position + Vector3.down + Vector3.forward * 4f, 0.1f);
    //    Gizmos.DrawWireSphere(transform.position + Vector3.down + Vector3.back * 4f, 0.1f);
    //}

    private bool CanMoveDown1(Vector3 direction)
    {
        int DisDownMask = LayerMask.GetMask("BG", "BG2", "BG3", "BG4", "Box", "Box2", "Box3", "Gem", "Soul");
        return Physics.OverlapSphere(transform.position + direction * 2f + Vector3.down * 2f, 0.01f, DisDownMask).Length > 0;
    }

    private bool CanMoveDown2(Vector3 direction)
    {
        int DisDownMask = LayerMask.GetMask("BG", "BG2", "BG3", "BG4", "Box", "Box2", "Box3", "Gem", "Soul");
        return Physics.OverlapSphere(transform.position + direction * 2f + Vector3.down * 3f, 0.01f, DisDownMask).Length > 0;
    }

    private bool CanMoveUp(Vector3 direction)
    {
        int gemSoulMask = LayerMask.GetMask("Gem", "Soul");
        int bgTreeMask = LayerMask.GetMask("BG", "BG2", "BG3", "Tree");

        if (Physics.Raycast(transform.position + Vector3.down + direction, direction, direction.magnitude, gemSoulMask))
            return true;
        return (!Physics.Raycast(transform.position + Vector3.down + direction, direction, direction.magnitude, bgTreeMask));
    }
    private bool CanMoveUpBox(Vector3 direction)
    {
        int mapsMask = LayerMask.GetMask("BG", "BG2", "BG3", "BG4", "Box", "Box2", "Box3", "Gem", "Soul");
        int boxMask = LayerMask.GetMask("Box");
        if (Physics.Raycast(transform.position + Vector3.down + direction, direction, direction.magnitude, boxMask))
            return !(Physics.OverlapSphere(transform.position + Vector3.down + direction * 4f, 0.01f, mapsMask).Length > 0);
        return true;
    }

    private bool Box2Check1(Vector3 direction)
    {
        int mapsMask = LayerMask.GetMask("BG", "BG2", "BG3", "BG4", "Box", "Box2", "Box3", "Gem", "Soul");
        int box2Mask = LayerMask.GetMask("Box2");

        if (Physics.Raycast(transform.position + Vector3.down + direction, direction, out RaycastHit hit, direction.magnitude, box2Mask))
        {
            if (Array.IndexOf(Physics.OverlapSphere(transform.position + Vector3.down + direction * 4f, 0.01f, box2Mask), hit.collider) < 0)
                return !(Physics.OverlapSphere(transform.position + Vector3.down + direction * 4f, 0.01f, mapsMask).Length > 0);
            else if (Array.IndexOf(Physics.OverlapSphere(transform.position + Vector3.down + direction * 4f, 0.01f, box2Mask), hit.collider) >= 0)
                return !(Physics.OverlapSphere(transform.position + Vector3.down + direction * 6f, 0.01f, mapsMask).Length > 0);
        }
        return true;
    }

    private bool Box2Check2(Vector3 direction)
    {
        int mapsMask = LayerMask.GetMask("BG", "BG2", "BG3", "BG4", "Box", "Box2", "Box3", "Gem", "Soul");
        int box2Mask = LayerMask.GetMask("Box2");
        Vector3 pos1 = (direction == Vector3.left || direction == Vector3.right) ? Vector3.forward * 2f : Vector3.left * 2f;
        Vector3 pos2 = (direction == Vector3.left || direction == Vector3.right) ? Vector3.back * 2f : Vector3.right * 2f;

        if (Physics.Raycast(transform.position + Vector3.down + direction, direction, out RaycastHit hit, direction.magnitude, box2Mask))
        {
            if (Array.IndexOf(Physics.OverlapSphere(transform.position + Vector3.down + pos1 + direction * 2f, 0.01f, box2Mask), hit.collider) >= 0)
                return !(Physics.OverlapSphere(transform.position + Vector3.down + pos1 + direction * 4f, 0.01f, mapsMask).Length > 0);
            else if (Array.IndexOf(Physics.OverlapSphere(transform.position + Vector3.down + pos2 + direction * 2f, 0.01f, box2Mask), hit.collider) >= 0)
                return !(Physics.OverlapSphere(transform.position + Vector3.down + pos2 + direction * 4f, 0.01f, mapsMask).Length > 0);
        }
        return true;
    }

    private bool Box3Check1(Vector3 direction)
    {
        int mapsMask = LayerMask.GetMask("BG", "BG2", "BG3", "BG4", "Box", "Box2", "Box3", "Gem", "Soul");
        int box3Mask = LayerMask.GetMask("Box3");

        if (Physics.Raycast(transform.position + Vector3.down + direction, direction, out RaycastHit hit, direction.magnitude, box3Mask))
            return !(Physics.OverlapSphere(transform.position + Vector3.down + direction * 6f, 0.01f, mapsMask).Length > 0);
        return true;
    }

    private bool Box3Check2(Vector3 direction)
    {
        int mapsMask = LayerMask.GetMask("BG", "BG2", "BG3", "BG4", "Box", "Box2", "Box3", "Gem", "Soul");
        int box3Mask = LayerMask.GetMask("Box3");
        Vector3 pos1 = (direction == Vector3.left || direction == Vector3.right) ? Vector3.forward * 2f : Vector3.left * 2f;
        Vector3 pos2 = (direction == Vector3.left || direction == Vector3.right) ? Vector3.back * 2f : Vector3.right * 2f;

        if (Physics.Raycast(transform.position + Vector3.down + direction, direction, out RaycastHit hit, direction.magnitude, box3Mask))
        {
            if (Array.IndexOf(Physics.OverlapSphere(transform.position + Vector3.down + pos1 + direction * 2f, 0.01f, box3Mask), hit.collider) >= 0)
                return !(Physics.OverlapSphere(transform.position + Vector3.down + pos1 + direction * 6f, 0.01f, mapsMask).Length > 0);
            else if (Array.IndexOf(Physics.OverlapSphere(transform.position + Vector3.down + pos2 + direction * 2f, 0.01f, box3Mask), hit.collider) >= 0)
                return !(Physics.OverlapSphere(transform.position + Vector3.down + pos2 + direction * 6f, 0.01f, mapsMask).Length > 0);
        }
        return true;
    }

    private void Moving()
    {
        if (!IsJumping && dir != Vector3.zero)
        {
            if (CanMoveUp(dir) && CanMoveUpBox(dir) && Box2Check1(dir) && Box2Check2(dir) && Box3Check1(dir) && Box3Check2(dir) && CanMoveDown1(dir) && CanMoveDown2(dir))
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