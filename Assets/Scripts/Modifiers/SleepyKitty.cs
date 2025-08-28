using UnityEngine;

public class SleepyKitty : BasicModifier
{
    [Range(0f,100f)]public float sleepyPercent;
    public bool isAsleep = false;

    public override void ActivateModifier()
    {
        isAsleep = Random.Range(0f, 100f) > sleepyPercent;
    }

    public override float ModifyScoreValue(float score)
    {
        return score * (isAsleep?.5f:1);
    }
}
