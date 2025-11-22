using UnityEngine;

public class VoidstalkerAttack : MonoBehaviour, IAttackPattern
{
    [SerializeField] private EnemyStatsSO stats;

    private EnemyAttack enemyAttack;
    
    private float cooldown;

    private float currentCooldown;

    private float timer;

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

        if (timer >= currentCooldown)
        {
            enemyAttack.Attack();
        }
    }
}
