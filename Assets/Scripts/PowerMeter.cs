using DG.Tweening;
using NUnit.Framework.Interfaces;
using System;
using Unity.Jobs;
using UnityEngine;

public class PowerMeter : MonoBehaviour
{
    [SerializeField] public bool isCharging { get; private set; }

    [SerializeField]float currentPower;
    Tween currentChargingTween;

    public void StartCharging()
    {
        if(isCharging)
        {
            Debug.LogError("Tried to start charging when meter was already charging");
        }
        isCharging = true;
        currentPower = 0;
        currentChargingTween = DOTween.To(() => currentPower, tweenedValue => currentPower = tweenedValue, 1f, 2f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear).SetDelay(.1f);
    }

    public float StopCharging()
    {
        if(!isCharging)
        {
            Debug.LogError("Tried to stop charging when meter wasn't already charging");
        }
        isCharging = false;
        currentChargingTween.Kill();
        return currentPower;
    }
}
