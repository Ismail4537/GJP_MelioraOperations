using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class CardSO : ScriptableObject
{
    [Header("Info Kartu")]
    public Sprite cardImage; // icon kartu
    public string cardName; // nama kartu

    [TextArea] 
    public string cardDescription; // deskripsi kartu
    [Header("Effect Settings")]
    public CardEffect effectType; // tipe efek kartu

    public float baseValue;        // base effect kartu
    public int maxLevel = 1;       // level maksimum kartu
}

