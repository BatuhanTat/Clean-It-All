using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JoystickMover : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float max_X;
    [SerializeField] float min_X;
    [Space]
    [SerializeField] float max_Z;
    [SerializeField] float min_Z;
    [Tooltip("Vertical means blue axis will point down, green axis will point right, red axis will point back.")]
    [SerializeField] bool isVertical = false;
    [SerializeField] bool isRotate = false;

    [SerializeField] float rotationSpeed = 5f;

    PlayerInput playerInput;
    Transform cubeTransform;
    //Vector2 inputVector;
    public Vector2 inputVector { get; private set; }

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

    //private void Update()
    //{
    //    Vector3 movementVector = Vector3.zero;
    //    // Calculate the incremental movement based on the input
    //    float movementX = inputVector.x * max_X * speed * Time.deltaTime;
    //    movementVector.x = movementX;
    //    float movement_YZ = inputVector.y * max_Y * speed * Time.deltaTime;
    //    if (vertical_Y)
    //    {
    //        movementVector.y = movement_YZ;
    //    }
    //    else
    //    {
    //        movementVector.z = movement_YZ;
    //    }

    //    // Calculate the new position
    //    Vector3 newPosition = rb.position + movementVector;

    //    // Clamp the current position to the range of -maxXPosition to maxXPosition
    //    newPosition.x = Mathf.Clamp(newPosition.x, -max_X, max_X);
    //    if (vertical_Y)
    //    {
    //        newPosition.y = Mathf.Clamp(newPosition.y, -max_Y, max_Y);
    //    }
    //    else
    //    {
    //        newPosition.z = Mathf.Clamp(newPosition.z, -max_Z, max_Z);
    //    }

    //    // Move the Rigidbody to the new position using MovePosition
    //    rb.MovePosition(newPosition);    
    //}

    private void FixedUpdate()
    {

        if (inputVector != Vector2.zero)
        {
            Vector3 movement = new Vector3(inputVector.x, 0, inputVector.y).normalized;
            Vector3 newPosition = rb.position + movement * speed * Time.deltaTime;

            // Apply boundaries to the new position
            newPosition.x = Mathf.Clamp(newPosition.x, min_X, max_X);
            newPosition.z = Mathf.Clamp(newPosition.z, min_Z, max_Z);

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);

            if (isVertical)
            {
                targetRotation = Quaternion.Euler(90f, 90f, 0f);
            }          

            if (isRotate)
            {
                Quaternion finalRotation  = Quaternion.LookRotation(movement) * targetRotation;
                rb.MoveRotation(finalRotation );

            }
            rb.MovePosition(newPosition);
        }
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
