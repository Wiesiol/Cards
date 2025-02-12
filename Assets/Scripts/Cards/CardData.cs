using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/Cards/Card")]
public class CardData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite;
    [field: SerializeField] public AnimationClip playCardAnimation;
}
