using UnityEngine;

public class SeraphidAttack : MonoBehaviour, IAttackPattern
{
    [SerializeField] private GameObject fireballPrefab;

    [SerializeField] private EnemyStatsSO stats;
    private float cooldown;
    
    private float currentCooldown;

    private float timer;

    private EnemyController enemyController;

    private bool once;

    private void Start() {
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

        if (timer >= currentCooldown)
        {
            timer = 0f;

            currentCooldown = cooldown;

            var fireball = Instantiate(fireballPrefab, enemy.attackPoint.position, Quaternion.identity);
            fireball.GetComponent<Fireball>().Initialize(enemy.DirectionToPlayer(), enemy.GetComponentInChildren<EnemyAttack>());
        }
    }
}
