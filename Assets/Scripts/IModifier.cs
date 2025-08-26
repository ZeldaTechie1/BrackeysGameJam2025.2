using System;
using System.Diagnostics;
using UnityEngine;

public interface IModifier
{
    [SerializeField]public string ModifierName { get; set; }

    public float ModifyScoreValue(float score);

    public void TurnEnded();
}


