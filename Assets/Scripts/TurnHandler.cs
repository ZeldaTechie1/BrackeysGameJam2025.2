using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TurnState
{
    RoundStart,
    ShowingPlayers,
    PlayerStartSlam,
    WaitingForTazos,
    ActivatingModifiers,
    WaitingForModifiers,
    ScoringTazos,
    CheckingIfTazosAreGone,
    CheckingForWinner
}
public class TurnHandler : MonoBehaviour
{
    public TurnState currentState;
    public static Action RoundStarting;
    public static Action ShowingPlayers;
    public static Action PlayerStartSlam;
    public static Action WaitingForTazos;
    public static Action ActivatingModifiers;
    public static Action WaitingForModifiers;
    public static Action<List<Tazo>> ScoringTazos;
    public static Action CheckingIfTazosAreGone;
    public static Action CheckingForWinner;

    [SerializeField] ScoreController scoreController;
    [SerializeField] TazoTracker tazoTracker;
    [SerializeField] SlammerController slammerController;

    private void Awake()
    {
        currentState = TurnState.RoundStart;
        IntroductionHandler.FinishedIntro += OnFinishedIntro;
        SlammerController.SlamCompleted += OnSlamCompleted;
        TazoTracker.TazosDoneMoving += OnTazosDoneMoving;
        ScoreController.DoneScoring += OnDoneScoring;
        TazoTracker.KeepPlaying += OnDoneCheckingIfPlayable;
        ScoreController.DeterminedWinner += OnDeterminedWinner;
    }

    private void Start()
    {
        RoundStarting?.Invoke();

        SetupControllers();

        currentState = TurnState.ShowingPlayers;
        Debug.Log("Showing Players");
        ShowingPlayers?.Invoke();
    }

    public void SetupControllers()
    {
        Debug.Log("Setting up controllers");
        scoreController.Setup();
        tazoTracker.Setup();
        slammerController.Setup();
    }

    void OnFinishedIntro()
    {
        Debug.Log("PlayerSlam");
        currentState = TurnState.PlayerStartSlam;
        PlayerStartSlam?.Invoke();
    }

    private void OnSlamCompleted()
    {
        Debug.Log("Slam Completed, waiting for Tazos");
        currentState = TurnState.WaitingForTazos;
        WaitingForTazos?.Invoke();
    }

    private void OnTazosDoneMoving(List<Tazo> activeTazos)
    {
        Debug.Log("Tazos done moving");
        if (currentState == TurnState.WaitingForTazos)
        {
            Debug.Log("Activating Modifiers");
            currentState = TurnState.ActivatingModifiers;
            ActivatingModifiers?.Invoke();
            foreach (Tazo t in activeTazos)
            {
                t.ActivateModifier();
            }
            Debug.Log("Waiting for Tazos again");
            currentState = TurnState.WaitingForModifiers;
            WaitingForModifiers?.Invoke();
        }
        else if (currentState == TurnState.WaitingForModifiers)
        {
            Debug.Log("Scoring Tazos");
            currentState = TurnState.ScoringTazos;
            ScoringTazos?.Invoke(activeTazos);
        }

    }
    private void OnDoneScoring()
    {
        currentState = TurnState.CheckingIfTazosAreGone;
        Debug.Log("Checking for Tazos");
        CheckingIfTazosAreGone?.Invoke();
    }

    private void OnDoneCheckingIfPlayable(bool keepPlaying)
    {
        if(keepPlaying)
        {
            Debug.Log("Next Player!");
            currentState = TurnState.PlayerStartSlam;
            PlayerStartSlam?.Invoke();
            return;
        }

        currentState = TurnState.CheckingForWinner;
        CheckingForWinner?.Invoke();
    }

    private void OnDeterminedWinner(int playerID)
    {
        if(playerID == -1)
        {
            Debug.Log("It's a tie!");
        }
        else
        {
            Debug.Log($"Winner is {playerID}");
        }
        Application.Quit();
    }
}
