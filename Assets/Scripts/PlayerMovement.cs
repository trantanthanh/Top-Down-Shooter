using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    private CharacterController characterController;
    private float verticalVelocity;

    public Vector3 movementDirection;
    [Header("Movement Info")]
    [SerializeField] float moveSpeed = 0f;

    private Vector2 moveInput;
    private Vector2 aimInput;


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
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity = verticalVelocity - 9.81f * Time.deltaTime;
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
