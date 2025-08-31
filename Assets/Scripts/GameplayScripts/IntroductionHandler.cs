using DG.Tweening;
using System;
using UnityEngine;

public class IntroductionHandler : MonoBehaviour
{
    public static Action FinishedIntro;
    
    private void Awake()
    {
        TurnHandler.ShowingPlayers += ShowPlayers;
    }

    void ShowPlayers()
    {
        DOTween.Sequence().AppendInterval(1f).AppendCallback(()=>FinishedIntro());//dostuff
    }

    private void OnDisable()
    {
        TurnHandler.ShowingPlayers -= ShowPlayers;
    }
}
