using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class SCP035 : SCPScript
{
    private bool _usedRadio = false;
    private bool _usedDClass = false;
    private int _strikes = 0;
    private int _totalQuestionsAsked = 0;
    private bool _alreadyLookedAtScreen = false;
    private bool _alreadyLookedAtScreenAfterDClass = false;
    private int _currentBeat;
    private List<int> _negativeBeatsNotToDisplay;
    private bool _startingBeatExecuted = false;

    private void Update()
    {

        if (_currentBeat >= 168 && _currentBeat <= 214)
        {
            if (Input.GetKey("r"))
            {
                _game.DisplayBeat(600);
            }
        }

        if (_currentBeat >= 168 && _currentBeat <= 214)
        {
            _game._canAccessEscapeButton = true;
        }
        else _game._canAccessEscapeButton = true;
    }



    public override void UpdateCurrentBeat(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {
        _currentBeat = _currentBeatID;

        if (!_startingBeatExecuted) //Remove options that require scissors at start
        {
            int[] beatsAdd = { 600, 175, 195, 204, 208 };
            _game.DontDisplayTheseBeats(beatsAdd.ToList());
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
                    if (_usedRadio)
                    {
                    _game.DisplayBeat(-14);
                    _game._beatHistory.RemoveAll(item => item == 600);
                    }
                    else {
                    _usedRadio = true;
                    _game.DisplayBeat(-15);
                    _game._beatHistory.RemoveAll(item => item == 600);
                }
                }
                else if (_game._beatHistory[_game._beatHistory.Count - 1] == 169)
                {
                    if (_usedDClass)
                    {
                        _game.DisplayBeat(179);
                    }
                    else _game.DisplayBeat(170);
                }
                else if (_game._beatHistory[_game._beatHistory.Count - 1] == 174)
                {
                
                    if (_usedDClass)
                    {
                        if (_alreadyLookedAtScreenAfterDClass)
                        {
                            _game.DisplayBeat(175);
                        }
                        else { 
                            _game.DisplayBeat(172);
                            _alreadyLookedAtScreenAfterDClass = true;
                        }
                    }
                    else {
                        if (_alreadyLookedAtScreen)
                        {
                            _game.DisplayBeat(173);
                        }else _game.DisplayBeat(176);

                    }
                    _alreadyLookedAtScreen = true;
                }
                else if (_game._beatHistory[_game._beatHistory.Count - 1] == 187)
                {
                    _strikes++;
                    if (_strikes >= 3)
                    {
                        _game.DisplayBeat(190);
                    }
                    else {
                    
                        _game.DisplayBeat(189); ; 
                    }
                }
                else if (_game._beatHistory[_game._beatHistory.Count - 1] == 185)
                {
                    _strikes++;
                    if (_strikes >= 3)
                    {
                        _game.DisplayBeat(191);
                    }
                    else
                    {
                        _game.DisplayBeat(192);
                    }
                }
                else if (_game._beatHistory[_game._beatHistory.Count - 1] == 197)
                {
                    _strikes++;
                    if (_strikes >= 3)
                    {
                        _game.DisplayBeat(199);
                    }
                    else
                    {
                        _game.DisplayBeat(200);
                    }
                }
                else if (_game._beatHistory[_game._beatHistory.Count - 1] == 204)
                {
                    _strikes++;
                    if (_strikes >= 3)
                    {
                        _game.DisplayBeat(205);
                    }
                    else
                    {
                        _game.DisplayBeat(206);
                    }

                }

            }

            if (_game._beatHistory[_game._beatHistory.Count - 2] == 168)
            {
                _game._displayTextOnlyOnce.Add(168);
            }

            if (_currentBeatID == -15)
            {
                _usedRadio = true;
                _usedDClass = true;
            }

            if (_currentBeatID == -13)
            {
                _game._dontDisplayTheseBeats.Add(600);
            }

            if (_currentBeatID == 171)
            {
                _game._dontDisplayTheseBeats.Remove(175);
            }

            if (_currentBeatID == 169)
            {
                _game._displayTextOnlyOnce.Add(169);
            }

            if (_currentBeatID == -6) //Encounter Survived
            {
                Score.Deaths -= 1;
                Score.Died = false;
            }
            if (_currentBeatID == 194)
            {
                _game._dontDisplayTheseBeats.Remove(195);
            }

            if (_currentBeatID == 186)
            {
                _game._dontDisplayTheseBeats.Remove(204);
            }

            if (_currentBeatID == 210)
            {
                _game._dontDisplayTheseBeats.Remove(208);
            }

            if (_currentBeatID == 188)
            {
                _strikes--;
            }

            if (_currentBeatID == 211)
            {
                Score.Total_Bonus_Info++;
                Score.Bonus_Info = true;
            }

            if (_currentBeatID == 183 || _currentBeatID == 181 || _currentBeatID == 186 || _currentBeatID == 180 || _currentBeatID == 187 || _currentBeatID == 193 || _currentBeatID == 195 || _currentBeatID == 212 || _currentBeatID == 203 || _currentBeatID == 211 || _currentBeatID == 204 || _currentBeatID == 210) //questions
            {
                _game._dontDisplayTheseBeats.Add(_currentBeatID);
                _totalQuestionsAsked++;
            }

            if (_currentBeatID == -10)
            {
                if (_game._beatHistory[_game._beatHistory.Count - 1] == 185)
                {
                    _game._dontDisplayTheseBeats.Add(_currentBeatID);
                    _negativeBeatsNotToDisplay.Add(_currentBeatID);
                }
                else if (_game._beatHistory[_game._beatHistory.Count - 1] == 197)
                {
                    _game._dontDisplayTheseBeats.Add(_currentBeatID);
                    _negativeBeatsNotToDisplay.Add(_currentBeatID);
                }
            }

            if (_currentBeatID == 196)
            {
                if (!_negativeBeatsNotToDisplay.Contains(197))
                {
                    _game._dontDisplayTheseBeats.Remove(_currentBeatID);
                }
            }else if (_currentBeatID == 184)
            {
                if (!_negativeBeatsNotToDisplay.Contains(185))
                {
                    _game._dontDisplayTheseBeats.Remove(_currentBeatID);
                }
            } else _game._dontDisplayTheseBeats.Remove(-10);

            if (_totalQuestionsAsked >= 8 )
            {
                _game.DisplayBeat(217);
            }
    }
}
