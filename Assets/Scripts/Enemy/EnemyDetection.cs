using UnityEditor;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerLayer;

    private Rigidbody2D rb;

    private void Start()
    {
        Debug.Log("Hallo Jawa");
        Transform parent = transform.parent;

        rb = parent.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        if (hits.Length > 0)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
