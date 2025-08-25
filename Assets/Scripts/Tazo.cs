using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Tazo : MonoBehaviour
{
    [SerializeField] float scoreValue;
    List<IModifier> modifiers = new();


    Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        TazoTracker.OnTazosDoneMoving += (x)=> AdjustModifiers();
        modifiers = GetComponents<IModifier>().ToList();
    }

    public void Slam(Vector3 position, float power)
    {
        
        rb.AddForceAtPosition(Vector3.up * power * 300, position);

        Vector3 newForce = position;
        newForce.y = this.transform.position.y;
        newForce.z = Random.Range(newForce.z - 1, newForce.z + 1);
        newForce.x = Random.Range(newForce.x - 1, newForce.x + 1);
        newForce.Normalize();
        rb.AddForce((transform.position - newForce).normalized * 1200 * power);
    }

    public bool RigidbodySleeping()
    {
        return rb.IsSleeping();
    }

    public bool HasBeenFlipped()
    {
        bool hasBeenFlipped = false;

        Debug.Log($"{transform.up} {name} has angle of {Vector3.Angle(Vector3.up,transform.up)}");
        hasBeenFlipped = Vector3.Angle(Vector3.up, transform.up) > 90;
        return hasBeenFlipped;
    }

    public void AdjustModifiers()
    {
        foreach (IModifier modifier in modifiers)
        {
            modifier.TurnEnded();
        }
    }

    public float GetFinalScore()
    {
        float modifiedScore = scoreValue;
        foreach(IModifier modifier in modifiers)
        {
            modifiedScore = modifier.ModifyScoreValue(modifiedScore);
        }
        return modifiedScore;
    }
}


