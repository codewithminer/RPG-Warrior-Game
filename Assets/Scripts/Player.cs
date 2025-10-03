using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public PlayerInputSet input { get; private set; }
    private StateMachine stateMachine;

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }


    [Header("Movement details")]
    public Vector2 moveInput { get; private set; }
    [Range(0,1)]
    public float inAirMoveMultiplier = 0.8f; // should be from 0 to 1
    [Range(0, 1)]
    public float wallSlideSlowMultiplier = 0.3f;
    public float moveSpeed = 7f;
    public float jumpForce = 5f;
    private bool isFacingRight = true;
    private int facingDir = 1;

    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance = 1.4f;
    [SerializeField] private float wallCheckDistance = .44f;
    [SerializeField] private LayerMask whatIsGround;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        input = new PlayerInputSet();
        stateMachine = new StateMachine();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
    }

    void OnEnable()
    {
        // this function is called when the game object becomes enabled and active
        // and called before Start() and after Awake()
        input.Enable();

        input.Player.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        input.Player.Movement.canceled += context => moveInput = Vector2.zero;
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Start()
    {
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !isFacingRight)
            Flip();
        else if (xVelocity < 0 && isFacingRight)
            Flip();
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
        facingDir *= -1;
    }

    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir, 0));
    }
}
