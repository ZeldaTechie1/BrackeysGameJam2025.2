using System.Diagnostics;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public class TrickOrTreat : BasicAOEModifier
{
    public override float ModifyScoreValue(float score)
    {
        float newScore = score * (Random.Range(0, 2) == 0 ? .5f : 2f);
        return newScore;
    }
}


