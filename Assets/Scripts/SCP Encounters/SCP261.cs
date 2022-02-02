using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class SCP261 : SCPScript
{
    private int _roomNumber = 497;
    private bool _insideDoor = false;
    private int _currentBeat;
    private bool _usedRadio = false;
    private bool _plugged = true;
    private int _money = 5000;
    private int _moneyInserted = 0;
    private string _coinInput;
    private string _numberInput;
    private bool _coinsInserted = false;
    private bool _numberInserted = false;
    private int _500YenBeat = 289;
    private int _1000YenBeat = 308;
    private int _1000YenBeatNoEletric = 318;
    private int _1500YenBeat = 328;
    private bool _startingBeatExecuted = false;

    [SerializeField] public GameObject Coins;
    [SerializeField] public GameObject Number;
    [SerializeField] public GameObject CoinsInput;
    [SerializeField] public GameObject NumberInput;
    [SerializeField] public GameObject _currentCoins;
    [SerializeField] public TMP_Text _currentCoinsText;

    private void Update()
    {
        if (_currentBeat >= 276 && _currentBeat <= 335)
        {
            if (Input.GetKey("r"))
            {
                _game.DisplayBeat(600);
            }
            _currentCoins.SetActive(true);
        }else _currentCoins.SetActive(false);

        if (_currentBeat >= 276 && _currentBeat <= 335)
        {
            _game._canAccessEscapeButton = true;
        }
        else _game._canAccessEscapeButton = true;

        _coinInput = CoinsInput.GetComponent<TMP_InputField>().text;
        _numberInput = NumberInput.GetComponent<TMP_InputField>().text;
        _currentCoinsText.text = "Current Coins: " + (_money / 500).ToString();
    }

    public override void UpdateCurrentBeat(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {

        if (!_startingBeatExecuted) //Remove options that require scissors at start
        {
            Score.Deaths += 1;
            Score.Incorrect_SCP += 1;
            Score.Incorrect_Containment += 1;
            Score.Died = true;
            _game._dontDisplayTheseBeats.Remove(600);
            _game._dontDisplayTheseBeats.Add(279);
            _startingBeatExecuted = true;
        }

        _currentBeat = _currentBeatID;

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
                    _game.DisplayBeat(-21);
                    _game._beatHistory.RemoveAll(item => item == 600);
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 276 || _game._beatHistory[_game._beatHistory.Count - 1] == 281)
            {
                if (_coinsInserted && _numberInserted)
                {
                    _numberInserted = false;
                    _coinsInserted = false;

                    if(_moneyInserted == 500)
                    {
                        _game.DisplayBeat(_500YenBeat);
                        _500YenBeat += 2;
                    }
                    else if (_moneyInserted == 1000)
                    {
                        if (!_plugged)
                        {
                            _game.DisplayBeat(_1000YenBeatNoEletric);
                            _1000YenBeatNoEletric += 2;
                        }
                        else { 
                            _game.DisplayBeat(_1000YenBeat);
                            _1000YenBeat += 2;
                        }
                    }
                    else if (_moneyInserted >= 1500)
                    {
                        if(_1500YenBeat == 328 ){
                            _game.DisplayBeat(_1500YenBeat);
                            _1500YenBeat += 1;
                        }else
                        {
                            _game.DisplayBeat(_1500YenBeat);
                            _1500YenBeat += 2;
                        }
                    }
                    _moneyInserted = 0;
                }
                else _game.DisplayBeat(288);
            }
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 278)
        {
            _game._dontDisplayTheseBeats.Remove(279);
            _game._dontDisplayTheseBeats.Add(278);
            
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 277)
        {
            _game._dontDisplayTheseBeats.Add(277);
        }

        if (_currentBeatID == 238 )
        {
            _money = 5000;
        }

        if (_currentBeatID == 278)
        {
            _plugged = false;
        }

        if (_currentBeatID == 279)
        {
            _plugged = true;
        }

        if (_currentBeatID == 328)
        {
            Score.Bonus_Info = true;
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 279)
        {
            _game._dontDisplayTheseBeats.Add(279);
            _game._dontDisplayTheseBeats.Remove(278);
        }

        if (_currentBeatID == 282)
        {
            Coins.SetActive(true);
            CoinsInput.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 281)
        {
            _game._displayTextOnlyOnce.Add(281);
        }

        if (_currentBeatID == 286)
        {
            Number.SetActive(true);
            NumberInput.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if (_currentBeatID == -6) //Encounter Survived
        {
            Score.Deaths -= 1;
            Score.Died = false;
        }

    }
    public void NumbersEntered()
    {
        if (!_coinsInserted)
        {
            _game.DisplayBeat(335);
            Number.SetActive(false);
        }
        else if (!string.IsNullOrEmpty(_numberInput))
        {
            _numberInserted = true;
            _game.DisplayBeat(287);
            Number.SetActive(false);
        }
    }

    public void CoinsEntered()
    {
        switch(_coinInput)
        {
            case ("1"):
                if(_money >= 500)
                {
                    _coinsInserted = true;
                    _money -= 500;
                    _moneyInserted = 500;
                    _game.DisplayBeat(284);
                }
                else
                {
                    _game.DisplayBeat(283);
                }
                break;
            case ("2"):
                if (_money >= 1000)
                {
                    _coinsInserted = true;
                    _money -= 1000;
                    _moneyInserted = 1000;
                    _game.DisplayBeat(284);
                }
                else
                {
                    _game.DisplayBeat(283);
                }
                break;
            case ("3"):
                if (_money >= 1500)
                {
                    _coinsInserted = true;
                    _money -= 1500;
                    _moneyInserted = 1500;
                    _game.DisplayBeat(284);
                }
                else
                {
                    _game.DisplayBeat(283);
                }
                break;
            case ("4"):
                if (_money >= 2000)
                {
                    _coinsInserted = true;
                    _money -= 2000;
                    _moneyInserted = 2000;
                    _game.DisplayBeat(284);
                }
                else
                {
                    _game.DisplayBeat(283);
                }
                break;
            case ("5"):
                if (_money >= 2500)
                {
                    _coinsInserted = true;
                    _money -= 2500;
                    _moneyInserted = 2500;
                    _game.DisplayBeat(284);
                }
                else
                {
                    _game.DisplayBeat(283);
                }
                break;
            case ("6"):
                if (_money >= 3000)
                {
                    _coinsInserted = true;
                    _money -= 3000;
                    _moneyInserted = 3000;
                    _game.DisplayBeat(284);
                }
                else
                {
                    _game.DisplayBeat(283);
                }
                break;
            case ("7"):
                if (_money >= 3500)
                {
                    _coinsInserted = true;
                    _money -= 3500;
                    _moneyInserted = 3500;
                    _game.DisplayBeat(284);
                }
                else
                {
                    _game.DisplayBeat(283);
                }
                break;
            case ("8"):
                if (_money >= 4000)
                {
                    _coinsInserted = true;
                    _money -= 4000;
                    _moneyInserted = 4000;
                    _game.DisplayBeat(284);
                }
                else
                {
                    _game.DisplayBeat(283);
                }
                break;
            case ("9"):
                if (_money >= 4500)
                {
                    _coinsInserted = true;
                    _money -= 4500;
                    _moneyInserted = 4500;
                    _game.DisplayBeat(284);
                }
                else
                {
                    _game.DisplayBeat(283);
                }
                break;
            case ("10"):
                if (_money >= 5000)
                {
                    _coinsInserted = true;
                    _money -= 5000;
                    _moneyInserted = 5000;
                    _game.DisplayBeat(284);
                }
                else
                {
                    _game.DisplayBeat(283);
                }
                break;
            default:
                Coins.SetActive(false);
                _game.DisplayBeat(283);
                break;
        }
        Coins.SetActive(false);
    }
}
