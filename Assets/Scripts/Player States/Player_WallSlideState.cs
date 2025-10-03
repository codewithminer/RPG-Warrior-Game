using UnityEngine;

public class Player_WallSlideState : EntityState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();
        if (!player.wallDetected)
            stateMachine.ChangeState(player.fallState);
        if (player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState);
                player.Flip();
            }
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0) // played pressed <S>
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);
        else
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallSlideSlowMultiplier);
    }

}
