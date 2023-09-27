using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpForce;
    public LayerMask groundLayerMask;

    private Vector3 direction;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private bool isSprint = false;

    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (isSprint)
        {
            if (GameManager.Instance.Player.status.Stamina > 0) GameManager.Instance.Player.status.Stamina -= 10f * Time.deltaTime;
        }
        else
        {
            if (GameManager.Instance.Player.status.Stamina < 100) GameManager.Instance.Player.status.Stamina += 5f * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            _rigidbody.rotation = targetAngle;
        }

        _rigidbody.velocity = direction * moveSpeed + Vector3.up * _rigidbody.velocity.y;

        _animator.SetBool("IsRun", _rigidbody.velocity != Vector3.zero);
    }


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !Inventory.instance.IsOpen() && context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
            direction = new Vector3(curMovementInput.x, 0f, curMovementInput.y);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            direction = Vector3.zero;
        }
    }

    public void OnSprintInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (GameManager.Instance.Player.status.Stamina > 0)
            {
                moveSpeed *= 1.5f;
                isSprint = true;
                _animator.SetBool("IsSprint", true);
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveSpeed /= 1.5f;
            isSprint = false;
            _animator.SetBool("IsSprint", false);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        //Debug.Log("SPACE");
        //if (!Inventory.instance.IsOpen() && context.phase == InputActionPhase.Started)
        //{
        //    if (IsGrounded())
        //    {
        //        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        //        _animator.SetTrigger("IsJump");
        //    }
        //}
    }

    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        Debug.Log("Inventory!");
        if (context.phase == InputActionPhase.Started)
        {
            ToggleCursor(true);
            Inventory.instance.Toggle();
        }

    }

    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f) , Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f)+ (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        //canLook = !toggle;
    }

    private void DecreaseStamina()
    {
        if (GameManager.Instance.Player.status.Stamina > 0) GameManager.Instance.Player.status.Stamina -= 5;
    }

    private void IncraseStamina()
    {
        if (GameManager.Instance.Player.status.Stamina < 100) GameManager.Instance.Player.status.Stamina += 5;
    }


}