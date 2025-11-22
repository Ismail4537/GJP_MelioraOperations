using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    int maxHealth = 3;
    int currentHealth;
    float baseMoveSpeed = 1f;
    float additionalMoveSpeed = 0f;
    float baseAttackDamage = 10f;
    float additionalAttackDamage = 0f;
    public enum WeaponType
    {
        Laser,
        Machinegun,
        RocketLauncher
    }
    public WeaponType currentWeapon = WeaponType.Machinegun;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject rocketPrefab;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Getter & Setter
    public float GetCooldown()
    {
        if (currentWeapon == WeaponType.Laser)
        {
            return 0.2f;
        }
        else if (currentWeapon == WeaponType.Machinegun)
        {
            return 0.3f;
        }
        else if (currentWeapon == WeaponType.RocketLauncher)
        {
            return 0.8f;
        }
        return 0.3f;
    }
    public GameObject GetBulletPrefab()
    {
        if (currentWeapon == WeaponType.RocketLauncher)
        {
            return rocketPrefab;
        }
        return bulletPrefab;
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetMoveSpeed()
    {
        return baseMoveSpeed + additionalMoveSpeed;
    }
    public float GetAttackDamage()
    {
        return baseAttackDamage + additionalAttackDamage;
    }
    public WeaponType GetWeapon()
    {
        return currentWeapon;
    }

    public void SetAdditionalMoveSpeed(float amount)
    {
        additionalMoveSpeed = amount;
    }
    public void SetAdditionalAttackDamage(float amount)
    {
        additionalAttackDamage = amount;
    }
    public void AddAdditionalMoveSpeed(float amount)
    {
        additionalMoveSpeed += amount;
    }
    public void AddAdditionalAttackDamage(float amount)
    {
        additionalAttackDamage += amount;
    }
    public void ResetAdditionalStats()
    {
        additionalMoveSpeed = 0f;
        additionalAttackDamage = 0f;
    }

    void Die()
    {
        // Handle player death (e.g., respawn, game over, etc.)
        Debug.Log("Player has died.");
    }
}