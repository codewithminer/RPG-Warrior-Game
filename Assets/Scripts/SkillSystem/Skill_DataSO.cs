using System;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/skill Data", fileName = "Skill data -")]
public class Skill_DataSO : ScriptableObject
{
    public int cost;
    public SkillType skillType;
    public UpgradeData upgradeData;

    [Header("Skill discription")]
    public string displayName;
    [TextArea]
    public string description;
    public Sprite icon;
}

[Serializable]
public class UpgradeData
{
    public SkillUpgradeType upgradeType;
    public float cooldown;
}
