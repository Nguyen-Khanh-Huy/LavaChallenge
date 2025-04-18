using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.3f;
    [SerializeField] private float jumpDistance = 2f;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float startTime;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 moveDir;

    private void Update()
    {
        moveDir = Vector3.zero;
        float targetZRotation = 0f;

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            moveDir = Vector3.forward;
            targetZRotation = 90f;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            moveDir = Vector3.back;
            targetZRotation = -90f;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveDir = Vector3.left;
            targetZRotation = 0f;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveDir = Vector3.right;
            targetZRotation = 180f;
        }

        if (!isJumping && moveDir != Vector3.zero)
        {
            isJumping = true;
            startTime = Time.time;
            startPos = transform.position;
            targetPos = startPos + moveDir * jumpDistance;

            transform.rotation = Quaternion.Euler(-90f, 0f, targetZRotation);
        }

        if (isJumping)
        {
            float t = Mathf.Clamp01((Time.time - startTime) / jumpDuration);
            float yOffset = jumpHeight * 4 * t * (1 - t);
            transform.position = Vector3.Lerp(startPos, targetPos, t) + Vector3.up * yOffset;

            if (t >= 1f) isJumping = false;
        }
    }
}