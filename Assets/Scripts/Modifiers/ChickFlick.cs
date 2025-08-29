using UnityEngine;

public class ChickFlick:BasicAOEModifier
{
    public override float ModifyScoreValue(float score)
    {
        return score;
    }

    public override void ActivateModifier()
    {
        base.ActivateModifier();
        
        if (validTazosInRange != null && validTazosInRange.Count != 0)
        {
            int randTazo = Random.Range(0, validTazosInRange.Count);
            Tazo t = validTazosInRange[randTazo];
            t.rb.AddForceAtPosition(Vector3.up * 40,this.transform.position);
        }

    }
}

