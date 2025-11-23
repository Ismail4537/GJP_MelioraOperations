using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO stats;

    private int score;

    private int demage;

    void Start()
    {
        score = stats.score;
        demage = stats.demage;
    }

    public void Attack()
    {
        Debug.Log("Attack");
    }
}
