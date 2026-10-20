using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private bool canMove = true;

    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float poderSalto = 10f;

    [SerializeField] private float groundAcceleration = 40f;
    [SerializeField] private float airAcceleration = 25f;
    [SerializeField] private float groundDeceleration = 60f;
    [SerializeField] private float airDeceleration = 10f;

    public bool isGrounded { get; private set; }
    private bool jumpRequested;
    private bool doubleJumpEnable;

    private bool bounceRequested;
    private float pendingBounceImpulse;

    private bool knockbackRequest;
    private Vector2 pendingKnockback;

    private float moveInput;

    public Vector2 velocity => rigidbody.linearVelocity;

    // Lifecycle methods
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            ApplyHorizontalMovement(moveInput);
            UpdateFacing(moveInput);
        }

        ApplyPendingAction();

    }

    // Public methods
    public void SetMoveInput(float horizontal) => moveInput = Mathf.Clamp(horizontal, -1f, 1f);
    public void SetMovementAllowed(bool allowed) => canMove = allowed;
    public void SetIsGrounded(bool value) => isGrounded = value;

    public void RequestJump()
    {
        if (!canMove) return;

        if (isGrounded)
        {
            jumpRequested = true;
            doubleJumpEnable = true;
        }
        else if (doubleJumpEnable)
        {
            jumpRequested = true;
            doubleJumpEnable = false;
        }
    }

    public void RequestKnockback(Vector2 force)
    {
        knockbackRequest = true;
        pendingKnockback = force;
    }

    public void RequestBounce(float impulse)
    {
        bounceRequested = true;
        pendingBounceImpulse = impulse;
    }

    public void ResetMotion()
    {
        moveInput = 0f;
        ClearPendingActions();
        rigidbody.linearVelocity = Vector2.zero;
    }

    // Private methods
    private void ApplyPendingAction()
    {
        if (bounceRequested)
        {
            ApplyVerticalImpulse(pendingBounceImpulse);
            ClearPendingActions();
            return;
        }

        if (knockbackRequest)
        {
            ApplyKnockback();
            ClearPendingActions();
            return;
        }

        if (jumpRequested && doubleJumpEnable) // First jump
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, 0);
            rigidbody.AddForce(Vector2.up * poderSalto, ForceMode2D.Impulse);
            ClearPendingActions();
            return;
        }

        if (jumpRequested)
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, 0);
            rigidbody.AddForce(.75f * poderSalto * Vector2.up, ForceMode2D.Impulse);
            ClearPendingActions();
            return;
        }
    }

    private void ApplyKnockback()
    {
        rigidbody.linearVelocity = Vector2.zero;
        rigidbody.AddForce(pendingKnockback, ForceMode2D.Impulse);
        canMove = false;
    }

    private void ApplyHorizontalMovement(float horizontal)
    {
        float targetSpeed = horizontal * maxSpeed;
        bool hasInput = Mathf.Abs(horizontal) > 0.01f;

        float rate = hasInput
            ? (isGrounded ? groundAcceleration : airAcceleration)
            : (isGrounded ? groundDeceleration : airDeceleration);

        Vector2 currentVelocity = rigidbody.linearVelocity;
        currentVelocity.x = Mathf.MoveTowards(
            currentVelocity.x,
            targetSpeed,
            rate * Time.fixedDeltaTime);


        rigidbody.linearVelocity = currentVelocity;
    }

    private void ApplyVerticalImpulse(float impulse)
    {
        Vector2 velocity = rigidbody.linearVelocity;
        velocity.y = 0f;
        rigidbody.linearVelocity = velocity;

        rigidbody.AddForce(
            Vector2.up * impulse,
            ForceMode2D.Impulse);
    }

    private void ClearPendingActions()
    {
        bounceRequested = false;
        pendingBounceImpulse = 0f;

        knockbackRequest = false;
        pendingKnockback = Vector2.zero;

        jumpRequested = false;
    }

    private void UpdateFacing(float horizontal)
    {
        if (horizontal > 0.1f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        if (horizontal < -0.1f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}