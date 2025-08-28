using System.Collections.Generic;
using UnityEngine;

public class RuffRuff : BasicAOEModifier
{
    int nonOwnerTazos = 0;
    public override float ModifyScoreValue(float score)
    {
        return score + (nonOwnerTazos);
    }
    public override void ActivateModifier()
    {
        base.ActivateModifier();
        Tazo parentTazo = GetComponent<Tazo>();
        List<Tazo> strangerTazos = new();
        nonOwnerTazos = 0;

        if (validTazosInRange != null)
        {
            foreach (Tazo t in validTazosInRange)
            {
                if (t.Owner != parentTazo.Owner)
                {
                    strangerTazos.Add(t);
                    nonOwnerTazos++;
                }
            }
        }

        foreach(Tazo t in strangerTazos)
        {
            Debug.Log($"Applying force to {t.name}");
            t.rb.AddForce(-(((transform.position - t.transform.position).normalized * 200) + (Vector3.up * 100)));
        }
    }
}

