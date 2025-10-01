using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim {get; private set; }
    
    private PlayerInputSet input;
    private StateMachine stateMachine;

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    public Vector2 moveInput { get; private set; }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        input = new PlayerInputSet();
        stateMachine = new StateMachine();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
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
        stateMachine.UpdateActiveState();
    }

}
