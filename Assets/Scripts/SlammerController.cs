using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlammerController : MonoBehaviour
{
    public static Action SlamCompleted;

    public int player { get; private set; } = -1;

    [SerializeField] ScoreController scoreController;
    [SerializeField] InputActionAsset InputActions;
    [SerializeField] PowerMeter powerMeter;
    [SerializeField] private float slammerMaxRadius;
    [SerializeField] private float slammerMinRadius;

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
        if(player == 1)
        {
            DoNPCTurn();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (player != 0) return;

        if (!isSlamming) return;

        if(!powerMeter.isCharging && LeftMouseButtonAction.WasPressedThisFrame())
        {
            power = 0;
            powerMeter.StartCharging();
            Ray ray = Camera.main.ScreenPointToRay(MousePositionAction.ReadValue<Vector2>());
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                slamPosition = hit.point;
            }
        }

        if(powerMeter.isCharging && LeftMouseButtonAction.WasReleasedThisFrame())
        {
            power = powerMeter.StopCharging();
            float difference = slammerMaxRadius - slammerMinRadius;
            float influence = difference * power;
            slamRadius = slammerMinRadius + influence;
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
        SlamCompleted();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(slamPosition, slamRadius);
    }
    
    void DoNPCTurn()
    {
        DOTween.Sequence()
            .AppendCallback(() =>
            {
                Debug.Log("Doing npc turn");
            })
            .AppendInterval(1f)
            .AppendCallback(() =>
            {
                ShootSlammer(slamRadius,Vector3.zero, 5);
            });
    }
}
