using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class SCP066 : SCPScript
{
    bool[] choiceMenus = new bool[5];
    private int _currentBeat;
    private bool _startingBeatExecuted = false;

    private void Start()
    {
        choiceMenus.Select(value => false);
    }

    public override void UpdateCurrentBeat(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {
        _currentBeat = _currentBeatID;

        if (_currentBeat >= 26 && _currentBeat <= 50)
        {
            _game._canAccessEscapeButton = true;
        }
        else _game._canAccessEscapeButton = false;

        if (!_startingBeatExecuted) //Remove options that require scissors at start and set base scores to +1
        {
            int[] beatsAdd = { 33, 42, 43, 45 };
            _game.DontDisplayTheseBeats(beatsAdd.ToList());
            Score.Deaths += 1;
            Score.Incorrect_SCP += 1;
            Score.Incorrect_Containment += 1;
            Score.Died = true;
            _startingBeatExecuted = true;
        }

        if (_currentBeatID == 30) //remove option to pick up scissors again, and enable options that use scissors
        {
            int[] beatsAdd = { 30 };
            int[] beatsRemove = { 33, 45 };
            _game.DontDisplayTheseBeats(beatsAdd.ToList());
            _game.DontDisplayTheseBeats(beatsRemove.ToList(), false);
        }

        if (_currentBeatID == 37 || _currentBeatID == 31) //update the option to bring it back to the box to compliant
        {
            int[] beatsAdd = { 38 };
            int[] beatsRemove = { 42, 43 };
            _game.DontDisplayTheseBeats(beatsAdd.ToList());
            _game.DontDisplayTheseBeats(beatsRemove.ToList(), false);
        }

        if (_currentBeatID == 26 ) //Remove text that has already been shown
        {
            if (choiceMenus[0]) _game._displayTextOnlyOnce.Add(_currentBeatID);
            _game._displayTextOnlyOnce = _game._displayTextOnlyOnce.Distinct().ToList();
            choiceMenus[0] = true;
        }

        if (_currentBeatID == 31) //Remove text that has already been shown
        {
            if (choiceMenus[1]) _game._displayTextOnlyOnce.Add(_currentBeatID);
            _game._displayTextOnlyOnce = _game._displayTextOnlyOnce.Distinct().ToList();
            choiceMenus[1] = true;
        }

        if (_currentBeatID == 35) //Remove text that has already been shown
        {
            if (choiceMenus[2]) _game._displayTextOnlyOnce.Add(_currentBeatID);
            _game._displayTextOnlyOnce = _game._displayTextOnlyOnce.Distinct().ToList();
            choiceMenus[2] = true;
        }

        if (_currentBeatID == 36) //Remove text that has already been shown
        {
            if (choiceMenus[1]) _game._displayTextOnlyOnce.Add(_currentBeatID);
            _game._displayTextOnlyOnce = _game._displayTextOnlyOnce.Distinct().ToList();
            choiceMenus[1] = true;
        }

        if (_currentBeatID == 43) //Remove text that has already been shown
        {
            if (choiceMenus[3]) _game._displayTextOnlyOnce.Add(_currentBeatID);
            _game._displayTextOnlyOnce = _game._displayTextOnlyOnce.Distinct().ToList();
            choiceMenus[3] = true;
        }

        if (_currentBeatID == 49) //Remove text that has already been shown
        {
            if (choiceMenus[4]) _game._displayTextOnlyOnce.Add(_currentBeatID);
            _game._displayTextOnlyOnce = _game._displayTextOnlyOnce.Distinct().ToList();
            choiceMenus[4] = true;
        }

        if (_currentBeatID == -6) //Encounter Survived
        {
            Score.Deaths = 0;
            Score.Died = false;
        }
    }
}


