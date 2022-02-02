using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivingQuarters : SCPScript
{
    [SerializeField] public GameObject SCPNameLabel;
    [SerializeField] public GameObject SCPNameInput;
    [SerializeField] public GameObject SCPNumber;

    private bool _ignoreLastElement = false;
    private string SCPName;
    private int _nextBeatAfterSCPNumberChoice;
    private bool _acceptedGOCMission = false;
    private bool _acceptedInsurgencyMission1 = false;
    private bool _acceptedInsurgencyMission2 = false;
    private ScoreTypes _currentScore;
    private int _currentBeat;
    public bool _gameOver = false;
    private void Update()
    {
        SCPName = SCPNameInput.GetComponent<TMP_InputField>().text;
        _game._canAccessEscapeButton = false;
        if (SCPNumber.activeSelf)
        {
            SCPNameInput.GetComponent<TMP_InputField>().ActivateInputField();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SCPNumber.SetActive(false);
                _game.DisplayBeat(_nextBeatAfterSCPNumberChoice);
            }
        }
    }

    void Start()
    {
        _game._canAccessEscapeButton = false;
        _game._dontDisplayTheseBeats.Add(142); // insurgency code
        _game._dontDisplayTheseBeats.Add(149); // Next step
        _game._dontDisplayTheseBeats.Add(147); // Read Me
        _game._dontDisplayTheseBeats.Add(148); // failure
        _game._dontDisplayTheseBeats.Add(153); // tv off
        _game._dontDisplayTheseBeats.Add(155); // tv on
        _game._dontDisplayTheseBeats.Add(158); // food
        _game._dontDisplayTheseBeats.Add(159); // Knocks
        _game._dontDisplayTheseBeats.Add(528); // Remove option to injest pills
    }

    public override void UpdateCurrentBeat(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {
        _currentBeat = _currentBeatID;
        _currentScore = Score;
        switch (_game._currentSCPNumber)
        {
            case ("066"):
                SCP_066_Post_Encounter(_currentBeatID, _startingBeat, Score);
                _nextBeatAfterSCPNumberChoice = -10;
                break;
            case ("LivingQuarters"):
                LivingQuartersBeats(_currentBeatID,_startingBeat, Score);
                break;
            case ("035"):
                SCP_035_Post_Encounter(_currentBeatID, _startingBeat, Score);
                _nextBeatAfterSCPNumberChoice = 67;
                break;
            case ("249"):
                SCP_249_Post_Encounter(_currentBeatID, _startingBeat, Score);
                _nextBeatAfterSCPNumberChoice = 79;
                break;
            case ("261"):
                SCP_261_Post_Encounter(_currentBeatID, _startingBeat, Score);
                _nextBeatAfterSCPNumberChoice = 93;
                break;
            case ("662"):
                SCP_662_Post_Encounter(_currentBeatID, _startingBeat, Score);
                _nextBeatAfterSCPNumberChoice = 101;
                break;
            case ("093"):
                SCP_093_Post_Encounter(_currentBeatID, _startingBeat, Score);
                _nextBeatAfterSCPNumberChoice = 113;
                break;
            default:
                break;
        }
    }

    private void GameOverCheck(ScoreTypes Score, int _currentBeatID)
    {
        if (Score.Deaths >= 4)
        {
            SCPNumber.SetActive(false);
            _game._dontDisplayTheseBeats.Add(_currentBeatID);
            _game.DisplayBeat(134);
            Score.Deaths = 0;
            _gameOver = true;
        }
        else if (Score.Incorrect_SCP >= 2)
        {
            SCPNumber.SetActive(false);
            _game._dontDisplayTheseBeats.Add(_currentBeatID);
            _game.DisplayBeat(135);
            Score.Incorrect_SCP = 0;
            _gameOver = true;
        }
        else if (Score.Incorrect_Containment >= 3)
        {
            SCPNumber.SetActive(false);
            _game._dontDisplayTheseBeats.Add(_currentBeatID);
            _game.DisplayBeat(136);
            Score.Incorrect_Containment = 0;
            _gameOver = true;
        }
    }

    private void SCP_066_Post_Encounter(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {
        if (_game._beatHistory[_game._beatHistory.Count - 1] == 52)
        {
            Score.Chosen_Containment = "Safe";
        }

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 53)
        {
            Score.Incorrect_Containment -= 1; //correct containment
            Score.Chosen_Containment = "Euclid";
        }

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 54)
        {
            Score.Chosen_Containment = "Keter";
        }

        if (_currentBeatID == 55)
        {
            SCPNumber.SetActive(true);
            SCPNameInput.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if (_currentBeatID == -10) //Redirect to post scenario containment selection
        {
            if (_game._beatHistory[_game._beatHistory.Count - 1] == 55) //show correct beat depending on if character died
            {
                if (SCPName.Contains("66"))
                {
                    Score.Incorrect_SCP -= 1;
                    Score.Wrong_SCP = false;
                }
                SCPNameInput.GetComponent<TMP_InputField>().DeactivateInputField();
                GameOverCheck(Score, _currentBeatID);
                if (!_gameOver)
                {
                    if (Score.Died)
                    {
                        Score.Died = false;
                        _game.DisplayBeat(58);
                    }
                    else
                    {
                        Score.Died = true;
                        _game.DisplayBeat(57);
                    }
                }

            }
            else if(_game._beatHistory[_game._beatHistory.Count - 1] == 57 || _game._beatHistory[_game._beatHistory.Count - 1] == 58) //show correct beat depending on if character chose correct containment
            {
                if (Score.Chosen_Containment == "Safe")
                {
                    _game.DisplayBeat(59);
                }
                if (Score.Chosen_Containment == "Euclid")
                {
                    _game.DisplayBeat(61);
                }
                if (Score.Chosen_Containment == "Keter") //correct containment
                {
                    _game.DisplayBeat(60);
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] >= 59 && _game._beatHistory[_game._beatHistory.Count - 1] <= 61) //show correct beat depending on if character got the wrong scp number
            {
                if (Score.Wrong_SCP)
                {
                    _game.DisplayBeat(63);
                }
                else
                {
                    _game.DisplayBeat(62);
                    Score.Wrong_SCP = true;
                }
            }
        }
        if (_currentBeatID == 65) // end encounter
        {
            _game._displayTextOnlyOnce.Add(137);
            _game._currentSCPNumber = "LivingQuarters";
        }
    }

    private void LivingQuartersBeats(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {

        if (_game._insurgencyTask1) {
            _game._displayTextOnlyOnce.Remove(142);
        }
  
        if(_game._GOCTask1)
        {
            _game._dontDisplayTheseBeats.Remove(155);
        }

        if(_currentBeatID == 142)
        {
            _game._insurgencyTask1 = false;
            _game._dontDisplayTheseBeats.Add(142);
            _game._dontDisplayTheseBeats.Remove(147);
        }

        if (_currentBeatID == 147)
        {
            _acceptedInsurgencyMission1 = true;
            _game._dontDisplayTheseBeats.Add(147);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 151)
        {
            _game._displayTextOnlyOnce.Add(151);
        }

        if (_currentBeatID == 152)
        {
            _game._dontDisplayTheseBeats.Add(152);
            _game._dontDisplayTheseBeats.Remove(153);
        }

        if (_currentBeatID == 153)
        {
            _game._dontDisplayTheseBeats.Add(153);
            _game._dontDisplayTheseBeats.Remove(152);
        }

        if (_currentBeatID == 158)
        {
            _game._dontDisplayTheseBeats.Remove(158);
        }

        if (_currentBeatID == 160)
        {
            _game._dontDisplayTheseBeats.Remove(158);
        }

        if (_currentBeatID == 165)
        {
            _acceptedGOCMission = true;
        }

        if (_currentBeatID == 66)
        {
            _game._displayTextOnlyOnce.Add(137);
        }else
        {
            _game._displayTextOnlyOnce.Remove(137);
        }

        if (_currentBeatID == 165)
        {
            _game._dontDisplayTheseBeats.Remove(528);
        }

        if (_currentBeatID == 159)
        {
            _game._dontDisplayTheseBeats.Add(159);// Knocks
        }

        if (_currentBeatID == 149)
        {
            _game._dontDisplayTheseBeats.Add(149);// next step
        }

        if (_currentBeatID == -10)
        {
            if (_game._beatHistory[_game._beatHistory.Count - 1] == 138)
            {
                switch (_game._SCPEncounterNumber)
                {
                    case (1):
                        _game.DisplayBeat(166);
                        Debug.Log(_game._SCPEncounterNumber);
                        _game._SCPEncounterNumber = 2;
                        break;
                    case (2):
                        _game.DisplayBeat(218);
                        Debug.Log(_game._SCPEncounterNumber);
                        _game._SCPEncounterNumber = 3;
                        break;
                    case (3):
                        _game.DisplayBeat(274);
                        Debug.Log(_game._SCPEncounterNumber);
                        _game._SCPEncounterNumber = 4;
                        break;
                    case (4):
                        _game.DisplayBeat(336);
                        Debug.Log(_game._SCPEncounterNumber);
                        _game._SCPEncounterNumber = 5;
                        break;
                    case (5):
                        _game.DisplayBeat(441);
                        Debug.Log(_game._SCPEncounterNumber);
                        _game._SCPEncounterNumber = 6;
                        break;
                    default:
                        break;
                }

            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 158)
            {
                _game._dontDisplayTheseBeats.Add(158);
                if (_game._GOCTask1)
                {
                    _game.DisplayBeat(161);
                }
                else _game.DisplayBeat(162);
            }
        }
    }

    private void SCP_035_Post_Encounter(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 52)
        {
            
            Score.Chosen_Containment = "Safe";
        }

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 53)
        {
            Score.Chosen_Containment = "Euclid";
        }

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 54)
        {
            Score.Incorrect_Containment -= 1; //correct containment
            Score.Chosen_Containment = "Keter";
        }

        if (_currentBeatID == 55)
        {
            SCPNumber.SetActive(true);
            SCPNameInput.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if (_currentBeatID == -10) //Redirect to post scenario containment selection
        {
            if (_game._beatHistory[_game._beatHistory.Count - 1] == 67) //show correct beat depending on if character found the secret info and check for correct scp name
            {
                if (SCPName.Contains("35"))
                {
                    Score.Incorrect_SCP -= 1;
                    Score.Wrong_SCP = false;
                }
                SCPNameInput.GetComponent<TMP_InputField>().DeactivateInputField();
                GameOverCheck(Score, _currentBeatID);
                if (!_gameOver)
                {
                    if (Score.Bonus_Info)
                    {
                        _game.DisplayBeat(68);
                        if (Score.Incorrect_Containment > 0)
                            Score.Incorrect_Containment -= 1;
                        else if (Score.Incorrect_SCP > 0)
                            Score.Incorrect_SCP -= 1;
                        else
                            _game.Score.Deaths -= 1;
                        Score.Bonus_Info = false;
                    }
                    else _game.DisplayBeat(69);

                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 68 || _game._beatHistory[_game._beatHistory.Count - 1] == 69) //show correct beat depending on if character chose correct containment
            {
                if (Score.Chosen_Containment == "Safe")
                {
                    _game.DisplayBeat(70);
                }
                if (Score.Chosen_Containment == "Euclid")
                {
                    _game.DisplayBeat(72);
                }
                if (Score.Chosen_Containment == "Keter")
                {
                    _game.DisplayBeat(71);
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] >= 70 && _game._beatHistory[_game._beatHistory.Count - 1] <= 72) //show correct beat depending on if character got the wrong scp number
            {
                if (Score.Wrong_SCP)
                {
                    _game.DisplayBeat(78);
                }
                else
                {
                    _game.DisplayBeat(77);
                    Score.Wrong_SCP = true;
                }
            }

        }
        if (_currentBeatID == 77 || _currentBeatID == 78) // end encounter
        {
            _game._currentSCPNumber = "LivingQuarters";
        }
    }

    private void SCP_249_Post_Encounter(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 52)
        {
            
            Score.Chosen_Containment = "Safe";
        }

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 53)
        {
            Score.Incorrect_Containment -= 1; //correct containment
            Score.Chosen_Containment = "Euclid";
        }

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 54)
        {
            Score.Chosen_Containment = "Keter";
        }

        if (_currentBeatID == 55)
        {
            SCPNumber.SetActive(true);
            SCPNameInput.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if(_currentBeatID == 88 || _currentBeatID == 92)
        {
            _game._joinedGOC = false;
        }

        if (_currentBeatID == 89 || _currentBeatID == 91)
        {
            
            _game._joinedInsurgency = false;
        }

        if (_currentBeatID == -10) //Redirect to post scenario containment selection
        {

            if (_game._beatHistory[_game._beatHistory.Count - 1] == 79) //show correct beat depending on if character died
            {
                if (SCPName.Contains("249"))
                {
                    Score.Incorrect_SCP -= 1;
                    Score.Wrong_SCP = false;
                }
                SCPNameInput.GetComponent<TMP_InputField>().DeactivateInputField();
                GameOverCheck(Score, _currentBeatID);
                if (!_gameOver)
                {
                    if (Score.Bonus_Info)
                    {
                        _game.DisplayBeat(80);
                        if (Score.Incorrect_Containment > 0)
                            Score.Incorrect_Containment -= 1;
                        else if (Score.Incorrect_SCP > 0)
                            Score.Incorrect_SCP -= 1;
                        else
                            _game.Score.Deaths -= 1;
                        Score.Bonus_Info = false;
                    }
                    else _game.DisplayBeat(81);
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 80 || _game._beatHistory[_game._beatHistory.Count - 1] == 81) //show correct beat depending on if character chose correct containment
            {
                if (Score.Chosen_Containment == "Safe")
                {
                    _game.DisplayBeat(82);

                }
                if (Score.Chosen_Containment == "Euclid")
                {
                    _game.DisplayBeat(83);
                }
                if (Score.Chosen_Containment == "Keter")
                {
                    _game.DisplayBeat(84);
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] >= 82 && _game._beatHistory[_game._beatHistory.Count - 1] <= 84) //show correct beat depending on if character got the wrong scp number
            {
                if (Score.Wrong_SCP)
                {
                    _game.DisplayBeat(86);
                }
                else
                {
                    _game.DisplayBeat(85);
                    Score.Wrong_SCP = true;
                }
            }

        }

        if (_currentBeatID >= 90 && _currentBeatID <= 91) // end encounter
        {
            _game._currentSCPNumber = "LivingQuarters";
        }
    }

    private void SCP_261_Post_Encounter(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 52)
        {
            Score.Incorrect_Containment -= 1; //correct containment
            Score.Chosen_Containment = "Safe";
        }

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 53)
        {
            Score.Chosen_Containment = "Euclid";
        }

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 54)
        {
            Score.Chosen_Containment = "Keter";
        }

        if (_currentBeatID == 55)
        {
            SCPNumber.SetActive(true);
            SCPNameInput.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if (_currentBeatID == -10) //Redirect to post scenario containment selection
        {


            if (_game._beatHistory[_game._beatHistory.Count - 1] == 93) //show correct beat depending on if character found the secret info
            {
                if (SCPName.Contains("261"))
                {
                    Score.Incorrect_SCP -= 1;
                    Score.Wrong_SCP = false;
                }
                SCPNameInput.GetComponent<TMP_InputField>().DeactivateInputField();
                GameOverCheck(Score, _currentBeatID);
                if (!_gameOver)
                {
                    if (Score.Bonus_Info)
                    {
                        _game.DisplayBeat(94);
                        if (Score.Incorrect_Containment > 0)
                            Score.Incorrect_Containment -= 1;
                        else if (Score.Incorrect_SCP > 0)
                            Score.Incorrect_SCP -= 1;
                        else
                            _game.Score.Deaths -= 1;
                        Score.Bonus_Info = false;
                    }
                    else _game.DisplayBeat(95);

                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 94 || _game._beatHistory[_game._beatHistory.Count - 1] == 95) //show correct beat depending on if character chose correct containment
            {
                if (Score.Chosen_Containment == "Safe")
                {
                    _game.DisplayBeat(96);

                }
                if (Score.Chosen_Containment == "Euclid")
                {
                    _game.DisplayBeat(97);
                }
                if (Score.Chosen_Containment == "Keter")
                {
                    _game.DisplayBeat(98);
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] >= 96 && _game._beatHistory[_game._beatHistory.Count - 1] <= 98) //show correct beat depending on if character got the wrong scp number
            {
                if (Score.Wrong_SCP)
                {
                    _game.DisplayBeat(100);
                }
                else
                {
                    _game.DisplayBeat(99);
                    Score.Wrong_SCP = true;
                }
            }

        }
        if (_currentBeatID == 99 || _currentBeatID == 100) // end encounter
        {
            _game._currentSCPNumber = "LivingQuarters";
        }
    }

    private void SCP_662_Post_Encounter(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 52)
        {
            Score.Incorrect_Containment = -1; //correct containment
            Score.Chosen_Containment = "Safe";
        }

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 53)
        {
            Score.Chosen_Containment = "Euclid";
        }

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 54)
        {
            Score.Chosen_Containment = "Keter";
        }

        if (_currentBeatID == 55)
        {
            SCPNumber.SetActive(true);
            SCPNameInput.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if (_currentBeatID == -10) //Redirect to post scenario containment selection
        {
            if (_game._beatHistory[_game._beatHistory.Count - 1] == 101) //show correct beat depending on if character found the secret info
            {
                if (SCPName.Contains("662"))
                {
                    Score.Incorrect_SCP = -1;
                    Score.Wrong_SCP = false;
                }
                SCPNameInput.GetComponent<TMP_InputField>().DeactivateInputField();
                GameOverCheck(Score, _currentBeatID);
                if (!_gameOver)
                {
                    if (Score.Bonus_Info)
                    {
                        _game._dontDisplayTheseBeats.Remove(149);
                        _game.DisplayBeat(102);
                        if (Score.Incorrect_Containment > 0)
                            Score.Incorrect_Containment -= 1;
                        else if (Score.Incorrect_SCP > 0)
                            Score.Incorrect_SCP -= 1;
                        else
                            _game.Score.Deaths -= 1;
                        Score.Bonus_Info = false;
                    }
                    else
                    {
                        _game._dontDisplayTheseBeats.Remove(148);
                        _game.DisplayBeat(103);
                    }
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 102 || _game._beatHistory[_game._beatHistory.Count - 1] == 103) //show correct beat depending on if character chose correct containment
            {
                if (Score.Chosen_Containment == "Safe")
                {
                    _game.DisplayBeat(104);

                }
                if (Score.Chosen_Containment == "Euclid")
                {
                    _game.DisplayBeat(105);
                }
                if (Score.Chosen_Containment == "Keter")
                {
                    _game.DisplayBeat(106);
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] >= 104 && _game._beatHistory[_game._beatHistory.Count - 1] <= 106) //show correct beat depending on if character got the wrong scp number
            {
                if (Score.Wrong_SCP)
                {
                    _game.DisplayBeat(108);
                }
                else
                {
                    _game.DisplayBeat(107);
                    Score.Wrong_SCP = true;
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 107 || _game._beatHistory[_game._beatHistory.Count - 1] == 108) //show correct beat depending on if character tried to escape
            {
                if (_game._triedEscape)
                {
                    _game.DisplayBeat(109);
                }else _game.DisplayBeat(110);
            }

        }
        if (_currentBeatID == 111 || _currentBeatID == 112) // end encounter
        {
            _game._currentSCPNumber = "LivingQuarters";
            _game._dontDisplayTheseBeats.Remove(159);// Knocks
        }
        
        Score.Died = false;
    }

    private void SCP_093_Post_Encounter(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {

        if (_currentBeatID == 55)
        {
            SCPNumber.SetActive(true);
            SCPNameInput.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if (_currentBeatID == 56)
        {
            if (SCPName.Contains("93"))
            {
                Score.Incorrect_SCP = -1;
                Score.Wrong_SCP = false;
            }
            SCPNumber.SetActive(false);
            SCPNameInput.GetComponent<TMP_InputField>().DeactivateInputField();
        }

        if (_currentBeatID == -10) //Redirect to post scenario containment selection
        {
            if (_game._beatHistory[_game._beatHistory.Count - 1] == 113) 
            {
                if (Score.Died)
                {
                    if (_game._insurgencyTask2)
                    {
                        Debug.Log("_joinedInsurgency + Dead");
                        _game.DisplayBeat(114);
                    }
                    else {
                        _game.DisplayBeat(115);
                        Debug.Log("notjoinedInsurgency + Dead"); 
                    }
                }
                else
                {
                    if (_game._insurgencyTask2)
                    {
                        Debug.Log("_joinedInsurgency + Alive");
                        _game.DisplayBeat(118);
                    }
                    else
                    {
                        Debug.Log("notjoinedInsurgency + Alive");
                        if (_game._tookFloppyDisk)
                        {
                            Debug.Log("_joinedInsurgency + Alive + Floppy");
                            _game.DisplayBeat(130);
                        }
                        else {
                            Debug.Log("notjoinedInsurgency + Alive + NotFloppy");
                            _game.DisplayBeat(115);
                        }
                        
                    }
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 115)
            {
                if (_game._GOCTask2)
                {
                    _game.DisplayBeat(116);
                }else _game.DisplayBeat(117);
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 118)
            {
                if (_game._tookFloppyDisk)
                {
                    _game.DisplayBeat(119);
                }
                else _game.DisplayBeat(120);
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 119)
            {
                if (_game._GOCTask2)
                {
                    _game.DisplayBeat(121);
                }
                else {

                    _game.DisplayBeat(122); 
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 120)
            {
                if (_game._GOCTask2)
                {
                    _game.DisplayBeat(128);
                }
                else {
                    if (!_game._tookFloppyDisk)
                    {
                        _game._dontDisplayTheseBeats.Add(123);
                    }
                    _game.DisplayBeat(122);
                } 
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 130)
            {
                if (_game._GOCTask2)
                {
                    _game.DisplayBeat(132);
                }
                else _game.DisplayBeat(131);
            }
        }
        if (_currentBeatID == 133 || _currentBeatID == 131 || _currentBeatID == 129 || _currentBeatID == 127 || _currentBeatID == 124 || _currentBeatID == 123 || _currentBeatID == 117 || _currentBeatID == 116 || _currentBeatID == 114)
        {
            _game._audioManager.FinalDecisionTheme();
        }
    }
}
