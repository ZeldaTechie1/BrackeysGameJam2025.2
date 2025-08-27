using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using DG.Tweening;
using System.Linq;

public class TazoTracker : MonoBehaviour
{
    public static Action<List<Tazo>> TazosDoneMoving;
    public static Action<bool> KeepPlaying;

    [SerializeField] List<Tazo> allTazos;
    [SerializeField] List<Tazo> activeTazos;

    bool checkingForTazosMoving;

    public void Setup()
    {
        TurnHandler.WaitingForTazos += OnCheckForMovement;
        TurnHandler.WaitingForModifiers += OnCheckForMovement;
        TurnHandler.CheckingIfTazosAreGone += CheckForActiveTazos;
        activeTazos = allTazos;
    }

    

    private void Update()
    {
        if(checkingForTazosMoving)
        {
            bool allRigidbodiesSleeping = true;
            Debug.Log("Rigidbody bool reset");
            foreach(Tazo t in activeTazos)
            {
               if(!t.RigidbodySleeping() || t.rb.linearVelocity.magnitude > .01f)
               {
                    allRigidbodiesSleeping = false;
                    Debug.Log($"{t.name} is still movin' with velocity {t.rb.linearVelocity} and angular velocity {t.rb.angularVelocity}");
               }
            }
            if (allRigidbodiesSleeping)
            {
                checkingForTazosMoving = false;
                TazosDoneMoving(activeTazos);
            }
        }
    }

    void OnCheckForMovement()
    {
        Debug.Log("Checking for movement");
        checkingForTazosMoving = true;
        activeTazos = activeTazos.Where(x=>x.gameObject.activeSelf).ToList();
    }

    void CheckForActiveTazos()
    {
        KeepPlaying?.Invoke(activeTazos.Count > 0);
    }
}
