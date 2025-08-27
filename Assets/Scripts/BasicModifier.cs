using System;
using UnityEngine;

public class BasicModifier : MonoBehaviour, IModifier
{
    public string ModifierName = "Default Modifier";

    public virtual float ModifyScoreValue(float score)
    {
        Debug.Log($"{ModifierName}: Is this intentional?");

        return score;
    }

    public virtual void ActivateModifier()
    {
        Debug.Log($"Not really doing anything.");
    }
}

public interface IAOEModifier: IModifier
{
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
    [SerializeField]public int Counter { get; set; }

    public void AddToCounter() { Counter++; }
    public void RemoveFromCounter() { Counter--; }
    public void ResetCounter() { Counter = 0; }
}
