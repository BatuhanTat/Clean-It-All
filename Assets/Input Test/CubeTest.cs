using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CubeTest : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    PlayerInput playerInput;
    Transform cubeTransform;
    Vector2 inputVector;


    float maxXPosition = 2.5f;
    float maxYPosition = 2.5f;
    /*    float newPositionX = 0f;
       float newPositionY = 0f; */

    Vector3 targetPosition;
    Rigidbody rb;
    private void Start()
    {
        Application.targetFrameRate = 60;
        cubeTransform = GetComponent<Transform>();
        //playerInput = GetComponent<PlayerInput>();
        targetPosition = cubeTransform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Calculate the incremental movement based on the input
        float movementX = inputVector.x * maxXPosition * speed * Time.deltaTime;
        float movementY = inputVector.y * maxYPosition * speed * Time.deltaTime;

        /*  // Update the current position for both X and Y axes
         newPositionX += movementX;
         newPositionY += movementY;

         // Clamp the current position to the range of -maxXPosition to maxXPosition
         newPositionX = Mathf.Clamp(newPositionX, -maxXPosition, maxXPosition);
         newPositionY = Mathf.Clamp(newPositionY, -maxYPosition, maxYPosition);

         // Keep the z position unchanged
         float newPositionZ = cubeTransform.position.z; */

        // Calculate the new position
        Vector3 newPosition = rb.position + new Vector3(movementX, movementY, 0f);

        // Clamp the current position to the range of -maxXPosition to maxXPosition
        newPosition.x = Mathf.Clamp(newPosition.x, -maxXPosition, maxXPosition);
        newPosition.y = Mathf.Clamp(newPosition.y, -maxYPosition, maxYPosition);

        // Move the Rigidbody to the new position using MovePosition
        rb.MovePosition(newPosition);

        /*  // Set the new position directly using Translate
         cubeTransform.Translate(new Vector3(newPositionX - cubeTransform.position.x,
                                             newPositionY - cubeTransform.position.y, 0f)); */

    }

    public void Move(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        //Debug.Log("Move: " + inputVector);
        if (context.canceled)
        {
            //Debug.Log("Value: " + inputVector);
        }
    }

}
