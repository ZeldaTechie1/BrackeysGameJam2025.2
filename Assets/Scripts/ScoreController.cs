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
        List<Tazo> flippedTazos = new();
        foreach(Tazo tazo in currentActiveTazos)
        {
            if(tazo.HasBeenFlipped())
            {
                score += tazo.GetFinalScore();
                flippedTazos.Add(tazo);
            }
        }
        foreach(Tazo tazo in flippedTazos)
        {
            tazo.gameObject.SetActive(false);
        }
    }
}
