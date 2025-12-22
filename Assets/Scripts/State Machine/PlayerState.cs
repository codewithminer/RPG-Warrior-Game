using UnityEngine;

public abstract class PlayerState : EntityState
{
    /*
    The base class for all states.
    It containing shared functionality
    */

    protected Player player;
    protected PlayerInputSet input;
    protected Player_SkillManager skillManager;

    public PlayerState(Player player, StateMachine stateMachine, string animBoolName):base(stateMachine, animBoolName)
    {
        this.player = player;

        anim = player.anim;
        rb = player.rb;
        stats = player.stats;
        input = player.input;
        skillManager = player.skillManager;
    }

    public override void Update()
    {
        base.Update();

        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            skillManager.dash.SetSkillOnCooldown();
            stateMachine.ChangeState(player.dashState);
        }

        if (input.Player.UltimateSpell.WasPressedThisFrame() && skillManager.domainExpansion.CanUseSkill())
        {
            if (skillManager.domainExpansion.InstantDomain())
                skillManager.domainExpansion.CreateDomain();
            else
                stateMachine.ChangeState(player.domainExpensionState);
            
            skillManager.domainExpansion.SetSkillOnCooldown();
        }
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private bool CanDash()
    {
        if(!skillManager.dash.CanUseSkill())
            return false;
        if (player.wallDetected)
            return false;
        if (stateMachine.currentState == player.dashState || stateMachine.currentState == player.domainExpensionState)
            return false;
        return true;
    }
}
