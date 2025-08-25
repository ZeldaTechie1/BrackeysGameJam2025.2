using System;
using UnityEngine;
using System.Collections.Generic;

public class BasicAOEModifier : MonoBehaviour, IAOEModifier
{
    public float Range { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string ModifierName { get; set; } = "Default AOE Modifier";

    public virtual void GetObjectsInRange()
    {
        Debug.Log($"Not really doing anything.");
    }

    public virtual float ModifyScoreValue(float score)
    {
        Debug.Log($"{ModifierName}: Is this intentional?");

        return score;
    }

    public virtual void TurnEnded()
    {
        GetObjectsInRange();
    }
}
