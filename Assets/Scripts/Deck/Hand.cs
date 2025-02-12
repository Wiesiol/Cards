using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool IsHandFull {  get; private set; }
    [SerializeField] public List<CardData> cards;
    [SerializeField] private List<HandCard> handCards;

    public void AddCard(CardData card)
    {
        cards.Add(card);
        
        foreach (var handCard in handCards)
        {
            if (!handCard.gameObject.activeSelf)
            {
                handCard.transform.SetSiblingIndex(transform.childCount - 1);
                handCard.SetNewCardData(card);
                handCard.AnimateCardAppearance();
                UpdateHandFullStatus();
                return;
            }
        }
    }

    private void UpdateHandFullStatus()
    {
        IsHandFull = cards.Count >= handCards.Count;
    }

    public void PlayCard(HandCard card)
    {
        card.gameObject.SetActive(false);
        cards.Remove(card.CardData);
        UpdateHandFullStatus();
    }
}