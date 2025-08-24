using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ScoreController : MonoBehaviour
{
    public float score = 0;

    private void Awake()
    {
        TazoTracker.OnTazosDoneMoving += ScoreTazos;
    }

    void ScoreTazos(List<Tazo> currentActiveTazos)
    {
        foreach(Tazo tazo in currentActiveTazos)
        {
            if(tazo.HasBeenFlipped())
            {
                tazo.gameObject.SetActive(false);
                score += tazo.GetFinalScore();
            }
        }
    }
}
