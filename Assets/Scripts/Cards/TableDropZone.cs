using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TableDropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private Hand hand;
    [SerializeField] private Animator playCardAnimator;
    [SerializeField] private Image animationCardImage;
    [SerializeField] private DiscardPile discardPile;

    private void Awake()
    {
        animationCardImage.color = Color.clear;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!TurnPhaseManager.CanPlayCards) return;

        if (eventData.pointerDrag.TryGetComponent(out CardPreview cardPreview))
        {
            var card = cardPreview.SelectedCard;
            hand.PlayCard(card);
            card.PlayShrinkAnimation();

            discardPile.AddToDiscardPile(card.CardData);

            animationCardImage.sprite = card.CardData.Sprite;
            var animation = card.CardData.playCardAnimation;

            playCardAnimator.Play(animation.name, 0, 0);
        }
    }
}
