using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    private CharacterController characterController;

    private float verticalVelocity;
    [SerializeField] float gravityScale = 9.81f;

    public Vector3 movementDirection;
    [Header("Movement Info")]
    [SerializeField] float moveSpeed = 0f;

    private Vector2 moveInput;
    [Header("Aim Info")]
    [SerializeField] Transform aim;
    [SerializeField] LayerMask aimLayerMask;
    private Vector2 aimInput;
    private Vector3 lookingDirection;


    private void Awake()
    {
        controls = new PlayerControls();
        controls.Character.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        controls.Character.Movement.canceled += context => moveInput = Vector2.zero;

        controls.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        controls.Character.Aim.canceled += context => aimInput = Vector2.zero;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyGravity();
        ApplyMovement();
        AimTowardsMouse();
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
            characterController.Move(movementDirection * Time.deltaTime * moveSpeed);
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
