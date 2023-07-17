using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PushObjectsAway : MonoBehaviour
{
    [SerializeField] float pushForce = 10f;
    [SerializeField] float rotationForce = 5f;

    private Rigidbody[] surroundingRigidbodies;
    bool canApplyForce = false;

    private void Start()
    {
        surroundingRigidbodies = GetComponentsInChildren<Rigidbody>();
        Debug.Log(surroundingRigidbodies.Length);
    }

    private void FixedUpdate()
    {
        if (canApplyForce)
        {
            ApplyForce();
            Debug.Log("Update deyiz");
        }
    }

    private void ApplyForce()
    {
        // Apply forces to push away the surrounding objects
        foreach (Rigidbody rb in surroundingRigidbodies)
        {
            Vector3 centerToSurrounding = rb.transform.position - transform.position;
            rb.AddForce(centerToSurrounding.normalized * pushForce, ForceMode.Force);

            // Calculate rotation direction based on object's position relative to the center object
            Vector3 rotationDirection = Quaternion.Euler(0f, 90f, 0f) * centerToSurrounding.normalized;

            // Apply torque force to rotate the object
            rb.AddTorque(rotationDirection * rotationForce, ForceMode.Force);
        }
    }

    private void OnPress(InputValue value)
    {
        canApplyForce = value.isPressed;
    }
}
