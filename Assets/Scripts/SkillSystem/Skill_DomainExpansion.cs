using UnityEngine;

public class Skill_DomainExpansion : Skill_Base
{
    [SerializeField] private GameObject domainPrefab;

    [Header("Slowing Slow Upgrade")]
    [SerializeField] private float slowDownPercent = .8f;
    [SerializeField] private float slowDownDomainDuration = 5;

    [Header("Slowing Casting Upgrade")]
    [SerializeField] private float spellCastingDomainSlowDown = 1;
    [SerializeField] private float spellCastingDomainDuration = 8;

    [Header("Domain details")]
    public float maxDomainSize = 10;
    public float expandSpeed = 3;

    public float GetDomainDuration()
    {
        if (upgradeType == SkillUpgradeType.Domain_SlowingDown)
            return slowDownDomainDuration;
        else
            return spellCastingDomainDuration;
    }

    public float GetSlowPrecentage()
    {
        if (upgradeType == SkillUpgradeType.Domain_SlowingDown)
            return slowDownPercent;
        else
            return spellCastingDomainSlowDown;
    }

    public bool InstantDomain()
    {
        return upgradeType != SkillUpgradeType.Domain_EchoSpam
            && upgradeType != SkillUpgradeType.Domain_ShardSpam;
    }

    public void CreateDomain()
    {
        GameObject domain = Instantiate(domainPrefab, transform.position, Quaternion.identity);
        domain.GetComponent<SkillObject_DomainExpansion>().SetupDomain(this);
    }
}
