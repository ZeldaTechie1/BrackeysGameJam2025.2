using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlammerController : MonoBehaviour
{
    public static Action OnSlamComplete;

    [SerializeField] ScoreController scoreController;
    [SerializeField] InputActionAsset InputActions;
    [SerializeField] PowerMeter powerMeter;
    [SerializeField] private float slammerMaxRadius;
    [SerializeField] private float slammerMinRadius;

    private InputAction LeftMouseButtonAction;
    private InputAction MousePositionAction;

    private Vector3 slamPosition;
    private float slamRadius;
    private bool isCharging = false;
    private float power = 0;
    


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

    private void Awake()
    {
        LeftMouseButtonAction = InputSystem.actions.FindAction("Attack");
        MousePositionAction = InputSystem.actions.FindAction("MousePosition");
    }

    // Update is called once per frame
    private void Update()
    {
        if(!powerMeter.isCharging && LeftMouseButtonAction.WasPressedThisFrame())
        {
            power = 0;
            powerMeter.StartCharging();
            isCharging = true;
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
            ShootSlammer(slamRadius,slamPosition);
        }
    }

    private void ShootSlammer(float radius, Vector3 location)
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
        OnSlamComplete();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(slamPosition, slamRadius);
    }
}
