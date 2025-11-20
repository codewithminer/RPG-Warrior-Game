using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/skill Data", fileName = "Skill data -")]
public class Skill_DataSO : ScriptableObject
{
    public int cost;
    public SkillType skillType;
    public SkillUpgradeType upgradeType;

    [Header("Skill discription")]
    public string displayName;
    [TextArea]
    public string description;
    public Sprite icon;
}
