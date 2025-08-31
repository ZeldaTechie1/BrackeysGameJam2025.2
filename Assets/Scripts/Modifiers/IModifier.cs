using System;
using System.Diagnostics;
using UnityEngine;

public interface IModifier
{
    public float ModifyScoreValue(float score);

    public void ActivateModifier();
}


