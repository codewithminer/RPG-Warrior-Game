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

    Animator anim;

    protected Rigidbody2D rb;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = stateName;

        anim = player.anim;
        rb = player.rb;
    }

    public void Enter()
    {
        // everytime state will be changed, Enter will be called
        anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        // we going to run logic of the state here
       
    }

    public virtual void Exit()
    {
        // Exit will be called, everytime we exit state and change to a new one
        anim.SetBool(animBoolName, false);
    }
}
