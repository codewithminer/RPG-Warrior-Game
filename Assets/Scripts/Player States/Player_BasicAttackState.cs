using System.Threading;
using UnityEngine;

public class Player_BasicAttackState : EntityState
{

    private float attackVelocityTimer;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GenerateAttackVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        if (attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }

    private void GenerateAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(player.attackVelocity.x * player.facingDir, rb.linearVelocity.y);
    }
}
