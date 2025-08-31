using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public static Action DoneScoring;
    public static Action<int> DeterminedWinner;
    public float playerScore = 0;
    public float opponentScore = 0;

    [SerializeField]SlammerController controller;
    [SerializeField]TextMeshProUGUI playerScoreText;
    [SerializeField]TextMeshProUGUI opponentScoreText;

    public void Setup()
    {
        TurnHandler.ScoringTazos += ScoreTazos;
        TurnHandler.CheckingForWinner += SelectWinner;
        VampiricDrain.RemovePlayerScore += OnRemovePlayerScore;
        VampiricDrain.GetPlayerScore += OnGetPlayerScore;
    }

    private void OnDisable()
    {
        TurnHandler.ScoringTazos -= ScoreTazos;
        TurnHandler.CheckingForWinner -= SelectWinner;
        VampiricDrain.RemovePlayerScore -= OnRemovePlayerScore;
        VampiricDrain.GetPlayerScore -= OnGetPlayerScore;
    }

    private float OnGetPlayerScore(int player)
    {
        return player == 0 ? playerScore : opponentScore;
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

        playerScoreText.text = $"Score: {playerScore}";
        opponentScoreText.text = $"Score: {opponentScore}";

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

    void OnRemovePlayerScore(int player, float score)
    {
        if(player == 0)
        {
            playerScore -= score;
        }
        else
        {
            opponentScore -= score;
        }

        playerScoreText.text = $"Score: {playerScore}";
        opponentScoreText.text = $"Score: {opponentScore}";
    }
}
