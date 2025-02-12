using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/Cards/Deck")]
public class Deck : ScriptableObject
{
    [field: SerializeField] public List<CardData> Cards { get; private set; }
}