using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    private CharacterController characterController;
    private Animator animator;

    private float verticalVelocity;
    [SerializeField] float gravityScale = 9.81f;

    public Vector3 movementDirection;
    [Header("Movement Info")]
    [SerializeField] float walkSpeed = 0f;
    [SerializeField] float runSpeed = 0f;
    float moveSpeed = 0f;

    private Vector2 moveInput;
    [Header("Aim Info")]
    [SerializeField] Transform aim;
    [SerializeField] LayerMask aimLayerMask;
    private Vector2 aimInput;
    private Vector3 lookingDirection;
    private bool IsMoving = false;
    private bool IsRunning = false;


    private void Awake()
    {
        controls = new PlayerControls();
        controls.Character.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        controls.Character.Movement.canceled += context => moveInput = Vector2.zero;

        controls.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        controls.Character.Aim.canceled += context => aimInput = Vector2.zero;

        controls.Character.Run.performed += context =>
        {
            moveSpeed = runSpeed;
            IsRunning = true;
        };
        controls.Character.Run.canceled += controls =>
        {
            moveSpeed = walkSpeed;
            IsRunning = false;
        };
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        moveSpeed = walkSpeed;
    }

    private void AnimatorController()
    {
        float xVelocity = Vector3.Dot(movementDirection.normalized, transform.right);
        float zVelocity = Vector3.Dot(movementDirection.normalized, transform.forward);

        animator.SetFloat("xVelocity", xVelocity, 0.1f, Time.deltaTime);
        animator.SetFloat("zVelocity", zVelocity, 0.1f, Time.deltaTime);
        animator.SetBool("IsRunning", IsMoving ? IsRunning : false);
        animator.SetBool("IsMoving", IsMoving);
    }

    private void Update()
    {
        ApplyGravity();
        ApplyMovement();
        AimTowardsMouse();
        AnimatorController();
    }

    private void AimTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(aimInput);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, aimLayerMask))
        {
            lookingDirection = hitInfo.point - transform.position;
            lookingDirection.y = 0f;
            lookingDirection.Normalize();

            transform.forward = lookingDirection;
            aim.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
        }
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity = verticalVelocity - gravityScale * Time.deltaTime;
        }
        else
        {
            verticalVelocity = 0f;
        }
    }

    private void ApplyMovement()
    {
        movementDirection = new Vector3(moveInput.x, verticalVelocity, moveInput.y);
        if (movementDirection.magnitude > 0)
        {
            IsMoving = true;
            characterController.Move(movementDirection * Time.deltaTime * moveSpeed);
        }
        else
        {
            IsMoving = false;
        }
    }

    private void Shoot()
    {
        Debug.Log("Shoot");

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
