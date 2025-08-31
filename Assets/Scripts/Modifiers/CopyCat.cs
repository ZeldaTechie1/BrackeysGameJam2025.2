using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class CopyCat : BasicAOEModifier
{

    Component copiedModifier;
    public override void ActivateModifier()
    {
        base.ActivateModifier();

        if (validTazosInRange != null && validTazosInRange.Count > 0)
        {
            int randTazo = Random.Range(0, validTazosInRange.Count);
            List<MonoBehaviour> modifiersOnTazo = validTazosInRange[randTazo].GetComponents<MonoBehaviour>().Where(x=> x is IModifier).ToList();
            int randModifier = Random.Range(0, modifiersOnTazo.Count);
            System.Type type = modifiersOnTazo[randModifier].GetType();
            copiedModifier = gameObject.AddComponent(type);
            Debug.Log($"Copied {copiedModifier}");
            if (copiedModifier is BasicAOEModifier aoe)
            {
                aoe.Range = this.Range;
            }
            (copiedModifier as IModifier).ActivateModifier();
        }
    }

    public override float ModifyScoreValue(float score)
    {
        float newScore = (copiedModifier as IModifier).ModifyScoreValue(score);
        return newScore;
    }
}
