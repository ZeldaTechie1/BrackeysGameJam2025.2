using UnityEngine;
using System.Collections.Generic;

public class Tazo : MonoBehaviour
{
    [SerializeField] float scoreValue;
    List<Modifier> modifiers;


    Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(transform.up);
    }

    public void Slam(Vector3 position, float power)
    {
        rb.AddForceAtPosition(Vector3.down * power * 5000, position);
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

    public float GetFinalScore()
    {
        float modifiedScore = scoreValue;
        foreach(Modifier modifier in modifiers)
        {
            modifiedScore = modifier.ModifyScoreValue(modifiedScore);
        }
        return modifiedScore;
    }
}


