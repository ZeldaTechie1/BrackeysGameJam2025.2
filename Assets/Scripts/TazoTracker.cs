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
    public GameObject TazoContainer;
    public List<Tazo> activeTazos { get; private set; }

    [SerializeField] List<Tazo> allTazos;
    bool checkingForTazosMoving;

    public void Setup()
    {
        TurnHandler.WaitingForTazos += OnCheckForMovement;
        TurnHandler.WaitingForModifiers += OnCheckForMovement;
        TurnHandler.CheckingIfTazosAreGone += CheckForActiveTazos;
        TurnHandler.PlayerStartSlam += UpdateActiveTazos;
        allTazos = TazoContainer.GetComponentsInChildren<Tazo>().ToList();
        activeTazos = allTazos;
    }

    private void UpdateActiveTazos()
    {
        activeTazos = activeTazos.Where(x => x.gameObject.activeSelf).ToList();
    }

    private void Update()
    {
        if(checkingForTazosMoving)
        {
            bool allRigidbodiesSleeping = true;
            //Debug.Log("Rigidbody bool reset");
            foreach(Tazo t in activeTazos)
            {
               if(t.rb.linearVelocity.magnitude > .01f || t.rb.angularVelocity.magnitude > .01f)
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
    }

    void CheckForActiveTazos()
    {
        UpdateActiveTazos();
        KeepPlaying?.Invoke(activeTazos.Count > 0);
    }
}
