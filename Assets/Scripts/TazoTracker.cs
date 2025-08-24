using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using DG.Tweening;
using System.Linq;

public class TazoTracker : MonoBehaviour
{
    public static Action<List<Tazo>> OnTazosDoneMoving;

    [SerializeField] List<Tazo> allTazos;
    [SerializeField] List<Tazo> activeTazos;

    bool hasSlammed;

    public void Awake()
    {
        SlammerController.OnSlamComplete += OnSlamCompleted;
        activeTazos = allTazos;
    }

    private void Update()
    {
        if(hasSlammed)
        {
            bool allRigidbodiesSleeping = true;
            foreach(Tazo t in activeTazos)
            {
               if(!t.RigidbodySleeping())
               {
                    allRigidbodiesSleeping = false;
                    Debug.Log($"{t.name} is still movin'");
               }
            }
            if (allRigidbodiesSleeping)
            {
                OnTazosDoneMoving(activeTazos);
                hasSlammed = false;
            }
        }
    }

    void OnSlamCompleted()
    {
        hasSlammed = true;
        activeTazos = activeTazos.Where(x=>x.gameObject.activeSelf).ToList();
    }
}
