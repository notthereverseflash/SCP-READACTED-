using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SCP249 : SCPScript
{
    private int _roomNumber = 497;
    private bool _insideDoor = false;
    private int _currentBeat;
    private bool _usedRadio = false;
    private bool _genderSwapped = false;
    private bool _startingBeatExecuted = false;

    private void Update()
    {
        if (_currentBeat >= 221 && _currentBeat <= 230)
        {
            if (Input.GetKey("r"))
            {
                _game.DisplayBeat(600);
            }
        }
        else if (Input.GetKey("r") && _currentBeat > 230 && _currentBeat < 274 && _currentBeat != 600 && _currentBeat != 601)
        {
            _game.DisplayBeat(-28);
            Debug.Log("Test1 " + _currentBeat);
        }

        if (_currentBeat >= 229 && _currentBeat <= 273)
        {
            _game._canAccessEscapeButton = false;
        }
        else _game._canAccessEscapeButton = true;
    }

    public override void UpdateCurrentBeat(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {
        _game._dontDisplayTheseBeats.Remove(600);
        _currentBeat = _currentBeatID;

        if (!_startingBeatExecuted)
        {
            _game.Score.Deaths += 1;
            _game.Score.Incorrect_SCP += 1;
            _game.Score.Incorrect_Containment += 1;
            _game.Score.Died = true;
            _startingBeatExecuted = true;
        }

        if (_currentBeatID == -10) 
        {
            if (_game._beatHistory[_game._beatHistory.Count - 1] == 600)
            {
                if (_usedRadio || _roomNumber == 500)
                {
                    _game.DisplayBeat(-19);
                    _game._beatHistory.RemoveAll(item => item == 600);
                }
                else {
                    if (_insideDoor)
                    {
                        _game.DisplayBeat(-19);
                    }
                    else
                    {
                        _usedRadio = true;
                        _game.DisplayBeat(-17);
                    }
                    _game._beatHistory.RemoveAll(item => item == 600);
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 221)
            {
                switch (_roomNumber)
                {
                    case (497):
                        _game.DisplayBeat(222);
                        break;
                    case (498):
                        _game.DisplayBeat(223);
                        break;
                    case (499):
                        _game.DisplayBeat(224);
                        break;
                    case (500):
                        _game.DisplayBeat(225);
                        break;
                    default:
                        _game.DisplayBeat(225);
                        break;
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 226)
            {
                if (_genderSwapped)
                {
                    _game.DisplayBeat(228);
                }
                else _game.DisplayBeat(227);

            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 601)
            {
                _game.DisplayBeat(79);
                _game._beatHistory.Remove(601);
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 229)
            {
                switch (_roomNumber)
                {
                    case (497):
                        _game.DisplayBeat(231);
                        break;
                    case (498):
                        _game.DisplayBeat(252);
                        break;
                    case (499):
                        _game.DisplayBeat(260);
                        break;
                    case (500):
                        _game.DisplayBeat(270);
                        break;
                    case (501):
                        _game.DisplayBeat(230);
                        break;
                    default:
                        break;
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 252)
            {
                if (_genderSwapped)
                {
                    _game.DisplayBeat(257);
                }
                else _game.DisplayBeat(256);

            }
        }

        else if (_currentBeatID == 246)
        {
            _game._dontDisplayTheseBeats.Add(245);

        }

        if (_currentBeatID == 233 || _currentBeatID == 235 || _currentBeatID == 253 || _currentBeatID == 269 || _currentBeatID == 262 || _currentBeatID == 271)
        {
            _roomNumber++;
        }

        if (_currentBeatID == 238)
        {
            _genderSwapped = true;
        }

        if (_currentBeatID == 239)
        {
            _game._dontDisplayTheseBeats.Add(238);
        }

        if (_currentBeatID == 251)
        {
            _game._joinedGOC = true;
            _game._dontDisplayTheseBeats.Remove(147);
        }

        if (_currentBeatID == -18)
        {
            _usedRadio = true;
            _roomNumber++;
        }

        if (_currentBeatID == -6) //Encounter Survived
        {
            Score.Deaths -= 1;
            Score.Died = false;
        }

        if (_currentBeatID == 273)
        {
            _game._dontDisplayTheseBeats.RemoveAll(item => item == 142);
            _game._joinedInsurgency = true;
            Score.Bonus_Info = true;
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 236)
        {
            _game._displayTextOnlyOnce.Add(236);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 252)
        {
            _game._displayTextOnlyOnce.Add(252);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 234)
        {
            _game._displayTextOnlyOnce.Add(234);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 221)
        {
            _game._displayTextOnlyOnce.Add(221);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 265)
        {
            _game._displayTextOnlyOnce.Add(265);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 270)
        {
            _game._displayTextOnlyOnce.Add(270);
        }

    }
}
