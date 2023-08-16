using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalRotation : MonoBehaviour
{
    [SerializeField] JoystickMover joystickMover;
    [SerializeField] float rotationSpeed = 5.0f;

    private void FixedUpdate()
    {
        // Get the inputVector from the joystickMover
        Vector2 inputVector = joystickMover.inputVector;

        if (inputVector != Vector2.zero)
        {
            // Calculate the rotation amount based on inputVector
            float rotationAmount = inputVector.magnitude * rotationSpeed * 100 * Time.fixedDeltaTime;

            // Apply the rotation around the y-axis in local space
            transform.Rotate(Vector3.up, rotationAmount, Space.Self);
        }
    }

}
