using UnityEngine;

public class Slasher : BasicAOEModifier
{
    public override void ActivateModifier()
    {
        base.ActivateModifier();
        if (validTazosInRange != null && validTazosInRange.Count > 0)
        {
            foreach (Tazo t in validTazosInRange)
            {
                if (t.Series != Series.Halloween)
                {
                    Debug.Log($"Slashed at {t.name}");
                    t.rb.AddForce(-(((transform.position - t.transform.position).normalized * 200) + (Vector3.up * 100)));
                }
            }
        }
    }
}


