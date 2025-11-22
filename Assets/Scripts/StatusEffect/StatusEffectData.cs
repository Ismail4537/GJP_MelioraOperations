using UnityEngine;
[CreateAssetMenu(fileName = "New Status Effect", menuName = "Status Effect")]
public class StatusEffectData : ScriptableObject
{
    public string nama;
    public float damage;
    public float damageMultiplier;
    public float tickSpeed;
    public float movePenalty;
    public float lifetime;
    public bool immovable;
}