using System;
using System.Diagnostics;

public interface Modifier
{
    public string ModifierName { get; set; }

    public float ModifyScoreValue(float score);
}


