using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    [SerializeField] private StoryData _data;
    [SerializeField] public GameObject _mainMenu;
    [SerializeField] public GameObject _gameOver;
    [SerializeField] public GameObject _form;
    [SerializeField] public GameObject _scpNumber;
    [SerializeField] public GameObject _scp261Number;
    [SerializeField] public GameObject _scp261Coins;
    [SerializeField] public GameObject _scp662Money;
    [SerializeField] public GameObject _scp662Keypad1;
    [SerializeField] public GameObject _scp662Keypad2;
    [SerializeField] public GameObject _scp093ReceptionPass;
    [SerializeField] public GameObject _scp093ServerPass;
    [SerializeField] public GameObject _scp093PrivateComputer;
    [SerializeField] public GameObject _scp093LabComputer;
    [SerializeField] public GameObject _SCPs;
    [SerializeField] public GameObject _credits;
    [SerializeField] public GameObject _controls;
    [SerializeField] public GameObject _currentCoins;
    [SerializeField] public TMP_Text _currentBeatDisplay; //Debug current beat data
    [SerializeField] public AudioClip[] _audioClips; //Audios
    [SerializeField] public AudioManager _audioManager;

    public List<int> _dontDisplayTheseBeats;
    public List<int> _displayTextOnlyOnce;
    public bool _canAccessEscapeButton = false;
    public List<int> _beatHistory;
    public bool IgnoreInputs = false;
    public string _currentSCPNumber = "066";
    public bool _joinedInsurgency = false;
    public bool _joinedGOC = false;
    public bool _triedEscape = false;
    public bool _insurgencyTask1 = false;
    public bool _GOCTask1 = false;
    public bool _insurgencyTask2 = false;
    public bool _GOCTask2 = false;
    public bool _tookFloppyDisk = false;
    public AudioClip _currentAudioClipBeingPlayed;
    public int _SCPEncounterNumber = 1;
    public BeatData _currentBeat;

    private List<SCPScript> _SCPList;
    private TextDisplay _output;
    private TMP_InputField[] _formInputs;
    private bool MainMenu = false;
    private int _currentInputField = 0;
    private bool _inputReset = false;
    private SCPScript _currentSCP;
    private int CurrentEncounterStartBeat;
    private bool _allValuesHaveBeenReset = true;
    private IEnumerator _currentAudioCoroutine;
    private LivingQuarters _livingQuarters;

    public ScoreTypes Score = new ScoreTypes
    {
        Deaths = 0,
        Incorrect_SCP = 0,
        Incorrect_Containment = 0,
        Bonus_Info = false,
        Died = false,
        Wrong_SCP = true
    };

    public void DontDisplayTheseBeats(List<int> beatList, bool Add = true)
    {
        if(Add)
            beatList.ForEach(value => _dontDisplayTheseBeats.Add(value));
        else
            beatList.ForEach(value => _dontDisplayTheseBeats.Remove(value));
    }

    private void Awake()
    {
        _output = GetComponentInChildren<TextDisplay>();
        _currentBeat = null;
        _gameOver.SetActive(false);
        _form.SetActive(false);
        _scpNumber.SetActive(false);
        _scp261Number.SetActive(false);
        _scp261Coins.SetActive(false);
        _scp662Money.SetActive(false);
        _scp662Keypad1.SetActive(false);
        _scp662Keypad2.SetActive(false);
        _scp093ServerPass.SetActive(false);
        _scp093ReceptionPass.SetActive(false);
        _scp093PrivateComputer.SetActive(false);
        _scp093LabComputer.SetActive(false);
        _formInputs = _form.gameObject.GetComponentsInChildren<TMP_InputField>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _audioManager.MainTheme();

        _SCPList = new List<SCPScript>();
        _beatHistory = new List<int>() { 0 };
        _dontDisplayTheseBeats = new List<int>();
        _displayTextOnlyOnce = new List<int>();
        _joinedInsurgency = false;
        _joinedGOC = false;

        foreach (SCPScript element in _SCPs.GetComponents(typeof(SCPScript))) //load the SCP scripts into a list
        {
            if (element != null)
            {
                _SCPList.Add(element);
            }
        }
        foreach (BeatData newData in _data.GetAllBeats())
        { 
            for (int count = 0; count < newData.AllDecisions.Count; ++count) // reset all disabled choices to false
            {
                newData.AllDecisions[count].Disabled = false;
            }
        }
    }

    private void Update()
    {
        if (!IgnoreInputs) { 
            if (MainMenu)
            {
                if (_output.IsIdle)
                {
                    if (_currentBeat != null)
                    {
                        if (_currentBeat.ID != -3) { UpdateInput(); }
                    }
                    if (_canAccessEscapeButton && Input.GetKey("b") && _currentBeat.ID > 1 && _currentBeat.ID != 600 && _currentBeat.ID != 601 && (_currentBeat.ID < 51 || _currentBeat.ID > 68) && (_currentBeat.ID < 137 || _currentBeat.ID > 165)) //Player has accessed the escape button
                    {
                        DisplayBeat(-4);
                    }
                }

                if ((_currentBeat.ID == -2 || _currentBeat.ID == -31) && (Input.GetKeyDown(KeyCode.Return)))
                {
                    MainMenu = false;
                    _mainMenu.SetActive(true);
                    _gameOver.SetActive(false);
                    _credits.SetActive(false);
                    _audioManager.MainTheme();
                }

                if ( _currentBeat.ID == -32 && (Input.GetKeyDown(KeyCode.Return)))
                {
                    MainMenu = false;
                    _mainMenu.SetActive(true);
                    _controls.SetActive(false);
                }
            }
            else UpdateMainMenu();

            if (Input.GetKey(KeyCode.Escape)) //Displays form
            {
                _formInputs[_currentInputField].DeactivateInputField();
                _currentInputField = 0;
                _formInputs[_currentInputField].ActivateInputField();
                _inputReset = true;
            }
        }
    }

    public void AudioPlayer(AudioClip clip, float waitForSeconds) //play the audio with delay
    {
        _currentAudioCoroutine = DelayAudioClips(clip, waitForSeconds);
        StartCoroutine(DelayAudioClips(clip, waitForSeconds));
    }

    public void AudioPlayerSkipWait() //stop the previous co-routine or audioSource and start the audio courotine immediatley
    {
        if (!_audioManager.voiceActingSource.isPlaying)
        {
            _audioManager.StopVoiceActingClip();
            StopCoroutine(_currentAudioCoroutine);
            _audioManager.StartVoiceActingClip(_currentAudioClipBeingPlayed);
        }
    }

    IEnumerator DelayAudioClips(AudioClip clip, float waitForSeconds) //play clip after a delay
    {
        yield return new WaitForSeconds(waitForSeconds);
        _audioManager.StartVoiceActingClip(clip);
    }

    private void UniqueBeats()
    {
        if (_currentBeat.ID == -1) //Brings it to the main menu
        {
            MainMenu = false;
            _mainMenu.SetActive(true);
            Score.Deaths = 0;
            Score.Incorrect_SCP = 0;
            Score.Incorrect_Containment = 0;
            Score.Died = false;
        }

        if (_currentBeat.ID == -29) //Brings it to the main menu without setting the MainMenu to False
        {
            Score.Deaths = 0;
            Score.Incorrect_SCP = 0;
            Score.Incorrect_Containment = 0;
            Score.Died = false;
        }
        if (_currentBeat.ID == -2) //Game Over
        {
            MainMenu = true;
            _mainMenu.SetActive(false);
            _gameOver.SetActive(true);
            ResetAllValues();
            _audioManager.GameOverTheme();
        }

        if (_currentBeat.ID == -3) //Form Fill In
        {
            _form.SetActive(true);
            _formInputs[_currentInputField].ActivateInputField();
        }

        if (_currentBeat.ID == -7) //Go back to the previous choice x2
        {
            DisplayBeat(_beatHistory[_beatHistory.Count - 2]);
            _beatHistory.Remove(_beatHistory[_beatHistory.Count - 1]);
        }
        if (_currentBeat.ID == -8) //Go back to the previous choice if prev beat is negative
        {
            DisplayBeat(_beatHistory[_beatHistory.Count - 1]);
        }
        if (_currentBeat.ID == -31) //Credits
        {
            _credits.SetActive(true);
            _audioManager.Credits();
        }
        if (_currentBeat.ID == -32) //Options
        {
            _controls.SetActive(true);
        }
        SCPEncounters(_currentBeat.ID);
    }

    private void SCPEncounters(int _currentBeatID)
    {

        if (_currentBeatID == 51 || _currentBeatID == 79 || _currentBeatID == 93 || _currentBeatID == 101 || _currentBeatID == 113 ) //Post encounters and Living quarters has begun
        {
            _currentSCP = _SCPList[1];
            CurrentEncounterStartBeat = _currentBeatID;
            _audioManager.AccomodationTheme();
        }
        if (_currentBeatID == 26) //SCP 066 encounter has begun
        {
            _currentSCP = _SCPList[0];
            _currentSCPNumber = "066";
            CurrentEncounterStartBeat = _currentBeatID;
            _audioManager.SCP066Theme();
        }
        if (_currentBeatID == 166) //SCP 035 encounter has begun
        {
            _currentSCP = _SCPList[2];
            _currentSCPNumber = "035";
            _SCPEncounterNumber = 2;
            CurrentEncounterStartBeat = _currentBeatID;
            _audioManager.SCP035Theme();
        }
        if (_currentBeatID == 218) //SCP 249 encounter has begun
        {
            _currentSCP = _SCPList[3];
            _currentSCPNumber = "249";
            _SCPEncounterNumber = 3;
            CurrentEncounterStartBeat = _currentBeatID;
            _audioManager.SCP249Theme();
        }
        if (_currentBeatID == 274) //SCP 261 encounter has begun
        {
            _currentSCP = _SCPList[4];
            _currentSCPNumber = "261";
            _SCPEncounterNumber = 4;
            CurrentEncounterStartBeat = _currentBeatID;
            _audioManager.SCP261Theme();
        }
        if (_currentBeatID == 336) //SCP 662 encounter has begun
        {
            _currentSCP = _SCPList[5];
            _currentSCPNumber = "662";
            _SCPEncounterNumber = 5;
            CurrentEncounterStartBeat = _currentBeatID;
            _audioManager.SCP662Theme();
        }
        if (_currentBeatID == 441) //SCP 093 encounter has begun
        {
            _currentSCP = _SCPList[6];
            _currentSCPNumber = "093";
            _SCPEncounterNumber = 6;
            CurrentEncounterStartBeat = _currentBeatID;
            _audioManager.SCP093Theme();
        }

        if (_currentSCP != null) //An encounter has begun
        {
            _currentSCP.UpdateCurrentBeat(_currentBeatID, CurrentEncounterStartBeat, Score);
        }
    }

    public void UpdateSelectedInputField()
    {
        _currentInputField = 0;
        _form.SetActive(false);
        MainMenu = true;
        IgnoreInputs = false;
        DisplayBeat(8);
    }

    public void UpdateSelectedInputField2()
    {
        _currentInputField++;
        _formInputs[_currentInputField].ActivateInputField();
        IgnoreInputs = true;
    }

    private void UpdateMainMenu()
    {
        var input = Input.inputString;
        switch (input)
        {
            case "1":
                _allValuesHaveBeenReset = false;
                MainMenu = true;
                _mainMenu.SetActive(false);
                DisplayBeat(1);
                break;

            case "2":
                _allValuesHaveBeenReset = false;
                MainMenu = true;
                _mainMenu.SetActive(false);
                DisplayBeat(-29);
                break;

            case "3":
                MainMenu = true;
                _mainMenu.SetActive(false);
                DisplayBeat(-32);
                break;

            case "4":
                MainMenu = true;
                _mainMenu.SetActive(false);
                DisplayBeat(-31);
                break;

            case "5":
                Application.Quit();
                break;

            default:
                break;
        }
    }

    private void ResetAllValues()
    {
        if(!_allValuesHaveBeenReset)
        {
            Score.Deaths = 0;
            Score.Incorrect_SCP = 0;
            Score.Incorrect_Containment = 0;
            Score.Bonus_Info = false;
            Score.Died = false;
            Score.Wrong_SCP = true;

            _dontDisplayTheseBeats.Clear();
            _displayTextOnlyOnce.Clear();
            _beatHistory = new List<int>() { 0 };
            _canAccessEscapeButton = false;

            IgnoreInputs = false;
            _currentSCPNumber = "066";
            _joinedInsurgency = false;
            _joinedGOC = false;
            _triedEscape = false;
            _insurgencyTask1 = false;
            _GOCTask1 = false;
            _insurgencyTask2 = false;
            _GOCTask2 = false;
            _tookFloppyDisk = false;
        }        
    }
    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentBeat != null)
            {
                if (_currentBeat.ID == 1)
                {
                    Application.Quit();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            for (int count = 0; count < _currentBeat.Decision.Count; ++count)
            {
                if(_currentBeat.Decision[count].DisplayText == "Continue." || _currentBeat.Decision.Count == 1) //if a player presses any button it automatically selects the continue choice
                {
                    ChoiceData choice = _currentBeat.Decision[count];
                    DisplayBeat(choice.NextID);
                }
            }
        }
        else
        {
            KeyCode alpha = KeyCode.Alpha1;
            KeyCode keypad = KeyCode.Keypad1;

            for (int count = 0; count < _currentBeat.Decision.Count; ++count)
            {
                if (alpha <= KeyCode.Alpha9 && keypad <= KeyCode.Keypad9)
                {
                    if (Input.GetKeyDown(alpha) || Input.GetKeyDown(keypad))
                    {
                        ChoiceData choice = _currentBeat.Decision[count];
                        DisplayBeat(choice.NextID);
                        break;
                    }
                }
                ++alpha;
                ++keypad;
            }
        }
    }

    public void DisplayBeat(int id)
    {
        if (id > 0 && id != _beatHistory[_beatHistory.Count - 1]) //adds a history of beats in case the program needs prev ones
        { 
            _beatHistory.Add(id);
        }
        _currentBeatDisplay.text = "Current Beat number: " + id;  //Debug current beat data
        BeatData newData = _data.GetBeatById(id);
        for (int count = 0; count < newData.AllDecisions.Count; ++count)
        {
            foreach (int element in _dontDisplayTheseBeats) //sets all choices that have a matching id in dontDisplayTheseBeats to disabled until they are not present in said variable
            {
                if (newData.AllDecisions[count].NextID == element)
                {
                    newData.AllDecisions[count].Disabled = true;
                    break;
                }
                else newData.AllDecisions[count].Disabled = false;
            }
        }

        StartCoroutine(DoDisplay(newData));
        _currentBeat = newData;
        UniqueBeats();
    }

    private IEnumerator DoDisplay(BeatData data)
    {
        _audioManager.StopVoiceActingClip();
        bool _forbiddenText = false;
        AudioPlayer(data.PlayClip, data.WaitForSeconds);
        if (data.PlayClip != null)
        {
            _currentAudioClipBeingPlayed = data.PlayClip;
        }
        _output.Clear();

        while (_output.IsBusy)
        {
            yield return null;
        }
        foreach (int element in _displayTextOnlyOnce) //if the current beatID's text should be shown only once do so
        {
            if (_currentBeat.ID == element)
            {
                _forbiddenText = true;
            }
        }

        if (!_forbiddenText) _output.Display(data.DisplayText);

        while (_output.IsBusy)
        {
            yield return null;
        }

        for (int count = 0; count < data.Decision.Count; ++count)
        {
            ChoiceData choice = data.Decision[count];
            if (!data.Decision[count].Disabled) _output.Display(string.Format("{0}: {1}", (count + 1), choice.DisplayText));
            
            while (_output.IsBusy)
            {
                yield return null;
            }
        }

        if (data.Decision.Count > 0)
        {
            _output.ShowWaitingForInput();
        }
    }
}
