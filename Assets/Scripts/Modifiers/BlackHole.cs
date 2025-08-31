using System.Collections.Generic;
using UnityEngine;

public class BlackHole : BasicAOEModifier
{
    public override float ModifyScoreValue(float score)
    {
        int tazosUnflipped = 0;

        if (validTazosInRange != null)
        {
            foreach (Tazo t in validTazosInRange)
            {
                if (!t.HasBeenFlipped())
                {
                    tazosUnflipped++;
                }
            }
        }

        return Mathf.Clamp(tazosUnflipped, 1, int.MaxValue) * score;
    }

    public override void ActivateModifier()
    {
        base.ActivateModifier();
        foreach(Tazo t in validTazosInRange)
        {
            t.rb.AddForce(((transform.position - t.transform.position).normalized * 200) + (Vector3.up * 100));
        }
    }
}

