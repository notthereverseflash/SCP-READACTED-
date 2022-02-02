using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] public Game _Game;
    private void EndCredits()
    {
        _Game.DisplayBeat(-1);
        _Game._credits.SetActive(false);
        _Game._audioManager.MainTheme();
    }
}
