using UnityEngine;

public class Player_FallState : EntityState
{
    public Player_FallState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();
    }
}
