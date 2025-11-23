using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    
    [SerializeField] private EnemyStatsSO stats;
    
    private int maxHealth;

    void Start()
    {
        maxHealth = stats.maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
