using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsSO", menuName = "EnemyStats/EnemyStatsNode")]
public class EnemyStatsSO : ScriptableObject
{
    [Header("HP")]
    public int maxHealth = 50;

    [Header("Attack Settings")]
    public int demage = 2;

    public int cooldown = 1;

    public float attackRange = 5;

    [Header("Speed")]
    public float speed = 2;
}
