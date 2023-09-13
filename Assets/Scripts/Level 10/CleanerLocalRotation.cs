using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerLocalRotation : MonoBehaviour
{
    [SerializeField] LimitedMove limitedMove;

    [SerializeField] float rotationSpeed = 45.0f; // Adjust the speed as needed.
    [SerializeField] float maxYRotation = 30.0f; // Maximum rotation angle in degrees.

    private float timeCounter = 0.0f;
    private void Update()
    {
        if(limitedMove.inputVector != Vector2.zero && limitedMove.canMove)
        {
            // Increment the time counter with Time.deltaTime.
            timeCounter += Time.deltaTime;

            // Calculate the rotation angle based on a sine wave.
            float rotationAngle = maxYRotation * Mathf.Sin(timeCounter * rotationSpeed);

            // Create a new rotation quaternion with the Y rotation angle.
            Quaternion newRotation = Quaternion.Euler(0, rotationAngle, 0);

            // Apply the new rotation to the object.
            transform.localRotation = newRotation;
        }      
    }
}
