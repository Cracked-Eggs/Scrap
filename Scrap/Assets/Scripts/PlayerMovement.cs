using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Realtime;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [Header("Movement and Speed Settings")]
    public float walkSpeed = 8f;
    public float sprintSpeed = 14f;
    public float maxVelocityChange = 10f;
    public float acceleration = 5f;  // Speed at which we accelerate
    public float deceleration = 10f; // Speed at which we decelerate

    [Header("Air & Jumping Controls")]
    [Range(0, 1f)] public float airControl = 0.5f;
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    [Space] public float groundCheckDistance = 0.75f;

    [Header("Animation")]
    public Animator animator; // Reference to Animator component

    #region Private Variables
    private Rigidbody rb;
    private InputAction _jumpAction;  // Jump action from Input System
    private InputAction _moveAction;
    private InputAction _sprintAction;// Movement action from Input System
    private InputSystem_Actions inputSystem;
    private bool sprinting;
    private bool jumping;
    private bool grounded;
    private Vector3 moveInput;  // Movement vector
    private float currentSpeed;  // Current movement speed, gradually increasing or decreasing
    #endregion
    public LayerMask groundLayer; // Assign ground layer in the inspector
    private CapsuleCollider playerCollider;

    void Awake()
    {
        inputSystem = new InputSystem_Actions();  // Initialize input system actions
    }

    public override void OnEnable()
    {
        // Bind input actions
        _jumpAction = inputSystem.Player.Jump;
        _moveAction = inputSystem.Player.Move;

        _jumpAction.performed += OnJumpPerformed; // Subscribe to jump event
        _moveAction.performed += OnMovePerformed; // Subscribe to move event
        _moveAction.canceled += OnMoveCanceled; // Handle stopping movement

        _sprintAction = inputSystem.Player.Sprint; // Bind Sprint action
        _sprintAction.performed += OnSprintPerformed; // Subscribe to sprint event
        _sprintAction.canceled += OnSprintCanceled; // Subscribe to sprint cancel
        _sprintAction.Enable();

        _jumpAction.Enable();
        _moveAction.Enable();
    }

    public override void OnDisable()
    {
        _jumpAction.Disable();
        _moveAction.Disable();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (grounded)
        {
            jumping = true; // Only jump when grounded
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Get movement input as a Vector2 from the action
        Vector2 input = context.ReadValue<Vector2>();
        moveInput = new Vector3(input.x, 0, input.y);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector3.zero; // Stop movement when input is canceled
    }
    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        sprinting = true; // Enable sprinting when the sprint button is pressed
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        sprinting = false; // Disable sprinting when the sprint button is released
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();

        // Ensure the animator is attached and assigned
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        if (InputManager.LockInput)
        {
            moveInput = Vector3.zero;
            sprinting = false;
            jumping = false;
            animator.SetBool("isMoving", false); // Set to idle when input is locked
            return;
        }

        // Detect sprinting (controller or keyboard left shift)
        sprinting = Input.GetKey(KeyCode.LeftShift);

        // Update the target speed
        float targetSpeed = 0f;

        if (moveInput.magnitude > 0) // Moving
        {
            targetSpeed = sprinting ? sprintSpeed : walkSpeed;
            animator.SetBool("isMoving", true); // Player is moving
        }
        else // Idle
        {
            animator.SetBool("isMoving", false); // Player is idle
        }

        // Gradually change the current speed towards the target speed
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, (targetSpeed > currentSpeed ? acceleration : deceleration) * Time.deltaTime);

        // Normalize the current speed for the Animator (0 to 1 range)
        float normalizedSpeed = Mathf.InverseLerp(0f, sprintSpeed, currentSpeed);

        // Set the moveSpeed parameter in the Animator to drive the blend tree
        animator.SetFloat("MoveSpeed", normalizedSpeed);
    }

    private void CheckGrounded()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, groundCheckDistance, groundLayer))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            grounded = angle < 45f;
        }
        else
        {
            grounded = false;
        }

        Debug.DrawRay(rayOrigin, Vector3.down * groundCheckDistance, grounded ? Color.yellow : Color.red);
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        CheckGrounded();

        if (grounded)
        {
            if (jumping)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumping = false; // Reset the jump flag after applying force
            }
            else
            {
                ApplyMovement(currentSpeed, false);
            }
        }
        else
        {
            if (moveInput.magnitude > 0.1f)
            {
                ApplyMovement(currentSpeed, true);
            }
        }

        grounded = false; // Reset grounded state for the next frame
    }

    private void ApplyMovement(float _speed, bool _inAir)
    {
        Vector3 targetVelocity = moveInput.normalized * _speed;
        targetVelocity = transform.TransformDirection(targetVelocity);

        if (_inAir)
            targetVelocity += rb.velocity * (1 - airControl);

        Vector3 velocityChange = targetVelocity - rb.velocity;

        if (_inAir)
        {
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange * airControl, maxVelocityChange * airControl);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange * airControl, maxVelocityChange * airControl);
        }
        else
        {
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        }

        velocityChange.y = 0;
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}
