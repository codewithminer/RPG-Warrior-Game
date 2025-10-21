using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;

    [Header("Battle details")]
    public float battleMoveSpeed = 3f;
    public float attackDistance = 2f;
    public float battleTimeDuration = 5f;
    public float minRetreatDistance = 1f;
    public Vector2 retreatVelocity;

    [Header("Movement details")]
    public float idleDuration = 2f;
    public float moveSpeed = 1.4f;
    [Range(0, 2)]
    public float moveAnimSpeedMultiplier = 1f;

    [Header("Player details")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10f;
    public Transform player { get; private set;}

    public void TryEnterBattleState(Transform player)
    {
        if (stateMachine.currentState == battleState || stateMachine.currentState == attackState)
            return;
        this.player = player;
        stateMachine.ChangeState(battleState);
    }

    public Transform GetPlayerRefrence()
    {
        if (player == null)
            player = PlayerDetectedByRaycast().transform;
        return player;
    }

    public RaycastHit2D PlayerDetectedByRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer | whatIsGround );

        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default; // return null

        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            playerCheck.position,
            new Vector3(playerCheck.position.x + (facingDir * playerCheckDistance), playerCheck.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            playerCheck.position,
            new Vector3(playerCheck.position.x + (facingDir * attackDistance), playerCheck.position.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            playerCheck.position,
            new Vector3(playerCheck.position.x + (facingDir * minRetreatDistance), playerCheck.position.y));
    }
}
