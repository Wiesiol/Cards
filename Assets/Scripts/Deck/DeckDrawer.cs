using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckDrawer : MonoBehaviour
{
    [SerializeField] private List<CardData> cards = new();
    [SerializeField] private Hand hand;
    [SerializeField] private Deck currentDeck;

    private void Awake()
    {
        cards.AddRange(currentDeck.Cards);
        Shuffle();
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            CardData temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public void DrawCards()
    {
        StartCoroutine(DrawCardsCoroutine());
    }

    private IEnumerator DrawCardsCoroutine()
    {
        while (TryDrawCard())
        {
            yield return new WaitForSeconds(0.2f);
        }
    }

    public bool TryDrawCard()
    {
        if (hand.IsHandFull)
        {
            Debug.Log("Hand is Full");
            return false;
        }

        if (cards.Count == 0)
        {
            Debug.Log("Your deck is empty!");
            return false;
        }

        CardData drawnCard = cards[0];
        hand.AddCard(drawnCard);
        cards.RemoveAt(0);
        return true;
    }
}
