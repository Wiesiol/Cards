using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDummy : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    private float originalWidth;

    private void Start()
    {
        originalWidth = rectTransform.rect.width;
        rectTransform.sizeDelta = new Vector2(0, rectTransform.rect.height);
    }

    public void ShrinkSlot(int siblingIndex, float animationTime)
    {
        rectTransform.SetSiblingIndex(siblingIndex);

        rectTransform.sizeDelta = new(originalWidth, rectTransform.rect.height);
        rectTransform.DOSizeDelta(new Vector2(0, rectTransform.sizeDelta.y), animationTime);
    }
}
