using UnityEngine;

public class PinHead : BasicAOEModifier
{
    public override void ActivateModifier()
    {
        base.ActivateModifier();
        if (validTazosInRange != null && validTazosInRange.Count > 0)
        {
            int randomTaz = Random.Range(0, validTazosInRange.Count);
            Tazo t = validTazosInRange[randomTaz];
            Debug.Log($"suck {t.name}");
            t.rb.AddForce(((transform.position - t.transform.position).normalized * 200) + (Vector3.up * 100));
        }
    }
}


