using UnityEngine;
using System.Collections; 
using System.Collections.Generic;

public class CthulhuAttack : MonoBehaviour, IAttackPattern {
    [SerializeField] private GameObject thornsPrefab;

    [SerializeField] private EnemyStatsSO stats;
    [SerializeField] private List<GameObject> enemies;

    [SerializeField] private LayerMask playerLayer;

    private float attackRange;

    private EnemyAttack enemyAttack;

    private float cooldown;

    private float currentCooldown;

    private float spawnEnemyCooldown = 10;

    private float meeleAttackCooldown = 2;

    private float timer;

    private float timerSpawnEnemy;

    private float timerMeeleAttack;

    private EnemyController enemyController;

    private bool once;

    private void Start() {
        Transform parent = transform.parent;
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
            currentCooldown = 0f;
        }
    }

    public void ExecuteAttack(EnemyController enemy)
    {
        timer += Time.deltaTime;

        timerSpawnEnemy += Time.deltaTime;

        if (timer >= currentCooldown)
        {
            timer = 0f;

            currentCooldown = cooldown;

            StartCoroutine(SpawnThornTwice(enemy));
        }

        if (timerSpawnEnemy >= spawnEnemyCooldown)
        {
            timerSpawnEnemy = 0f;

            Transform t = enemy.transform;

            Vector3 topPos  = t.position - t.up * 2f;
            Vector3 bottomPos = t.position + t.up * 2f;

            if (enemies.Count > 0)
                Instantiate(enemies[0], topPos, Quaternion.identity);

            if (enemies.Count > 1)
                Instantiate(enemies[1], bottomPos, Quaternion.identity);
        }
    }

    private IEnumerator SpawnThornTwice(EnemyController enemy)
    {
        // Thorn pertama
        SpawnThorn(enemy);

        yield return new WaitForSeconds(0.4f);

        // Thorn kedua
        SpawnThorn(enemy);
    }

    private void SpawnThorn(EnemyController enemy)
    {
        var thorn = Instantiate(thornsPrefab, enemy.attackPoint.position, Quaternion.identity);
        thorn.GetComponent<Thorns>().Initialize(
            enemy.DirectionToPlayer(),
            enemy.GetComponentInChildren<EnemyAttack>(),
            stats
        );
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        bool isSeeingPlayer = hits.Length > 0;


        if (isSeeingPlayer && stats.enemyName != "Chimerator")
        {
            timerMeeleAttack += Time.deltaTime;

            if (timerMeeleAttack >= meeleAttackCooldown)
            {
                timerMeeleAttack = 0f;

                enemyAttack.Attack();
            }
        }
            
    }
}