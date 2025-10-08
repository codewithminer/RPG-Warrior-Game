using System.Threading;
using UnityEngine;

public class Player_BasicAttackState : EntityState
{

    private float attackVelocityTimer;
    private float lastTimeAttacked;

    private bool comboAttackQueued;
    private int attackDir;
    private const int FirstComboIndex = 1;  // we start combo index with number 1, this parameter is used in the Animator.
    private int comboIndex = 1;
    private int comboLimit = 3;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
        if (comboLimit != player.attackVelocity.Length)
        {
            Debug.LogWarning("Adjusted combo limit to match attack velocity array!");
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndexIfNeeded();

        attackDir = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDir;

        anim.SetInteger("basicAttackIndex", comboIndex);

        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (input.Player.Attack.WasPressedThisFrame())
            QueueNextAttack();

        if (triggerCalled)
            HandleStateExit();
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        stateMachine.ChangeState(player.idleState);        
    }

    public override void Exit()
    {
        base.Exit();
        lastTimeAttacked = Time.time;
        comboIndex++;
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
            comboAttackQueued = true;
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
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
    }

    private void ResetComboIndexIfNeeded()
    {
        if (Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;
        if (comboIndex > comboLimit)
                comboIndex = FirstComboIndex;
    }
}
