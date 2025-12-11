using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Skill_Shard : Skill_Base
{
    private SkillObject_Shard currentShard;
    private Entity_Health playerHealth;

    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private float detonateTime = 2;

    [Header("Moving shard upgrade")]
    [SerializeField] private float shardSpeed = 7;

    [Header("MultiCast shard upgrade")]
    [SerializeField] private int maxCharges = 3;
    [SerializeField] private int currentCharges;
    [SerializeField] private bool isRecharging;

    [Header("Teleport shard upgrade")]
    [SerializeField] private float shardExistDuration = 10;

    [Header("Health Rewind Shard Upgrade")]
    [SerializeField] private float saveHealthPercent;

    protected override void Awake()
    {
        base.Awake();
        currentCharges = maxCharges;
        playerHealth = GetComponentInParent<Entity_Health>();
    }

    public override void TryUseSkill()
    {
        if (!CanUseSkill())
            return;
        
        if (Unlocked(SkillUpgradeType.Shard))
            HandleShardRegular();

        if (Unlocked(SkillUpgradeType.Shard_MoveToEnemy))
            HandleShardMoving();

        if (Unlocked(SkillUpgradeType.Shard_Multicast))
            HandleShardMultiCast();

        if (Unlocked(SkillUpgradeType.Shard_Teleport))
            HandleShardTeleport();

        if (Unlocked(SkillUpgradeType.Shard_TeleportHpRewind))
            HandleShardHealthRewind();
    }

    private void HandleShardHealthRewind()
    {
        Debug.Log("1");
        if (currentShard == null)
        {
            Debug.Log("2");
            CreateShard();
            saveHealthPercent = playerHealth.GetHealthPercent();
        }
        else
        {
            Debug.Log("3");
            SwapPlayerAndShard();
            playerHealth.SetHealthToPercent(saveHealthPercent);
            SetSkillOnCooldown();
        }        
    }

    private void HandleShardTeleport()
    {
        if (currentShard == null)
        {
            CreateShard();
        }
        else
        {
            SwapPlayerAndShard();
            SetSkillOnCooldown();
        }
    }

    private void SwapPlayerAndShard()
    {
        Vector3 shardPosition = currentShard.transform.position;
        Vector3 playerPosition = player.transform.position;

        currentShard.transform.position = playerPosition;
        currentShard.Explode();

        player.TeleportPlayer(shardPosition);
    }

    private void HandleShardMultiCast()
    {
        if (currentCharges <= 0)
            return;
        
        CreateShard();
        currentShard.MoveTowardsClosestTarget(shardSpeed);
        currentCharges--;

        if (!isRecharging)
            StartCoroutine(ShardRechargeCo());
    }

    private IEnumerator ShardRechargeCo()
    {
        isRecharging = true;
        while (currentCharges < maxCharges)
        {
            yield return new WaitForSeconds(cooldown);
            currentCharges++;
        }

        isRecharging = false;
    }

    private void HandleShardMoving()
    {
        CreateShard();
        currentShard.MoveTowardsClosestTarget(shardSpeed);
        SetSkillOnCooldown();
    }

    private void HandleShardRegular()
    {
        CreateShard();
        SetSkillOnCooldown();
    }

    public void CreateShard()
    { 
        float detonateTime = GetDenotateTime();

        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        currentShard = shard.GetComponent<SkillObject_Shard>();
        currentShard.SetupShard(detonateTime);

        if (Unlocked(SkillUpgradeType.Shard_Teleport) || Unlocked(SkillUpgradeType.Shard_TeleportHpRewind))
            currentShard.OnExplode += ForceCooldown;
    }

    public float GetDenotateTime()
    {
        if (Unlocked(SkillUpgradeType.Shard_Teleport) || Unlocked(SkillUpgradeType.Shard_TeleportHpRewind))
            return shardExistDuration;
        return detonateTime;
    }

    private void ForceCooldown()
    {
        if (!OnCooldown())
        {
            SetSkillOnCooldown();
            currentShard.OnExplode -= ForceCooldown;
        }
    }
}
