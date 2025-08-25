using System;
using UnityEngine;
using System.Collections.Generic;

public class UnflippedMultplier : BasicAOEModifier
{
    public new float Range { get; set; } = 10;
    public new string ModifierName { get; set; } = "BingBong";

    List<Tazo> validObjectsInRange = new();

    public override void GetObjectsInRange()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, Range);
        validObjectsInRange.Clear();
        foreach (Collider collider in objectsInRange)
        {
            Tazo t = null;
            collider.TryGetComponent<Tazo>(out t);
            if(t != null)
            {
                validObjectsInRange.Add(t);
            }
        }
    }

    public override float ModifyScoreValue(float score)
    {
        int tazosUnflipped = 0;

        if(validObjectsInRange!= null)
        {
            foreach (Tazo t in validObjectsInRange)
            {
                if (!t.HasBeenFlipped())
                {
                    tazosUnflipped++;
                }
            }
        }

        return Mathf.Clamp(tazosUnflipped,1,int.MaxValue) * score;
    }
}
