using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePropeller : MonoBehaviour
{
    [SerializeField] float rotationForce = 5.0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Rotate()
    {
        // Apply rotation force
        rb.AddTorque(transform.up * rotationForce, ForceMode.Force);   
    }
}
