using UnityEngine;
using System.Collections.Generic;
using System;

public class ScoreController : MonoBehaviour
{
    public static Action DoneScoring;
    public static Action<int> DeterminedWinner;
    public float playerScore = 0;
    public float opponentScore = 0;

    [SerializeField]SlammerController controller;

    public void Setup()
    {
        TurnHandler.ScoringTazos += ScoreTazos;
        TurnHandler.CheckingForWinner += SelectWinner;
    }

    void ScoreTazos(List<Tazo> currentActiveTazos)
    {
        List<Tazo> flippedTazos = new();
        foreach (Tazo tazo in currentActiveTazos)
        {
            if (tazo.HasBeenFlipped())
            {
                float score = tazo.GetFinalScore();
                if (controller.player == 0)
                    playerScore += score;
                else
                    opponentScore += score;
                
                flippedTazos.Add(tazo);
            }
        }
        foreach (Tazo tazo in flippedTazos)
        {
            tazo.gameObject.SetActive(false);
        }
        DoneScoring();
    }

    void SelectWinner()
    {
        DeterminedWinner?.Invoke(playerScore > opponentScore ? 0: playerScore == opponentScore ? -1 : 1);
    }
}
