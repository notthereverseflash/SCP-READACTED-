using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class SCP662 : SCPScript
{
    //private int _roomNumber = 497;
    //private bool _insideDoor = false;
    private int _currentBeat;
    private bool _usedRadio = false;
    private bool _hasGun = false;
    private string _moneyInput;
    //private string _keypad1Input;
    private string _keypad2Input;
    private bool _mrDeedsAlive = false;
    private bool _readNote = false;
    int _parsedMoney;
    private bool _startingBeatExecuted = false;

    [SerializeField] public GameObject Money;
    [SerializeField] public GameObject MoneyInput;
    [SerializeField] public GameObject Keypad1;
    [SerializeField] public GameObject Keypad1Input;
    [SerializeField] public GameObject Keypad2;
    [SerializeField] public GameObject Keypad2Input;

    private void Update()
    {
        if (Input.GetKey("r") && _currentBeat >= 338 && _currentBeat <= 399) //Player has accessed the radio
        {
            _game.DisplayBeat(600);
        }
        else if (Input.GetKey("r") && _currentBeat > 399 && _currentBeat < 441 && _currentBeat != 600 && _currentBeat != 601 && _currentBeat != 439)
        {
            _game.DisplayBeat(-28);
            Debug.Log("Test2 " + _currentBeat);
        }

        if (_currentBeat >= 338 && _currentBeat <= 440)
        {
            _game._canAccessEscapeButton = true;
        }
        else _game._canAccessEscapeButton = true;

        //_keypad1Input = Keypad1Input.GetComponent<TMP_InputField>().text;
        _keypad2Input = Keypad2Input.GetComponent<TMP_InputField>().text;
    }

    public override void UpdateCurrentBeat(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {
        _currentBeat = _currentBeatID;

        if (!_startingBeatExecuted) //Remove options that require scissors at start
        {
            int[] beatsAdd = {356,378,440,358};
            _game.DontDisplayTheseBeats(beatsAdd.ToList());
            _game._dontDisplayTheseBeats.Remove(600);

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
                    _game.DisplayBeat(-22);
                    _game._beatHistory.RemoveAll(item => item == 600);

                }
                else
                {
                    _usedRadio = true;
                    _game.DisplayBeat(601);
                    _game._beatHistory.RemoveAll(item => item == 600);
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 368)
            {
                int.TryParse(_moneyInput, out _parsedMoney);
                if(_parsedMoney > 1000)
                {
                    _game.DisplayBeat(373);
                }else _game.DisplayBeat(372);
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 601)
            {
                if (_mrDeedsAlive)
                {
                    _game._beatHistory.RemoveAll(item => item == 601);
                    _game.DisplayBeat(-23);
                    
                }
                else {
                    _game.DisplayBeat(-26);
                    _game._beatHistory.RemoveAll(item => item == 601);
                } 
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 400)
            {
                if(_hasGun)
                {
                    _game.DisplayBeat(404);
                }
                else _game.DisplayBeat(401);
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 440)
            {
                
                if (_game._joinedGOC)
                {
                    _readNote = true;
                    _game.DisplayBeat(-40);
                }
                else _game.DisplayBeat(380);
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 380)
            {
                if (_game._beatHistory[_game._beatHistory.Count - 3] == 346)
                {
                    _game.DisplayBeat(346);
                }
                else _game.DisplayBeat(377);
            }
        }

        if (_currentBeatID == 601)
        {
            Debug.Log("Note taken");
            _game._dontDisplayTheseBeats.Remove(440);
            Debug.Log(_game._dontDisplayTheseBeats.Contains(440));
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 341)
        {
            _game._displayTextOnlyOnce.Add(341);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 346)
        {
            _game._displayTextOnlyOnce.Add(346);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 368)
        {
            _game._displayTextOnlyOnce.Add(368);
        }


        if (_game._beatHistory[_game._beatHistory.Count - 2] == 377)
        {
            _game._displayTextOnlyOnce.Add(377);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 338)
        {
            _game._displayTextOnlyOnce.Add(338);
        }

        if (_currentBeatID == 340 || _currentBeatID == 390)
        {
            _mrDeedsAlive = true;
        }

        if (_currentBeatID == 352)
        {
            _hasGun = true;
        }

        if (_currentBeatID == 367)
        {
            Money.SetActive(true);
        }

        if (_currentBeatID == 372 || _currentBeatID == 373)
        {
            _game._dontDisplayTheseBeats.Add(367);
        }

        if (_currentBeatID == 363)
        {
            _game._dontDisplayTheseBeats.Add(361);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 352)
        {
            _game._dontDisplayTheseBeats.Add(352);
        }

        if (_currentBeatID == 363)
        {
            _game._dontDisplayTheseBeats.Add(361);
        }

        if (_currentBeatID == 349)
        {
            _game._dontDisplayTheseBeats.Add(347);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 354)
        {
            _game._dontDisplayTheseBeats.Add(354);
        }

        if (_currentBeatID == 359 || !_game._joinedInsurgency)
        {
            _game._dontDisplayTheseBeats.Add(358);
        }

        if (_currentBeatID == 396 || _currentBeatID == 397)
        {
            _game._dontDisplayTheseBeats.Add(394);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 346)
        {
            _game._dontDisplayTheseBeats.Add(356);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 378)
        {
            _game._dontDisplayTheseBeats.Add(378);
        }


        if (_currentBeatID == 388)
        {
            _mrDeedsAlive = false;
        }

        if (_readNote)
        {
            _game._dontDisplayTheseBeats.Remove(378);
        }

        if (_currentBeatID == 436)
        {
            Keypad1.SetActive(true);
            Keypad1Input.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if (_currentBeatID == 429)
        {
            Keypad2.SetActive(true);
            Keypad2Input.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if (_currentBeatID == -6 || _currentBeatID == 419 || _currentBeatID == 428 || _currentBeatID == 433) //Encounter Survived
        {
            Score.Deaths -= 1;
            Score.Died = false;
        }

        if (_currentBeatID == -40)
        {
            _game._dontDisplayTheseBeats.Remove(378);
        }

        if (_currentBeatID == 397 || _currentBeatID == 401)
        {
            _game._triedEscape = true;
        }

        if (_currentBeatID == 439) //Encounter Survived
        {
            Score.Bonus_Info = true;
            _game._dontDisplayTheseBeats.Remove(149);
        }

        if (_currentBeatID == 378) //Got Keycard
        {
            _game._GOCTask1 = true;
            _game._dontDisplayTheseBeats.Add(378);
        }
    }

    public void KeypadCode1()
    {
        Keypad1.SetActive(false);
        _game.DisplayBeat(430);
    }

    public void KeypadCode2()
    {
        Keypad2.SetActive(false);
        if (_keypad2Input == "45772")
        {
            _game.DisplayBeat(437);
        }
        else _game.DisplayBeat(430);
    }
}
