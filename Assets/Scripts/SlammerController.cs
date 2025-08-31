using DG.Tweening;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class SlammerController : MonoBehaviour
{
    public static Action SlamCompleted;

    public int player { get; private set; } = -1;

    [SerializeField] ScoreController scoreController;
    [SerializeField] InputActionAsset InputActions;
    [SerializeField] PowerMeter powerMeter;
    [SerializeField] private float slammerMaxRadius;
    [SerializeField] private float slammerMinRadius;
    [SerializeField] AimIndicator aimIndicator;
    [SerializeField] TazoTracker tazoTracker;
    [SerializeField] Camera aimCamera;

    private InputAction LeftMouseButtonAction;
    private InputAction MousePositionAction;

    private Vector3 slamPosition;
    private float slamRadius;
    private float power = 0;
    private bool isSlamming = false;

    public void Setup()
    {
        LeftMouseButtonAction = InputSystem.actions.FindAction("Attack");
        MousePositionAction = InputSystem.actions.FindAction("MousePosition");
        TurnHandler.PlayerStartSlam += OnPlayerStartSlam;
        VampiricDrain.GetCurrentPlayer += GetCurrentPlayer;
    }

    private int GetCurrentPlayer()
    {
        return player;
    }

    private void OnEnable()
    {
        if(!InputActions.enabled)
        {
            InputActions.FindActionMap("Player").Enable();
        }
    }
    private void OnDisable()
    {
        if (InputActions.enabled)
        {
            InputActions.FindActionMap("Player").Disable();
        }
    }

    void OnPlayerStartSlam()
    {
        isSlamming = true;
        player++;
        player = (int)Mathf.Repeat(player, 2);
        Debug.Log($"Player is {player}");
        aimIndicator.gameObject.SetActive(true);
        if (player == 1)
        {
            DoNPCTurn();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isSlamming) return;

        if(powerMeter.isCharging)
        {
            slamRadius = CalculateSlamRadius(powerMeter.currentPower);
            Debug.Log(slamRadius);
            aimIndicator.SetIndicatorSize(slamRadius);
        }
        else
        {
            if (player == 0)
            {
                Vector3 modifiedScreenSpace = Mouse.current.position.ReadValue();
                modifiedScreenSpace.x /= 2.25f;
                modifiedScreenSpace.x -= 106.666f;
                modifiedScreenSpace.y /= 2.25f;
                Ray ray = aimCamera.ScreenPointToRay(modifiedScreenSpace);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    slamPosition = hit.point;
                    aimIndicator.SetIndicatorPosition(slamPosition);
                }
            }
        }

        if (!powerMeter.isCharging && LeftMouseButtonAction.WasPressedThisFrame())
        {
            power = 0;
            powerMeter.StartCharging();
        }

        if(powerMeter.isCharging && LeftMouseButtonAction.WasReleasedThisFrame())
        {
            power = powerMeter.StopCharging();
            ShootSlammer(slamRadius,slamPosition,power);
        }
    }

    private void ShootSlammer(float radius, Vector3 location, float power)
    {
        Vector3 slamSpherecastPosition = location;
        slamSpherecastPosition.y = 100000;

        RaycastHit[] raycastHit;
        raycastHit = Physics.SphereCastAll(slamSpherecastPosition, radius, Vector3.down);
        foreach(RaycastHit hit in raycastHit)
        {
            Tazo currentTazo = null;
            hit.transform.TryGetComponent<Tazo>(out currentTazo);
            if (currentTazo != null)
            {
                currentTazo.Slam(slamSpherecastPosition, power);
            }
        }
        isSlamming = false;
        aimIndicator.gameObject.SetActive(false);
        aimIndicator.SetIndicatorSize(1);
        SlamCompleted();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(slamPosition, slamRadius);
    }
    
    void DoNPCTurn()
    {
        Vector3 calculatedSlamPosition = Vector3.zero;
        foreach (Tazo t in tazoTracker.activeTazos)
        {
            calculatedSlamPosition += t.transform.position;
        }
        calculatedSlamPosition /= tazoTracker.activeTazos.Count;
        calculatedSlamPosition.x = UnityEngine.Random.Range(calculatedSlamPosition.x - 2, calculatedSlamPosition.x + 2);
        calculatedSlamPosition.y = UnityEngine.Random.Range(calculatedSlamPosition.y - 2, calculatedSlamPosition.y + 2);

        Vector3 fakeAimingPosition = Vector3.zero;

        Sequence npcSequence = DOTween.Sequence()
            .AppendCallback(() =>
             {
                 Debug.Log("Doing npc turn");
             });

        //aiming section
        List<Vector3> aimPoints = new();
        int randAimPoints = UnityEngine.Random.Range(1,3);
        for (int i = 0; i < randAimPoints; i++)
        {
            aimPoints.Add(new Vector3(UnityEngine.Random.Range(calculatedSlamPosition.x - 2, calculatedSlamPosition.x + 2), calculatedSlamPosition.y, UnityEngine.Random.Range(calculatedSlamPosition.z - 2, calculatedSlamPosition.z + 2)));
        }
        aimPoints.Add(calculatedSlamPosition);

        foreach (Vector3 point in aimPoints)
        {
            npcSequence.Append(DOTween.To(() => fakeAimingPosition, x => fakeAimingPosition = x, point,1f).SetEase(Ease.Linear).OnUpdate(()=>aimIndicator.SetIndicatorPosition(fakeAimingPosition)));
        }

        //charging section
        float chargeTime = UnityEngine.Random.Range(1f,3f);
        npcSequence
            .AppendInterval(1f)
            .AppendCallback(() => 
            {
                powerMeter.StartCharging();
            })
            .AppendInterval(chargeTime)
            .AppendCallback(() =>
            {
                power = powerMeter.StopCharging();
            })
            .AppendInterval(0)//this waits exactly a frame
            .AppendCallback(() =>
            {
                Debug.Log($"Power {power}");
                ShootSlammer(slamRadius, calculatedSlamPosition, power);
            });
    }

    float CalculateSlamRadius(float power)
    {
        float radius;
        float difference = slammerMaxRadius - slammerMinRadius;
        float influence = difference * power;
        radius = slammerMinRadius + influence;
        return radius;

    }
}

