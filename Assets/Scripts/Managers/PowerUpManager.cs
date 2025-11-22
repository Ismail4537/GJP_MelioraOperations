using UnityEngine;

using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

    // track level power up
    private Dictionary<CardEffect, int> levels = new Dictionary<CardEffect, int>();

    private void Awake()
    {
        Instance = this;

        // semua effect 0 di awal
        foreach (CardEffect e in System.Enum.GetValues(typeof(CardEffect)))
            levels[e] = 0;
    }

    public bool CanUpgrade(CardSO card)
    {
        return levels[card.effectType] < card.maxLevel;
    }

    public void ApplyCard(CardSO card)
    {
        if (!CanUpgrade(card))
        {
            Debug.Log("Tidak bisa ditingkatkan, level tertinggi telah tercapai untuk " + card.cardName);
            return;
        }

        int currentLevel = levels[card.effectType];

        // apply effect
        switch (card.effectType)
        {
            case CardEffect.AtkUp:
                PlayerStats.Instance.AddAttack(card.baseValue);
                break;

            case CardEffect.HpUp:
                PlayerStats.Instance.AddHP(card.baseValue);
                break;

            case CardEffect.SpeedUp:
                PlayerStats.Instance.AddSpeed(card.baseValue);
                break;

            case CardEffect.Burn:
                BulletEffects.Instance.SetBurnLevel(currentLevel + 1);
                break;

            case CardEffect.Freeze:
                BulletEffects.Instance.SetFreezeLevel(currentLevel + 1);
                break;

            case CardEffect.WeaponMissile:
                WeaponManager.Instance.UpgradeMissile(currentLevel + 1);
                break;

            case CardEffect.WeaponLaser:
                WeaponManager.Instance.UpgradeLaser(currentLevel + 1);
                break;

            case CardEffect.WeaponMachineGun:
                WeaponManager.Instance.UpgradeMachineGun(currentLevel + 1);
                break;

            case CardEffect.LongerDash:
                PlayerStats.Instance.UpgradeDash(currentLevel + 1);
                break;
        }

        // tambahin levelnya
        levels[card.effectType]++;
    }

    public int GetLevel(CardEffect effect)
    {
        return levels[effect];
    }
}
