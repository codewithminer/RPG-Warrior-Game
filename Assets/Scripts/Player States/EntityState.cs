using UnityEngine;

public abstract class EntityState
{
    /*
    The base class for all states.
    It containing shared functionality
    */

    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator anim;

    protected Rigidbody2D rb;
    protected PlayerInputSet input;

    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = stateName;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    public virtual void Enter()
    {
        // everytime state will be changed, Enter will be called
        anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        // we going to run logic of the state here

        stateTimer -= Time.deltaTime;

        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
            stateMachine.ChangeState(player.dashState);
    }

    public virtual void Exit()
    {
        // Exit will be called, everytime we exit state and change to a new one
        anim.SetBool(animBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }

    private bool CanDash()
    {
        if (player.wallDetected)
            return false;
        if (stateMachine.currentState == player.dashState)
            return false;
        return true;
    }
}
