using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO stats;

    [SerializeField] private LayerMask playerLayer;

    private float attackRange;

    private EnemyController controller;

    private float speed;

    private Transform player;

    private Transform spriteObject;

    private Rigidbody2D rb;

    private Transform parent;

    private bool canMove = true;

    public bool slowMove;

    [SerializeField] private float rotationSpeed = 5f;

    private void Start()
    {
        attackRange = stats.attackRange;
        
        speed = stats.speed;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        parent = transform.parent;

        rb = parent.GetComponent<Rigidbody2D>();

        controller = GetComponent<EnemyController>();

        spriteObject = parent.Find("Sprite");
    }

    private void Update()
    {
        CheckForPlayer();
        Chase();
    }

    void Chase()
    {
        if (player == null) return;

        Vector2 direction = (player.position - parent.position).normalized;

        if (canMove)
            rb.linearVelocity = direction * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (slowMove)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

            parent.rotation = Quaternion.Slerp(parent.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            parent.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        bool isSeeingPlayer = hits.Length > 0;

        if (isSeeingPlayer && stats.enemyName != "Chimerator")
        {
            canMove = false;
            rb.linearVelocity = Vector2.zero;
        }
        else
            canMove = true;
    }

    private void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, player.position);
    }
}
