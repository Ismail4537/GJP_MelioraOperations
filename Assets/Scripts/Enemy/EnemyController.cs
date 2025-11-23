using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint;
    private Transform player;

    [Header("Attack Patterns")]
    [SerializeField] private List<MonoBehaviour> attackPatterns;
    private IAttackPattern[] patterns;
    private bool isAttacking = false;
    private bool isStartAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        patterns = attackPatterns.Cast<IAttackPattern>().ToArray();
    }

    void Update()
    {
        if (!isAttacking) return;

        foreach (var p in patterns)
        {
            if(isStartAttacking)
            {
                isStartAttacking = false;
                p.OnAttackStart();
            }
            p.ExecuteAttack(this);
        }
    }

    public void StartAttack()
    {
        isAttacking = true;
        isStartAttacking = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
    }

    public Vector2 DirectionToPlayer()
    {
        if (player == null) return Vector2.right;
        return ((Vector2)(player.position - transform.position)).normalized;
    }
}
