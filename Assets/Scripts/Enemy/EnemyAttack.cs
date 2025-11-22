using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO stats;

    private int demage;

    void Start()
    {
        demage = stats.demage;
    }

    public void Attack()
    {
        Debug.Log("Attack");
    }
}
