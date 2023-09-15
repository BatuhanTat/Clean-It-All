using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LimitedMove : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;  // Adjust the speed as needed.
    [SerializeField] Vector3 defaultPosition;

    [Space]
    [Header("Target, dirty object to be cleaned")]
    [SerializeField] Transform startPosition_Target;
    [SerializeField] Transform endPosition_Target;
    [SerializeField] TargetMover targetMover;

    public Vector2 inputVector { get; private set; }
    public bool canMove { get; private set; } = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!targetMover.doMove && !canMove)
        {
            startPosition = startPosition_Target.position;
            endPosition = endPosition_Target.position;
            canMove = true;
        }

        if (inputVector != Vector2.zero && canMove)
        {
            Vector3 movement = new Vector3(inputVector.x, 0, inputVector.y).normalized;

            // Scale the movement vector based on the input vector's magnitude
            movement *= inputVector.magnitude;

            Vector3 newPosition;

            if (inputVector.x > 0)
            {
                newPosition = endPosition;
            }
            else
            {
                newPosition = startPosition;
            }
            transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * speed * movement.magnitude);
        }
        else
        {
            // Move to the default position.
            transform.position = Vector3.MoveTowards(transform.position, defaultPosition, Time.deltaTime * speed);
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

    public void UpdateTargetObject(GameObject targetObject)
    {
        startPosition_Target = targetObject.transform.Find("Start Point");
        endPosition_Target = targetObject.transform.Find("End Point");
        canMove = false;
        targetMover = targetObject.GetComponent<TargetMover>();
    }

    //private void PrintStuff()
    //{
    //    Debug.Log("Start position      : " + startPosition);
    //    Debug.Log("Start position phone: " + startPosition_Phone.position);
    //    Debug.Log("Start position phone: " + startPosition_Phone.localPosition);
    //    Debug.Log("");
    //    Debug.Log("End position        : " + endPosition);
    //    Debug.Log("End position phone  : " + endPosition_Phone.position);
    //    Debug.Log("End position phone  : " + endPosition_Phone.localPosition);     
    //}
}
