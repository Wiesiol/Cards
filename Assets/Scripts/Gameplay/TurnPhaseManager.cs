using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPhaseManager : MonoBehaviour
{
    [SerializeField] private GameObject turnButtons;
    [SerializeField] private GameObject drawButtons;
    public static bool CanPlayCards = false;

    public void StartTurn()
    {
        drawButtons.SetActive(false);
        turnButtons.SetActive(true);
        CanPlayCards = true;
    }

    public void EndTurn()
    {
        drawButtons.SetActive(true);
        turnButtons.SetActive(false);
        CanPlayCards = false;
    }
}
