using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardPile : MonoBehaviour
{
    [SerializeField] private List<CardData> cards;
    [SerializeField] private Image cardImage;

    public void AddToDiscardPile(CardData cardData)
    {
        cardImage.gameObject.SetActive(true);
        cardImage.sprite = cardData.Sprite;
        cards.Add(cardData);
    }
}