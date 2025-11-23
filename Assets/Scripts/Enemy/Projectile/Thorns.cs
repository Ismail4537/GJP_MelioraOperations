using UnityEngine;

public class Thorns : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    [SerializeField] private float lifeTime = 2f;

    private EnemyStatsSO stats;

    private Vector2 direction;

    private Transform player;

    private Rigidbody2D rb;

    private EnemyAttack enemyAttack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Initialize(Vector2 dir, EnemyAttack attacker, EnemyStatsSO enemyStats)
    {
        stats = enemyStats;
        direction = dir.normalized;
        enemyAttack = attacker;
        Destroy(gameObject, lifeTime);

        if(stats.enemyCategory == "G")
        {
            rb.linearVelocity = direction * speed; 

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    private void FixedUpdate()
    {
        LockTarget();
    }

    public void LockTarget()
    {
        if (player == null || stats.enemyCategory == "G") return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(enemyAttack != null) enemyAttack.Attack(); 
            
            Destroy(gameObject);
        }
    }
}
