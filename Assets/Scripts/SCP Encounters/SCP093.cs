using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class SCP093 : SCPScript
{


    [SerializeField] public GameObject Scp093ReceptionPass;
    [SerializeField] public GameObject Scp093ReceptionPassInput;
    [SerializeField] public GameObject Scp093ServerPass;
    [SerializeField] public GameObject Scp093ServerPassInput;
    [SerializeField] public GameObject Scp093PrivateComputer;
    [SerializeField] public GameObject Scp093PrivateComputerInput;
    [SerializeField] public GameObject Scp093LabComputer;
    [SerializeField] public GameObject Scp093LabComputerInput;

    private int _currentBeat;
    private bool _examinedComputer = false;
    private bool _enteredElevator = false;
    private bool _databaseFixed = false;
    private string _scp093ReceptionPassInput;
    private string _scp093ServerPassInput;
    private string _scp093PrivateComputerInput;
    private string _scp093LabComputerInput;
    private bool _unlocked2115And1151 = false;
    private bool _calledElevator = false;
    private bool _serverTurnedOn = false;
    private bool _rubbleRemoved = false;
    private bool _doorUnlocked = false;
    private bool _churchPurged = false;
    private bool _manifestation = false;
    private bool _startingBeatExecuted = false;
    private void Update()
    {
        if (Input.GetKey("r") && _currentBeat >= 444 && _currentBeat <= 449) //Player has accessed the radio
        {
            _game.DisplayBeat(600);
        }

        if (_currentBeat >= 441 && _currentBeat <= 449)
        {
            _game._canAccessEscapeButton = true;
        }
        else _game._canAccessEscapeButton = true;

        _scp093ReceptionPassInput = Scp093ReceptionPassInput.GetComponent<TMP_InputField>().text;
        _scp093ServerPassInput = Scp093ServerPassInput.GetComponent<TMP_InputField>().text;
        _scp093PrivateComputerInput = Scp093PrivateComputerInput.GetComponent<TMP_InputField>().text;
        _scp093LabComputerInput = Scp093LabComputerInput.GetComponent<TMP_InputField>().text;
    }
    public override void UpdateCurrentBeat(int _currentBeatID, int _startingBeat, ScoreTypes Score)
    {
        if (!_startingBeatExecuted)
        {

            int[] beatsAdd = { 450,466,496,530,514,486,488,489,490,529,535,544,543};
            _game.DontDisplayTheseBeats(beatsAdd.ToList());
            _game._dontDisplayTheseBeats.Remove(600);

            _game.Score.Deaths += 1;
            _game.Score.Incorrect_SCP += 1;
            _game.Score.Incorrect_Containment += 1;
            _game.Score.Died = false;
            _game._insurgencyTask2 = false;
            _game._GOCTask2 = false;
            _startingBeatExecuted = true;
        }

        _currentBeat = _currentBeatID;

        if (_currentBeatID == 449)
        {
            _game._dontDisplayTheseBeats.Remove(450);
        }

        if (_currentBeatID == 444)
        {
            _game._dontDisplayTheseBeats.Remove(444);
        }

        if (_currentBeatID == -10)
        {
            if (_game._beatHistory[_game._beatHistory.Count - 1] == 600)
            {
                _game.DisplayBeat(-22);
                _game._beatHistory.RemoveAll(item => item == 600);
            } else if (_game._beatHistory[_game._beatHistory.Count - 1] == 441)
            {
                if (_game._joinedInsurgency || _game._joinedGOC)
                {
                    _game.DisplayBeat(443);
                } else _game.DisplayBeat(442);
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 450)
            {
                if (_enteredElevator)
                {
                    _game.DisplayBeat(464);
                }
                else {
                    _enteredElevator = true;
                    _game.DisplayBeat(463);
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 456)
            {
                if (_examinedComputer)
                {
                    if (_databaseFixed)
                    {
                        _game.DisplayBeat(458);
                    }
                    else
                        _game.DisplayBeat(457);
                }
                else
                {
                    if (_databaseFixed)
                    {
                        _game.DisplayBeat(458);
                    }
                    else {

                        _game.DisplayBeat(455);
                    }
                    _examinedComputer = true;
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 434)
            {
                if (_calledElevator)
                {
                    _game.DisplayBeat(464);
                }
                else
                {
                    _calledElevator = true;
                    _game.DisplayBeat(463);
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 530)
            {
                if (_churchPurged)
                {
                    _game.DisplayBeat(532);
                }
                else
                {
                    _game.DisplayBeat(531);
                    Score.Died = true;
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 482)
            {
                if (_serverTurnedOn)
                {
                    _game.DisplayBeat(484);
                }
                else
                {
                    _game.DisplayBeat(483);
                }
            }
            else if (_game._beatHistory[_game._beatHistory.Count - 1] == 472)
            {
                if (_manifestation)
                {
                    _game.DisplayBeat(476);
                }
                else
                {
                    _game.DisplayBeat(466);
                }
            }
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 444)
        {
            _game._displayTextOnlyOnce.Add(444);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 450)
        {
            _game._displayTextOnlyOnce.Add(450);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 453)
        {
            _game._displayTextOnlyOnce.Add(453);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 1] == 453)
        {
            _game._displayTextOnlyOnce.Add(453);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 460)
        {
            _game._displayTextOnlyOnce.Add(460);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 466)
        {
            _game._displayTextOnlyOnce.Add(466);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 477)
        {
            _game._displayTextOnlyOnce.Add(477);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 472)
        {
            _game._displayTextOnlyOnce.Add(472);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 482)
        {
            _game._displayTextOnlyOnce.Add(482);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 496)
        {
            _game._displayTextOnlyOnce.Add(496);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 491)
        {
            _game._displayTextOnlyOnce.Add(491);
        }


        if (_game._beatHistory[_game._beatHistory.Count - 2] == 502)
        {
            _game._displayTextOnlyOnce.Add(502);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 517)
        {
            _game._displayTextOnlyOnce.Add(517);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 514)
        {
            _game._displayTextOnlyOnce.Add(514);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 521)
        {
            _game._displayTextOnlyOnce.Add(521);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 524)
        {
            _game._displayTextOnlyOnce.Add(524);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 532)
        {
            _game._displayTextOnlyOnce.Add(532);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 535)
        {
            _game._displayTextOnlyOnce.Add(535);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 539)
        {
            _game._displayTextOnlyOnce.Add(539);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 544)
        {
            _game._displayTextOnlyOnce.Add(544);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 546)
        {
            _game._displayTextOnlyOnce.Add(546);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 546)
        {
            _game._displayTextOnlyOnce.Add(546);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 463)
        {
            _game._displayTextOnlyOnce.Add(463);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 547)
        {
            _game._displayTextOnlyOnce.Add(547);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 447)
        {
            _game._displayTextOnlyOnce.Add(447);
        }

        if (_currentBeatID == 446)
        {
            _game._dontDisplayTheseBeats.Remove(450);
        }

        if (_currentBeatID == 484)
        {
            _databaseFixed = true;
        }

        if (_currentBeatID == 455 || _currentBeatID == 457)
        {
            _game._dontDisplayTheseBeats.Remove(466);
        }

        if (_currentBeatID == 461)
        {
            _unlocked2115And1151 = true;
            _game._dontDisplayTheseBeats.Remove(514);
            _game._dontDisplayTheseBeats.Remove(496);
        }

        if (_currentBeatID == 458)
        {
            Scp093ReceptionPass.SetActive(true);
            Scp093ReceptionPassInput.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if (_currentBeatID == 493)
        {
            Scp093ServerPass.SetActive(true);
            Scp093ServerPassInput.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if (_currentBeatID == 500)
        {
            Scp093PrivateComputer.SetActive(true);
            Scp093PrivateComputerInput.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if (_currentBeatID == 522)
        {
            Scp093LabComputer.SetActive(true);
            Scp093LabComputerInput.GetComponent<TMP_InputField>().ActivateInputField();
        }

        if (_currentBeatID == 533)
        {
            _game._dontDisplayTheseBeats.Remove(535);
        }

        if (_currentBeatID == 540)
        {
            _game._dontDisplayTheseBeats.Remove(544);
        }


        if (_currentBeatID == 510)
        {
            _churchPurged = true;
        }

        if (_currentBeatID == 476)
        {
            _manifestation = true;
        }


        if (_currentBeatID == 513)
        {
            _game._dontDisplayTheseBeats.Remove(530);
        }

        if (_doorUnlocked)
        {
            if (_rubbleRemoved)
            {
                _game._dontDisplayTheseBeats.RemoveAll(item => item == 490);
                int[] beatsAdd = { 489, 487, 488 };
                _game.DontDisplayTheseBeats(beatsAdd.ToList());
            }
            else {
                _game._dontDisplayTheseBeats.RemoveAll(item => item == 488);
                int[] beatsAdd = { 489, 487, 490 };
                _game.DontDisplayTheseBeats(beatsAdd.ToList());
            }
        }
        else
        {
            if (_rubbleRemoved)
            {
                _game._dontDisplayTheseBeats.RemoveAll(item => item == 489);
                int[] beatsAdd = { 488, 490, 487 };
                _game.DontDisplayTheseBeats(beatsAdd.ToList());
            }
            else
            {
                _game._dontDisplayTheseBeats.RemoveAll(item => item == 487);
                int[] beatsAdd = { 488, 489, 490 };
                _game.DontDisplayTheseBeats(beatsAdd.ToList());
            }
        }

        if (_currentBeatID == 485) //open door
        {
            _doorUnlocked = true;
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 485)
        {
            _game._dontDisplayTheseBeats.Add(485);
            _game._dontDisplayTheseBeats.Remove(486);
        }

        if (_game._beatHistory[_game._beatHistory.Count - 2] == 486)
        {
            _game._dontDisplayTheseBeats.Add(486);
            _game._dontDisplayTheseBeats.Remove(485);
        }

        if (_currentBeatID == 487 || _currentBeatID == 488)
        {
            _rubbleRemoved = true;
        }

        if (_currentBeatID == 486) //closed door
        {
            _doorUnlocked = false;
        }

        if (_currentBeatID == 529) 
        {
            _game._GOCTask2 = true;
            _game._dontDisplayTheseBeats.Add(528);
        }

        if (_currentBeatID == 543) 
        {
            _game._insurgencyTask2 = true;
            _game._dontDisplayTheseBeats.Add(543);
        }

        if (_currentBeatID == 547)
        {
            _game._tookFloppyDisk = true;
        }

        if (_game._joinedGOC)
        {
            _game._dontDisplayTheseBeats.Remove(529);
        }

        if (_game._joinedInsurgency)
        {
            _game._dontDisplayTheseBeats.Remove(543);
        }
    }

    public void ReceptionPass()
    {
        if(_scp093ReceptionPassInput == "LT3RS")
        {
            _game.DisplayBeat(460);
        }
        else _game.DisplayBeat(459);

        Scp093ReceptionPass.SetActive(false);
        
    }

    public void ServerPass()
    {
        if (_scp093ServerPassInput == "Lord's Tears")
        {
            _game.DisplayBeat(494);
            _serverTurnedOn = true;
        }
        else _game.DisplayBeat(495);
        Scp093ServerPass.SetActive(false);
    }

    public void PrivateComputer()
    {
        if (_scp093PrivateComputerInput == "LT3RS")
        {
            _game.DisplayBeat(502);
        }
        else _game.DisplayBeat(501);
        Scp093PrivateComputer.SetActive(false);
    }

    public void LabComputer()
    {
        if (_scp093LabComputerInput == "LT3RS")
        {
            _game.DisplayBeat(524);
        }
        else _game.DisplayBeat(523);
        Scp093LabComputer.SetActive(false);
    }
}
