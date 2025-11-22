using UnityEngine;

public class ChronoAttack : MonoBehaviour, IAttackPattern
{
    [SerializeField] private GameObject needlePrefab;

    [SerializeField] private EnemyStatsSO stats;

    private float cooldown;

    private float currentCooldown;

    private float timer;

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

            var needle = Instantiate(needlePrefab, enemy.attackPoint.position, Quaternion.identity);
            needle.GetComponent<Needle>().Initialize(enemy.DirectionToPlayer(), enemy.GetComponentInChildren<EnemyAttack>());
        }
    }
}
