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
    private bool toggleSprint = false;

    [Header("Air & Jumping Controls")]
    [Range(0, 1f)] public float airControl = 0.5f;
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    [Space] public float groundCheckDistance = 0.75f;

    [Header("Animation")]
    public Animator animator; // Reference to Animator component

    [Header("Camera Effects")]
    public Camera playerCamera; // Assign the player's camera in the inspector
    public float baseFOV = 70f; // Default FOV
    public float sprintFOVIncrease = 15f; // Amount to increase FOV when sprinting
    public float fovLerpSpeed = 5f; // How quickly the FOV adjusts
    public float screenShakeIntensity = 0.1f; // Intensity of the screen shake
    public float screenShakeFrequency = 20f; // Frequency of the screen shake

    #region Private Variables
    private Rigidbody rb;
    private InputAction _jumpAction;  // Jump action from Input System
    private InputAction _moveAction;
    private InputAction _sprintAction; // Movement action from Input System
    private InputSystem_Actions inputSystem;
    private bool sprinting;
    private bool jumping;
    public bool grounded;
    private Vector3 moveInput;  // Movement vector
    private float currentSpeed;  // Current movement speed, gradually increasing or decreasing
    private float currentFOV; // Current field of view
    private float shakeOffset; // Offset for screen shake
    #endregion

    public LayerMask groundLayer; // Assign ground layer in the inspector
    private CapsuleCollider playerCollider;
    CapPointManager koth;

    void Awake()
    {
        inputSystem = new InputSystem_Actions();  // Initialize input system actions
        koth = FindObjectOfType<CapPointManager>();
        currentFOV = baseFOV; // Initialize FOV
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
            animator.SetBool("isJumping", true);
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        moveInput = new Vector3(input.x, 0, input.y);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector3.zero;
    }

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
      
        toggleSprint = !toggleSprint;
    }

   

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (playerCamera == null)
        {
            Debug.LogWarning("Player Camera not assigned. Please assign it in the inspector.");
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
            toggleSprint = false; // Reset sprint toggle when input is locked
            animator.SetBool("isMoving", false);
            return;
        }

        // If movement is zero, reset sprint toggle
        if (moveInput.magnitude <= 0.1f)
        {
            toggleSprint = false;
        }

        // Determine sprinting based on toggle and movement
        sprinting = toggleSprint && moveInput.magnitude > 0;

        // Target speed depends on movement and sprinting state
        float targetSpeed = moveInput.magnitude > 0 ? (sprinting ? sprintSpeed : walkSpeed) : 0f;

        // Gradually interpolate the current speed toward the target speed
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, (targetSpeed > currentSpeed ? acceleration : deceleration) * Time.deltaTime);

        // Normalize speed to feed into the animator
        float normalizedSpeed = Mathf.InverseLerp(0f, sprintSpeed, currentSpeed);
        animator.SetFloat("MoveSpeed", normalizedSpeed);

        // Update camera effects
        UpdateFOV();
    }


    private void UpdateFOV()
    {
        if (playerCamera == null) return;

        // Adjust the FOV for sprinting
        float targetFOV = sprinting ? baseFOV + sprintFOVIncrease : baseFOV;
        currentFOV = Mathf.Lerp(currentFOV, targetFOV, fovLerpSpeed * Time.deltaTime);

        // Apply side-to-side shake during sprinting
        float shakeOffsetX = sprinting ? Mathf.Sin(Time.time * screenShakeFrequency) * screenShakeIntensity : 0f;

        // Set the camera's FOV and position offset
        playerCamera.fieldOfView = currentFOV;
        playerCamera.transform.localPosition = new Vector3(shakeOffsetX, playerCamera.transform.localPosition.y, playerCamera.transform.localPosition.z);
    }


    void CheckGrounded()
    {
        Vector3[] offsets = {
            Vector3.zero,
            new Vector3(0, 0, 0.5f),
            new Vector3(0, 0, -0.5f),
            new Vector3(-0.5f, 0, 0),
            new Vector3(0.5f, 0, 0)
        };

        grounded = false;
        animator.SetBool("isJumping", false);

        foreach (Vector3 offset in offsets)
        {
            Vector3 rayOrigin = transform.position + offset + Vector3.up * 0.1f;
            Debug.DrawRay(rayOrigin, Vector3.down * groundCheckDistance, Color.yellow);

            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, groundCheckDistance, groundLayer))
            {
                grounded = true;
                return;
            }
        }
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
                jumping = false;
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
