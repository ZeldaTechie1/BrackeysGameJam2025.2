using System;
using UnityEngine;

public class BasicModifier : MonoBehaviour, IModifier
{
    public string ModifierName { get; set; } = "Default Modifier";

    public virtual float ModifyScoreValue(float score)
    {
        Debug.Log($"{ModifierName}: Is this intentional?");

        return score;
    }

    public virtual void TurnEnded()
    {
        Debug.Log($"Not really doing anything.");
    }
}

public interface IAOEModifier: IModifier
{
    float Range { get; set; }

    public void GetObjectsInRange();
}

public interface IConditionalModifier: IModifier
{ 
    Func<bool> Conditional { get; set; }

    public void TestConditional();
}

public interface IAOEConditionalModifier: IConditionalModifier,IAOEModifier
{
    public void TestConditionalInRange();
}

public interface IOverTimeModifier
{
    public void IncreaseEffect();
}

public interface ICounterModifier
{
    public int Counter { get; set; }

    public void AddToCounter() { Counter++; }
    public void RemoveFromCounter() { Counter--; }
    public void ResetCounter() { Counter = 0; }
}
