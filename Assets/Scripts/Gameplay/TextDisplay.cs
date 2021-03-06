using System.Collections;
using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    public enum State { Initialising, Idle, Busy }

    private TMP_Text _displayText;
    private string _displayString;
    private WaitForSeconds _shortWait;
    private WaitForSeconds _longWait;
    private State _state = State.Initialising;
    private int _waitBeforeSkip = 0;
    public bool IsIdle { get { return _state == State.Idle; } }
    public bool IsBusy { get { return _state != State.Idle; } }
    public Game _game;
    private void Awake()
    {
        _displayText = GetComponent<TMP_Text>();
        _shortWait = new WaitForSeconds(0.05f);
        _longWait = new WaitForSeconds(0.1f);

        _displayText.text = string.Empty;
        _state = State.Idle;
    }

    private IEnumerator DoShowText(string text)
    {
        int currentLetter = 0;
        char[] charArray = text.ToCharArray();
            while (currentLetter < charArray.Length)
            {
                _displayText.text += charArray[currentLetter++];

                if (Input.anyKey && _waitBeforeSkip > 5)
                {
                    _game.AudioPlayerSkipWait();
                    _shortWait = new WaitForSeconds(0.005f);
                }
                else if (Input.GetKey("enter"))
                {
                    _game.AudioPlayerSkipWait();
                    yield return 0;
                }
                else
                {
                    yield return _shortWait;
                }
                _waitBeforeSkip++;
            }

            _displayText.text += "\n\n";
            _displayString = _displayText.text;
            _state = State.Idle;
            _waitBeforeSkip = 0;
            _shortWait = new WaitForSeconds(0.05f);
    }

    private IEnumerator DoAwaitingInput()
    {
        bool on = true;

        while (enabled)
        {
            _displayText.text = string.Format( "{0}> {1}", _displayString, ( on ? "|" : " " ));
            on = !on;
            yield return _longWait;
        }
    }

    private IEnumerator DoClearText()
    {
        int currentLetter = 0;
        char[] charArray = _displayText.text.ToCharArray();

        while (currentLetter < charArray.Length)
        {
            if (currentLetter > 0 && charArray[currentLetter - 1] != '\n')
            {
                charArray[currentLetter - 1] = ' ';
            }

            if (charArray[currentLetter] != '\n')
            {
                charArray[currentLetter] = '_';
            }

            _displayText.text = charArray.ArrayToString();
            ++currentLetter;
            yield return null;
        }

        _displayString = string.Empty;
        _displayText.text = _displayString;
        _state = State.Idle;
    }

    public void Display(string text)
    {
        if (_state == State.Idle)
        {
            StopAllCoroutines();
            _state = State.Busy;
            StartCoroutine(DoShowText(text));
        }
    }

    public void ShowWaitingForInput()
    {
        if (_state == State.Idle)
        {
            StopAllCoroutines();
            StartCoroutine(DoAwaitingInput());
        }
    }
    public void Clear()
    {
        if (_state == State.Idle)
        {
            StopAllCoroutines();
            _state = State.Busy;
            StartCoroutine(DoClearText());
        }
    }
}
