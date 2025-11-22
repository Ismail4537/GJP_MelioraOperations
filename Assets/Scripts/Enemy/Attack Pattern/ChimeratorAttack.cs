using UnityEngine;

public class ChimeratorAttack : MonoBehaviour, IAttackPattern {
    [SerializeField] private EnemyStatsSO stats;

    [SerializeField] private float speedAttack = 2;

    private EnemyAttack enemyAttack;
    
    private float cooldown;

    private float currentCooldown;

    private float timer;

    private Transform player;

    private EnemyController enemyController;

    private Rigidbody2D rb;

    private bool once;

    private Transform parent;

    private bool isChase = true;

    private void Start()
    {
        parent = transform.parent;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAttack = parent.Find("Scripts").GetComponent<EnemyAttack>();
        cooldown = stats.cooldown;
        once = true;
        rb = parent.GetComponent<Rigidbody2D>();
    }
    
    private void Update() {
        if (isChase)
            Chase();
    }
    
    public void OnAttackStart()
    {
        if(once)
        {
            once = false;
            timer = 0f;
            currentCooldown = cooldown;
        }
    }

    public void ExecuteAttack(EnemyController enemy)
    {
        timer += Time.deltaTime;

        if (timer >= currentCooldown)
        {
            isChase = false;

            timer = 0f;

            currentCooldown = cooldown;

            DashTowardPlayer(enemy);
        }
    }

    private void DashTowardPlayer(EnemyController enemy)
    {
        Debug.Log("haha");

        Vector2 dir = enemy.DirectionToPlayer();

        float dashSpeed = speedAttack * 5f;
        rb.linearVelocity = dir * dashSpeed;

        Invoke(nameof(StopDash), 1f);
    }

    private void StopDash()
    {
        rb.linearVelocity = Vector2.zero;

        isChase = true;
    }

    void Chase()
    {
        Vector2 direction = (player.position - parent.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        parent.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyAttack.Attack();

            Invoke(nameof(StopDash), 0.5f);
        }
    }
}