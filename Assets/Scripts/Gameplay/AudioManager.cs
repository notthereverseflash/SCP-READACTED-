using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip _mainTheme1;
    public AudioClip _mainTheme2;
    public AudioClip _credits;
    public AudioClip _scp066Theme;
    public AudioClip _scp035Theme;
    public AudioClip _scp093Theme;
    public AudioClip _accomodationTheme;
    public AudioClip _finalDecisionTheme;
    public AudioClip _scp261Theme;
    public AudioClip _scp662Theme;
    public AudioClip _scp249Theme;
    public AudioClip _gameOverTheme;
    public AudioSource backgroundMusicSource;
    public AudioSource voiceActingSource;
    public List<AudioClip> _audioClipHistory;
    public Game _game;

    private IEnumerator _currentSong;
    void Start()
    {
        backgroundMusicSource.loop = true;
        _currentSong = playMusic(_mainTheme1, _mainTheme2);
    }

    public void MainTheme()
    {
        if (_currentSong != playMusic(_mainTheme1, _mainTheme2) )
        {
            if (_currentSong != null)
            {
                StopCurrentSong();
            }
            _currentSong = playMusic(_mainTheme1, _mainTheme2);
            StartCoroutine(playMusic(_mainTheme1, _mainTheme2));
        }
    }

    public void Credits()
    {
        if (_currentSong != playMusic(_credits, null))
        {
            StopCurrentSong();
            _currentSong = playMusic(_credits, null);
            StartCoroutine(playMusic(_credits, null));
        }
    }

    public void SCP066Theme()
    {
        if (_currentSong != playMusic(_scp066Theme, null))
        {
            StopCurrentSong();
            _currentSong = playMusic(_scp066Theme, null);
            StartCoroutine(playMusic(_scp066Theme, null));
        }
    }

    public void SCP035Theme()
    {
        if (_currentSong != playMusic(_scp035Theme, null))
        {
            StopCurrentSong();
            _currentSong = playMusic(_scp035Theme, null);
            StartCoroutine(playMusic(_scp035Theme, null));
        }
    }

    public void SCP093Theme()
    {
        if (_currentSong != playMusic(_scp035Theme, null))
        {
            StopCurrentSong();
            _currentSong = playMusic(_scp093Theme, null);
            StartCoroutine(playMusic(_scp093Theme, null));
        }
    }

    public void AccomodationTheme()
    {
        if (_currentSong != playMusic(_accomodationTheme, null))
        {
            StopCurrentSong();
            _currentSong = playMusic(_accomodationTheme, null);
            StartCoroutine(playMusic(_accomodationTheme, null));
        }
    }

    public void SCP261Theme()
    {
        if (_currentSong != playMusic(_scp261Theme, null))
        {
            StopCurrentSong();
            _currentSong = playMusic(_scp261Theme, null);
            StartCoroutine(playMusic(_scp261Theme, null));
        }
    }

    public void SCP662Theme()
    {
        if (_currentSong != playMusic(_scp662Theme, null))
        {
            StopCurrentSong();
            _currentSong = playMusic(_scp662Theme, null);
            StartCoroutine(playMusic(_scp662Theme, null));
        }
    }

    public void SCP249Theme()
    {
        if (_currentSong != playMusic(_scp249Theme, null))
        {
            StopCurrentSong();
            _currentSong = playMusic(_scp249Theme, null);
            StartCoroutine(playMusic(_scp249Theme, null));
        }
    }

    public void FinalDecisionTheme()
    {
        if (_currentSong != playMusic(_finalDecisionTheme, null))
        {
            StopCurrentSong();
            _currentSong = playMusic(_finalDecisionTheme, null);
            StartCoroutine(playMusic(_finalDecisionTheme, null));
        }
    }

    public void GameOverTheme()
    {
        if (_currentSong != playMusic(_gameOverTheme, null))
        {
            StopCurrentSong();
            _currentSong = playMusic(_gameOverTheme, null);
            StartCoroutine(playMusic(_gameOverTheme, null));
        }
    }

    public void StartVoiceActingClip(AudioClip voiceActing)
    {
        if (voiceActing != _audioClipHistory[_audioClipHistory.Count - 1] && voiceActing != null) // avoid repeating the same voiceline twice
        {
            _audioClipHistory.Add(voiceActing);
            voiceActingSource.clip = voiceActing;
            voiceActingSource.Play();
        }
    }

    public void StopVoiceActingClip()
    {
        voiceActingSource.Stop();
    }

    private void StopCurrentSong()
    {
        StopCoroutine(_currentSong);
    }

    IEnumerator playMusic(AudioClip song1, AudioClip song2)
    {
        backgroundMusicSource.clip = song1;
        backgroundMusicSource.Play();
        yield return new WaitForSeconds(backgroundMusicSource.clip.length);
        if (song2)
        {
            backgroundMusicSource.clip = song2;
            backgroundMusicSource.Play();
        }
    }
}