using Random = UnityEngine.Random;

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
            t.gameObject.SetActive(false);
        }

        return newScore;
    }
}

