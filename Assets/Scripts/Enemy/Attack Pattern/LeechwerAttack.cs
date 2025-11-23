using UnityEngine;

public class LeechwerAttack : MonoBehaviour, IAttackPattern {
    [SerializeField] private EnemyStatsSO stats;
    private EnemyAttack enemyAttack;

    private float cooldown;

    private float currentCooldown;

    private float timer;

    private bool once;

    private Transform player;

    private void Start() {
        Transform parent = transform.parent;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAttack = parent.Find("Scripts").GetComponent<EnemyAttack>();
        cooldown = stats.cooldown;
        once = true;
    }
    
    public void OnAttackStart()
    {
        if(once)
        {
            once = false;
            timer = 0f;
            currentCooldown = cooldown;

            transform.parent.SetParent(player);
        }
    }

    public void ExecuteAttack(EnemyController enemy)
    {
        timer += Time.deltaTime;

        if (timer >= currentCooldown)
        {
            timer = 0f;

            currentCooldown = cooldown;

            enemyAttack.Attack();

            Destroy(transform.parent.gameObject);
        }
    }
}