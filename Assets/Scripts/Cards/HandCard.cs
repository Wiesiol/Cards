using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardData CardData {  get; private set; }
    [field: SerializeField] public Transform PreviewPosition { get; private set; }
    [field: SerializeField] public RectTransform RectTransform { get; private set; }
    [SerializeField] private Image image;
    [SerializeField] private RectTransform layoutGroup;
    [SerializeField] private CardDummy cardDummy;

    private int siblingIndex;
    private Transform originalParent;
    private float originalWidth;
    private static UnityEvent<bool> OnSetRaycastTarget = new();
    private const float animationDuration = 0.2f;

    private void Awake()
    {
        originalWidth = RectTransform.rect.width;
        originalParent = transform.parent;
    }

    private void OnEnable()
    {
        OnSetRaycastTarget.AddListener(EnableRaycastTarget);
    }

    public void SetNewCardData(CardData data)
    {
        CardData = data;
        image.sprite = CardData.Sprite;
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        OnSetRaycastTarget.RemoveListener(EnableRaycastTarget);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging) return;

        CardPreview.ChangeCard(CardData, this);
        EnableImage(false);
    }

    public void BeginDrag()
    {
        EnableImage(false);
        siblingIndex = transform.GetSiblingIndex();
    }

    public void EndDrag()
    {
        EnableImage(true);

        if (transform.parent == originalParent)
        {
            if (transform.GetSiblingIndex() != siblingIndex)
            {
                PlayShrinkAnimation();
                AnimateCardAppearance();
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup);
    }

    public void PlayShrinkAnimation()
    {
        cardDummy.ShrinkSlot(siblingIndex, animationDuration);
    }

    public void AnimateCardAppearance()
    {
        RectTransform.sizeDelta = new Vector2(0, RectTransform.sizeDelta.y);

        OnSetRaycastTarget.Invoke(false);
        RectTransform.DOSizeDelta(new Vector2(originalWidth, RectTransform.sizeDelta.y), animationDuration).OnUpdate(() => {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup);
        })
            .OnComplete(()=> OnSetRaycastTarget.Invoke(true));
    }

    public void EnableImage(bool status)
    {
        image.enabled = status;
    }

    public void EnableRaycastTarget(bool status)
    {
        image.raycastTarget = status;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.dragging) 
            return;

        EnableImage(true);
    }
}
