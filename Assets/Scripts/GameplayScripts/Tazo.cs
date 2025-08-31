using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public enum Series
{
    Halloween,
    Pets
}

[Flags]
public enum StatusEffects
{
    Hairy
}


public class Tazo : MonoBehaviour
{
    public Rigidbody rb;
    public Series Series;
    public string Owner;
    public string Description;
    public string Name;

    public float scoreValue;
    List<IModifier> modifiers = new();

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        modifiers = GetComponents<IModifier>().ToList();
    }

    public void Slam(Vector3 position, float power)
    {
        
        rb.AddForceAtPosition(Vector3.up * power * 300, position);

        Vector3 newForce = position;
        newForce.y = this.transform.position.y;
        newForce.z = UnityEngine.Random.Range(newForce.z - 1, newForce.z + 1);
        newForce.x = UnityEngine.Random.Range(newForce.x - 1, newForce.x + 1);
        newForce.Normalize();
        rb.AddForce((transform.position - newForce).normalized * 1200 * power);
    }

    public bool RigidbodySleeping()
    {
        return rb.IsSleeping();
    }

    public bool HasBeenFlipped()
    {
        if (!gameObject.activeSelf)
            return false;
        
        bool hasBeenFlipped = false;
        hasBeenFlipped = Vector3.Angle(Vector3.up, transform.up) > 90;
        return hasBeenFlipped;
    }

    public void ActivateModifier()
    {
        foreach (IModifier modifier in modifiers)
        {
            modifier.ActivateModifier();
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

    void FixedUpdate()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = 0;
        if (transform.position.y < 0)
        {
            transform.position = currentPosition;
        }
        if(currentPosition.magnitude > 5)
        {
            Vector3 newDirection = currentPosition;
            newDirection.z = UnityEngine.Random.Range(newDirection.z - 5, newDirection.z + 5);
            newDirection.x = UnityEngine.Random.Range(newDirection.x - 5, newDirection.x + 5);

            rb.linearVelocity = -newDirection.normalized * rb.linearVelocity.magnitude * .8f;
        }
    }

    public string GetDescription()
    {
        return Description;
    }
}


