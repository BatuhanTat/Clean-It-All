using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PushObjectsAway : MonoBehaviour
{
    [SerializeField] float pushForce = 10f;
    [SerializeField] float rotationForce = 5f;

    //private Rigidbody[] surroundingRigidbodies;
    private List<Rigidbody> surroundingRigidbodies = new List<Rigidbody>();

    bool canApplyForce = false;

    private void Start()
    {
        //surroundingRigidbodies = GetComponentsInChildren<Rigidbody>();
        // Get all Rigidbody components in the children of the GameObject and add them to the list
        surroundingRigidbodies.AddRange(GetComponentsInChildren<Rigidbody>());
        Debug.Log(surroundingRigidbodies.Count);
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

    public void OnHold(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            canApplyForce = true;
        }
        if (context.canceled)
        {
            canApplyForce = false;
        }
        Debug.Log("Phases: " + context.phase);
    }

    public void RemoveFromSurroundingRb(Rigidbody rbToRemove)
    {
        // Remove the object from the list
        surroundingRigidbodies.Remove(rbToRemove);
        // Destroy the cube object when it exits the area and removed from the list.
        Destroy(rbToRemove.gameObject);
    }
}
