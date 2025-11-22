using UnityEditor;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private EnemyStatsSO stats;

    private float attackRange;

    private EnemyController controller;

    private Rigidbody2D rb;

    private bool playerDetected = false;

    private void Start()
    {
        attackRange = stats.attackRange;
        
        Transform parent = transform.parent;

        rb = parent.GetComponent<Rigidbody2D>();
        
        controller = parent.Find("Scripts").GetComponent<EnemyController>();
    }

    private void Update() {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        bool isSeeingPlayer = hits.Length > 0;
        
        if (isSeeingPlayer && stats.enemyName != "Chimerator")
            rb.linearVelocity = Vector2.zero;

        if (isSeeingPlayer && !playerDetected)
        {
            playerDetected = true;
            controller.StartAttack();
        }
        else if (!isSeeingPlayer && playerDetected)
        {
            playerDetected = false;
            controller.StopAttack();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
