using UnityEngine;

public abstract class EntityState
{
    protected StateMachine stateMachine;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
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
}
