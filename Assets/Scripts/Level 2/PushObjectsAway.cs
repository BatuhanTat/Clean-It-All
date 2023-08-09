using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PushObjectsAway : MonoBehaviour
{
    [SerializeField] float pushForce = 10f;
    [SerializeField] RotatePropeller propeller; 

    //private Rigidbody[] surroundingRigidbodies;
    private List<Rigidbody> surroundingRigidbodies = new List<Rigidbody>();

    bool canApplyForce = false;
    int totalObject_Count;
    int pushedObject_Count = 0;

    private void Start()
    {
        //surroundingRigidbodies = GetComponentsInChildren<Rigidbody>();
        // Get all Rigidbody components in the children of the GameObject and add them to the list
        surroundingRigidbodies.AddRange(GetComponentsInChildren<Rigidbody>());
        totalObject_Count = surroundingRigidbodies.Count;
        Debug.Log(surroundingRigidbodies.Count + " Totel object count " + totalObject_Count);
    }

    private void FixedUpdate()
    {
        if (canApplyForce)
        {
            ApplyForce();
            //Rotate Propeller
            propeller.Rotate();
            Debug.Log("Update deyiz");
        }
    }

    private void ApplyForce()
    {
        // Apply forces to push away the surrounding objects
        foreach (Rigidbody rb in surroundingRigidbodies)
        {
            Vector3 centerToSurrounding = rb.transform.position - transform.position;
            centerToSurrounding.y = 0f; // Ignore Y component
            rb.AddForce(centerToSurrounding.normalized * pushForce, ForceMode.Force);

            // Rotation
            /*  // Calculate rotation direction based on object's position relative to the center object
                Vector3 rotationDirection = Quaternion.Euler(0f, 90f, 0f) * centerToSurrounding.normalized;

                // Apply torque force to rotate the object
                rb.AddTorque(rotationDirection * rotationForce, ForceMode.Force); */
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
        //Debug.Log("Phases: " + context.phase);
    }

    public void RemoveFromSurroundingRb(Rigidbody rbToRemove)
    {
        // Remove the object from the list
        surroundingRigidbodies.Remove(rbToRemove);
        // Destroy the cube object when it exits the area and removed from the list.
        Destroy(rbToRemove.gameObject);
        pushedObject_Count++;
        // When "pushedObject_Count >= totalObject_Count" win condition.
        if (pushedObject_Count >= totalObject_Count)
        {
            Debug.Log("Level Completed");
        }
    }
}
