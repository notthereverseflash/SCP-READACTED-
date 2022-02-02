using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SCPScript : MonoBehaviour
{
    public Game _game;

    public abstract void UpdateCurrentBeat(int _currentBeatID, int _startingBeat, ScoreTypes Score);

}
