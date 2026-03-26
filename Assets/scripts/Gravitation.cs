using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class Gravitation : MonoBehaviour
{
    public static List<Gravitation> otherObj;
    private Rigidbody rb;

    const float G = 6.67f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObj == null)
        {
            otherObj = new List<Gravitation>();
        }
        otherObj.Add(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Gravitation other in otherObj)
        {
            if (other != this)
            {
                Attract(other);
            }
        }
    }

    void Attract(Gravitation other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;

        float distance = direction.magnitude;
        if (distance == 0f)
        {
            return; // Avoid division by zero
        }

        float forceMagnitude = G * (rb.mass * otherRb.mass) / (distance * distance);
        Vector3 gravitationForce = forceMagnitude * direction.normalized;
        otherRb.AddForce(gravitationForce);
    }

}
