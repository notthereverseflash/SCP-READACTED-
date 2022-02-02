using System;
using UnityEngine;

[Serializable]
public class ChoiceData
{
    [SerializeField] private string _text;
    [SerializeField] private int _beatId;
    private bool _disabled;

    public string DisplayText { get { return _text; } }
    public int NextID { get { return _beatId; } }
    public bool Disabled { get { return _disabled; } set { _disabled = value; } }
}

