using UnityEngine;

public static class RectTransformExtensions
{
    public static void AlignWithOffset(this RectTransform source, RectTransform target, float yOffset)
    {
        Vector2 targetCenter = target.TransformPoint(target.rect.center);

        Vector2 localPosition = source.parent.InverseTransformPoint(targetCenter);

        source.localPosition = new Vector3(localPosition.x, localPosition.y + yOffset, source.localPosition.z);
    }
}
