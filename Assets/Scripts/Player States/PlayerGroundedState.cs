using UnityEngine;

public class PlayerGroundedState : EntityState
{
    public PlayerGroundedState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.jumpState);
    }
}
