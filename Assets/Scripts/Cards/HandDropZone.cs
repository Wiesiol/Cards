using UnityEngine;
using UnityEngine.EventSystems;

public class HandDropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out CardPreview card))
        {
            var selectedCard = card.SelectedCard;

            float closestDistance = float.MaxValue;

            Transform closestSibling = FindClosestSiling(card, closestDistance);
            SetPropperCardPlace(selectedCard, closestSibling);

            selectedCard.transform.localPosition = Vector3.zero;
        }
    }

    private void SetPropperCardPlace(HandCard selectedCard, Transform closestSibling)
    {
        if (closestSibling != null)
        {
            int siblingIndex = closestSibling.GetSiblingIndex();
            selectedCard.transform.SetParent(transform, true);
            selectedCard.transform.SetSiblingIndex(siblingIndex);
        }
        else
        {
            selectedCard.transform.SetParent(transform, true);
        }
    }

    private Transform FindClosestSiling(CardPreview card, float closestDistance)
    {
        Transform closestSibling = null;

        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                continue;
            }

            float distance = Mathf.Abs(child.position.x - card.transform.position.x);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSibling = child;
            }
        }

        return closestSibling;
    }
}
