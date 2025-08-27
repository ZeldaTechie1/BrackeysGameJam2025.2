using System;
using UnityEngine;
using System.Collections.Generic;

public class UnflippedMultplier : BasicAOEModifier
{
    public override float ModifyScoreValue(float score)
    {
        int tazosUnflipped = 0;

        if(validTazosInRange!= null)
        {
            foreach (Tazo t in validTazosInRange)
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
