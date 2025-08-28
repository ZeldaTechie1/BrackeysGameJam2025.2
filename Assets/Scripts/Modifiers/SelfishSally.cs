using UnityEngine;

public class SelfishSally : BasicAOEModifier
{
    public override float ModifyScoreValue(float score)
    {
        if(validTazosInRange != null)
        {
            foreach (Tazo t in validTazosInRange)
            {
                if(t.HasBeenFlipped())
                {
                    score *= .5f;
                }
            }
        }
        

        return score;
    }
}
