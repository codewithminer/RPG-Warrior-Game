using UnityEngine;

public abstract class EntityState
{
    /*
    The base class for all states.
    It containing shared functionality
    */

    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public void Enter()
    {
        // everytime state will be changed, Enter will be called
        Debug.Log("I enter " + stateName);
    }

    public virtual void Update()
    {
        // we going to run logic of the state here
        Debug.Log("I update " + stateName);
    }

    public virtual void Exit()
    {
        // Exit will be called, everytime we exit state and change to a new one
        Debug.Log("I exit " + stateName);
    }
}
