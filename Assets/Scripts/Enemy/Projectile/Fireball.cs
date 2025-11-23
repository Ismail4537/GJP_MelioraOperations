using UnityEngine;

public class Fireball : MonoBehaviour {
    [SerializeField] private float speed = 10f;
    
    [SerializeField] private float lifeTime = 5f;

    private Vector2 direction;

    private Transform player;

    private Rigidbody2D rb;

    private EnemyAttack enemyAttack;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Initialize(Vector2 dir, EnemyAttack attacker)
    {
        direction = dir.normalized;
        enemyAttack = attacker; 
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        LockTarget();
    }

    public void LockTarget()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyAttack.Attack(); 
            Destroy(gameObject);
        }
    }
}