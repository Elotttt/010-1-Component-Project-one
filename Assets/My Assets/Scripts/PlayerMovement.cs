using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 0.25f;
    float snapDistance = 0.25f;
    float rayLength = 1.4f;
    float rayOffsetX = 0.5f;
    float rayOffsetY = 0.5f;
    float rayOffsetZ = 0.5f;

    Vector3 targetPosition;
    Vector3 startPosition;

    bool moving;

    public GameObject characterModel;
    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (moving)
        {
            if (Vector3.Distance(startPosition, transform.position) > 1f)
            {
                transform.position = targetPosition;
                moving = false;
                return;
            }

            transform.position += (targetPosition - startPosition) * moveSpeed * Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (CanMove(Vector3.forward))
            {
                targetPosition = transform.position + Vector3.forward;
                startPosition = transform.position;
                moving = true;
                characterModel.transform.rotation = Quaternion.Euler(0, 0, 0);

            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (CanMove(Vector3.left))
            {
                targetPosition = transform.position + Vector3.left;
                startPosition = transform.position;
                moving = true;
                characterModel.transform.rotation = Quaternion.Euler(0, -90, 0);

            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (CanMove(Vector3.back))
            {
                targetPosition = transform.position + Vector3.back;
                startPosition = transform.position;
                moving = true;
                characterModel.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (CanMove(Vector3.right))
            {
                targetPosition = transform.position + Vector3.right;
                startPosition = transform.position;
                moving = true;
                characterModel.transform.rotation = Quaternion.Euler(0, 90, 0);

            }
        }
        if (moving)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private bool CanMove(Vector3 direction)
    {
        if (Vector3.Equals(Vector3.forward, direction) || Vector3.Equals(Vector3.back, direction))
        {
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.right * rayOffsetX, direction, rayLength))
            {
                Debug.Log("Can't move forward");
                return false;
            }
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.right * rayOffsetX, direction, rayLength))
            {
                Debug.Log("Can't move backwards");
                return false;
            }
        }
        else if (Vector3.Equals(Vector3.left, direction) || Vector3.Equals(Vector3.right, direction))
        {
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.forward * rayOffsetZ, direction, rayLength))
            {
                Debug.Log("Can't move left");
                return false;
            }
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.forward * rayOffsetZ, direction, rayLength))
            {
                Debug.Log("Can't move right");
                return false;
            }
        }
        return true;
    }
}
