using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public class Cannibal : BasicAOEModifier
{
    public override float ModifyScoreValue(float score)
    {
        float newScore = 0;

        if(validTazosInRange != null && validTazosInRange.Count > 0)
        {
            int randTazo = Random.Range(0, validTazosInRange.Count);
            Tazo t = validTazosInRange[randTazo];
            newScore = t.scoreValue * 2f;
            Debug.Log($"Munch on {t.name}");
            t.gameObject.SetActive(false);
        }

        return newScore;
    }
}


