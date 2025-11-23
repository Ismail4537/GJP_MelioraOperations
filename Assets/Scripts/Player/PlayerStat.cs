using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    int baseMaxHealth = 3;
    float baseMoveSpeed = 1f;
    float baseAttackDamage = 10f;
    float baseReloadTime = 3f;
    float baseMagazine = 30f;
    float jumpForce = 50f;

    public int currentHealth;
    public float currentAmunition;

    public float additionaljumpForce = 0f;
    public int additionalMaxHealth = 0;
    public float additionalMoveSpeed = 0f;
    public float additionalMagazine = 0f;
    public float reductionReloadTime = 0f;
    public float additionalAttackDamage = 0f;

    public int weaponLevel = 1;
    public int BurnLevel = 0;
    public int FreezeLevel = 0;
    public enum WeaponType
    {
        Laser,
        Machinegun,
        RocketLauncher,
        Pistol
    }
    WeaponType currentWeapon = WeaponType.Pistol;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject rocketPrefab;
    public StatusEffectData fireEffect;
    public StatusEffectData freezeEffect;
    void Start()
    {
        currentHealth = baseMaxHealth + additionalMaxHealth;
        currentAmunition = baseMagazine + additionalMagazine;
    }

    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, baseMaxHealth + additionalMaxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void UpdateAmunition(float amount)
    {
        currentAmunition += amount;
        currentAmunition = Mathf.Clamp(currentAmunition, 0, GetMagazine());
    }

    public StatusEffectData ApplyLeveledFireEffect()
    {
        if (BurnLevel > 0 && fireEffect != null)
        {
            StatusEffectData leveledFireEffect = ScriptableObject.CreateInstance<StatusEffectData>();
            leveledFireEffect.damage = fireEffect.damage + BurnLevel;
            return leveledFireEffect;
        }
        return null;
    }

    public StatusEffectData ApplyLeveledFreezeEffect()
    {
        if (FreezeLevel > 0 && freezeEffect != null)
        {
            StatusEffectData leveledFreezeEffect = ScriptableObject.CreateInstance<StatusEffectData>();
            leveledFreezeEffect.lifetime = freezeEffect.lifetime + (FreezeLevel * 0.5f);
            return leveledFreezeEffect;
        }
        return null;
    }

    // Getter & Setter
    public float GetCooldown()
    {
        if (currentWeapon == WeaponType.Laser)
        {
            return 0.1f;
        }
        else if (currentWeapon == WeaponType.Machinegun)
        {
            return 0.5f;
        }
        else if (currentWeapon == WeaponType.RocketLauncher)
        {
            return 1f;
        }
        else
        {
            return 0.6f;
        }
    }
    public WeaponType GetWeapon()
    {
        return currentWeapon;
    }
    public GameObject GetBulletPrefab()
    {
        if (currentWeapon == WeaponType.RocketLauncher)
        {
            return rocketPrefab;
        }
        return bulletPrefab;
    }
    public int GetMaxHealth()
    {
        return baseMaxHealth + additionalMaxHealth;
    }
    public float GetMoveSpeed()
    {
        return baseMoveSpeed + additionalMoveSpeed;
    }
    public float GetAttackDamage()
    {
        return baseAttackDamage + additionalAttackDamage;
    }
    public float GetMagazine()
    {
        return baseMagazine + additionalMagazine;
    }
    public float GetReloadTime()
    {
        return Mathf.Max(0.1f, baseReloadTime - reductionReloadTime);
    }
    public float GetJumpForce()
    {
        return jumpForce + additionaljumpForce;
    }
    public void btnSetWeapon(int weaponIndex)
    {
        WeaponType newWeapon = (WeaponType)weaponIndex;
        SetWeapon(newWeapon);
    }
    public void SetWeapon(WeaponType newWeapon)
    {
        if (currentWeapon == newWeapon)
        {
            AddWeaponLevel(1);
        }
        if (WeaponType.Laser == newWeapon)
        {
            baseAttackDamage = 5f;
            baseReloadTime = 3f;
            additionalMagazine = 10 * weaponLevel;
        }
        else if (WeaponType.Machinegun == newWeapon)
        {
            baseAttackDamage = 10f;
            baseReloadTime = 3f;
            additionalMagazine = 0;
        }
        else if (WeaponType.RocketLauncher == newWeapon)
        {
            baseAttackDamage = 30f;
            baseReloadTime = 4f;
            additionalMagazine = 0;
        }
        else
        {
            baseAttackDamage = 10f;
            baseReloadTime = 2f;
        }
        currentWeapon = newWeapon;
    }
    public void AddAdditionalMaxHealth(int amount)
    {
        additionalMaxHealth += amount;
        if (additionalMaxHealth + baseMaxHealth > 10)
        {
            additionalMaxHealth = 10;
        }
    }
    public void AddWeaponLevel(int level)
    {
        weaponLevel += level;
        if (weaponLevel > 3)
        {
            weaponLevel = 3;
        }
    }
    public void AddFireLevel(int level)
    {
        BurnLevel += level;
        if (BurnLevel > 5)
        {
            BurnLevel = 5;
        }
    }
    public void AddFreezeLevel(int level)
    {
        FreezeLevel += level;
        if (FreezeLevel > 5)
        {
            FreezeLevel = 5;
        }
    }
    public void ResetAdditionalStats()
    {
        additionalMoveSpeed = 0f;
        additionalAttackDamage = 0f;
        reductionReloadTime = 0f;
        additionalMagazine = 0f;
        additionalMaxHealth = 0;
        BurnLevel = 0;
        FreezeLevel = 0;
        weaponLevel = 1;
    }

    void Die()
    {
        // Handle player death (e.g., respawn, game over, etc.)
        Debug.Log("Player has died.");
    }
}