using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BeatData
{
    [SerializeField] private List<ChoiceData> _choices;
    [SerializeField] private string _text;
    [SerializeField] private int _id;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private float _waitBeforePlaying;

    public AudioClip PlayClip { get { return _clip; } }
    public float WaitForSeconds { get { return _waitBeforePlaying; } }
    public List<ChoiceData> Decision { get { return _choices.FindAll(b => !b.Disabled); } }
    public List<ChoiceData> AllDecisions { get { return _choices; } }
    public string DisplayText { get { return _text; } }
    public int ID { get { return _id; } }
}