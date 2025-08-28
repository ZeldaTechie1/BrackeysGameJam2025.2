using System;
using UnityEngine;
using System.Collections.Generic;

public class BasicAOEModifier : MonoBehaviour, IAOEModifier
{
    public float Range;
    public string ModifierName = "Default AOE Modifier";
    
    protected List<Tazo> validTazosInRange = new();
    public virtual void GetObjectsInRange()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, Range);
        validTazosInRange.Clear();
        foreach (Collider collider in objectsInRange)
        {
            Tazo t = null;
            collider.TryGetComponent<Tazo>(out t);
            if (t != null && t.gameObject != this.gameObject)
            {
                validTazosInRange.Add(t);
            }
        }
    }

    public virtual float ModifyScoreValue(float score)
    {
        Debug.Log($"{ModifierName}: Is this intentional?");

        return score;
    }

    public virtual void ActivateModifier()
    {
        GetObjectsInRange();
    }
}
