using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class CardPreview : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerExitHandler
{
    public HandCard SelectedCard { get; private set; }

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image image;

    private const float animationDuration = 0.2f;
    private const float sizedUpImageScale = 1.5f;
    private static UnityEvent<CardData, HandCard> OnShowCardFromPreview = new();

    private void OnEnable()
    {
        OnShowCardFromPreview.AddListener(ShowCard);
    }

    private void OnDisable()
    {
        OnShowCardFromPreview.RemoveListener(ShowCard);
    }

    private void ShowCard(CardData cardData, HandCard selectedCard)
    {
        image.sprite = cardData.Sprite;
        rectTransform.AlignWithOffset(selectedCard.RectTransform, 0);

        rectTransform.DOKill();
        SelectedCard = selectedCard;
        AnimateSize();

        image.enabled = true;
    }

    private void AnimateSize()
    {
        rectTransform.localScale = Vector3.one;

        rectTransform.DOScale(sizedUpImageScale, animationDuration);
        rectTransform.DOMove(SelectedCard.PreviewPosition.position, animationDuration);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SelectedCard.BeginDrag();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.enabled = false;
        SelectedCard.EndDrag();
        image.raycastTarget = true;
    }

    public static void ChangeCard(CardData cardData, HandCard selectedCard)
    {
        OnShowCardFromPreview.Invoke(cardData, selectedCard);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.dragging) return;

        SelectedCard.EnableImage(true);
        image.enabled = false;
    }
}
