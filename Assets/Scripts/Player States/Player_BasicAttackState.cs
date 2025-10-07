using System.Threading;
using UnityEngine;

public class Player_BasicAttackState : EntityState
{

    private float attackVelocityTimer;

    private const int FirstComboIndex = 1;  // we start combo index with number 1, this parameter is used in the Animator.
    private int comboIndex = 1;
    private int comboLimit = 3;
    private float lastTimeAttacked;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
        if (comboLimit != player.attackVelocity.Length)
            comboLimit = player.attackVelocity.Length;
    }

    public override void Enter()
    {
        base.Enter();

        ResetComboIndexIfNeeded();

        anim.SetInteger("basicAttackIndex", comboIndex);

        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        lastTimeAttacked = Time.time;
        comboIndex++;
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        if (attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * player.facingDir, attackVelocity.y);
    }

    private void ResetComboIndexIfNeeded()
    {
        if (Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;
        if (comboIndex > comboLimit)
                comboIndex = FirstComboIndex;
    }
}
