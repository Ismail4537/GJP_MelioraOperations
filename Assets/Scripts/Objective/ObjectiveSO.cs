using UnityEngine;

public enum ObjectiveType
{
    KillAmount,
    KillSpecificType,
    SurviveTime,
    NoHit,
    NoDash,
    KillBoss
}

[CreateAssetMenu(menuName = "Objective")]
public class ObjectiveSO : ScriptableObject
{
    public ObjectiveType type;

    public int requiredAmount;          // for kill count
    public EnemyCategory targetCategory; // for category-specific kills

    public string description;
}

