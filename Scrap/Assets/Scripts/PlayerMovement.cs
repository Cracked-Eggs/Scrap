using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [Header("Movement and Speed Settings")]
    public float maxSpeed = 10f; // Sprint speed (no walking)
    public float turnSpeed = 720f; // Degrees per second for smooth rotation
    public float drag = 5f; // Momentum-like slowing effect

    [Header("Jumping")]
    public float jumpForce = 8f;
    public float gravityScale = 2f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.5f;

    [Header("Animation")]
    public Animator animator;

    private Rigidbody rb;
    private InputSystem_Actions inputSystem;
    private InputAction _moveAction;
    private InputAction _jumpAction;

    private Vector3 inputDirection = Vector3.zero;
    private bool grounded;
    private bool jumping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // We'll handle custom gravity

        inputSystem = new InputSystem_Actions();

        // Bind actions
        _moveAction = inputSystem.Player.Move;
        _jumpAction = inputSystem.Player.Jump;

        _moveAction.performed += OnMovePerformed;
        _moveAction.canceled += OnMoveCanceled;
        _jumpAction.performed += OnJumpPerformed;

        inputSystem.Player.Enable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputDirection = new Vector3(input.x, 0, input.y);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        inputDirection = Vector3.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (grounded)
        {
            jumping = true;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from actions
        _moveAction.performed -= OnMovePerformed;
        _moveAction.canceled -= OnMoveCanceled;
        _jumpAction.performed -= OnJumpPerformed;

        // Disable input action map
        inputSystem.Player.Disable();
    }

    private void OnDestroy()
    {
        // Same cleanup in case OnDisable is not called
        OnDisable();
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        UpdateAnimator();
        RotateTowardsMovementDirection();
    }

    private void UpdateAnimator()
    {
        if (animator != null)
        {
            // Use max speed as the MoveSpeed since we're sprinting
            float normalizedSpeed = inputDirection.magnitude; // Either 0 or 1 depending on input

            // Debug the normalized speed
            Debug.Log("Normalized MoveSpeed: " + normalizedSpeed);

            animator.SetFloat("MoveSpeed", normalizedSpeed);
            animator.SetBool("isJumping", jumping);
        }
    }

    private void RotateTowardsMovementDirection()
    {
        if (inputDirection.magnitude > 0.1f)
        {
            Vector3 lookDirection = new Vector3(inputDirection.x, 0, inputDirection.z).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        CheckGrounded();
        ApplyGravity();

        if (grounded)
        {
            MoveOnGround();
        }
        else
        {
            MoveInAir();
        }
    }

    private void CheckGrounded()
    {
        grounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance, groundLayer);
    }

    private void ApplyGravity()
    {
        rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
    }

    private void MoveOnGround()
    {
        // Debug Input Direction
        Debug.Log("Input Direction: " + inputDirection);

        if (inputDirection.magnitude > 0)
        {
            // Directly set the velocity to max speed in the direction of input
            Vector3 targetVelocity = inputDirection.normalized * maxSpeed;

            // Apply the target velocity directly, with drag applied for slowing down
            Vector3 velocityChange = targetVelocity - new Vector3(rb.velocity.x, 0, rb.velocity.z);
            velocityChange -= velocityChange.normalized * drag * Time.fixedDeltaTime;

            rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);

            // Debug Rigidbody Velocity
            Debug.Log("Rigidbody Velocity: " + rb.velocity);
        }

        if (jumping)
        {
            // Debug Jump
            Debug.Log("Jumping!");

            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumping = false;
        }
    }

    private void MoveInAir()
    {
        // We apply air control, but we do not need walking here. This allows for some control mid-air.
        Vector3 targetVelocity = inputDirection.normalized * maxSpeed;
        Vector3 velocityChange = targetVelocity - new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);

        // Debug Air Movement
        Debug.Log("Air Movement Velocity: " + rb.velocity);
    }
}
