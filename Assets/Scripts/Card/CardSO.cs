using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public Sprite cardImage;
    public string cardText;
    public CardEffect effectType;
    public int effectValue; 
    public int unlockLevel;
}

public enum CardEffect
{
    AtkUp,
    HpUp,
    SpeedUp,
    Burn,
    Freeze,
    WeaponMissile,
    WeaponLaser,
    WeaponMachineGun,
    LongerDash
}

