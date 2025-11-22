using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject cardSelectionUI;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject cardPositionOne;
    [SerializeField] GameObject cardPositionTwo;
    [SerializeField] GameObject cardPositionThree;
    [SerializeField] List<CardSO> deck;

    GameObject cardOne, cardTwo, cardThree;

    List<CardSO> alreadySelectedCards = new List<CardSO>();
    void randomizeNewCards()
    {
        if(cardOne != null) Destroy(cardOne);
        if(cardTwo != null) Destroy(cardTwo);
        if(cardThree != null) Destroy(cardThree);

        List<CardSO> randomizeCards = new List<CardSO>();

        List<CardSO> availableCards = new List<CardSO>();
    }

    void RandomizeNewCards()
    {

        if (cardOne != null) Destroy(cardOne);
        if (cardTwo != null) Destroy(cardTwo);
        if (cardThree != null) Destroy(cardThree);

        List<CardSO> randomizedCards = new List<CardSO>();
        List<CardSO> availableCards = new List<CardSO>(deck);

        if (availableCards.Count < 3)
        {
            Debug.Log("Not enough available cards");
            return;
        }

        while (randomizedCards.Count < 3)
        {
            CardSO randomCard = availableCards[Random.Range(0, availableCards.Count)];

            if (!alreadySelectedCards.Contains(randomCard) &&
                !randomizedCards.Contains(randomCard))
            {
                randomizedCards.Add(randomCard);
            }
        }

        cardOne = InstantiateCard(randomizedCards[0], cardPositionOne);
        cardTwo = InstantiateCard(randomizedCards[1], cardPositionTwo);
        cardThree = InstantiateCard(randomizedCards[2], cardPositionThree);
    }

    GameObject InstantiateCard(CardSO cardSO, GameObject position)
    {
        GameObject cardGo = Instantiate(cardPrefab, position.position, Quaternion.identity, position);
        Card card = cardGo.GetComponent<Card>();
        card.Setup(cardSO);
        return cardGo;
    }
}

