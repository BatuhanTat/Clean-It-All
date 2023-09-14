using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetMover : MonoBehaviour
{
    [SerializeField] float transitionSpeed = 2f;
    [SerializeField] float positionThreshold = 0.1f;
    [SerializeField] float rotationThreshold = 1.0f;
    [SerializeField] Transform defaultTransform;
    public bool doMove = true;

    private Transform targetTransform;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Vector3 initialPosition;
    private Quaternion initialRotation;


    private void Start()
    {
        if (defaultTransform != null)
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
            targetPosition = defaultTransform.position;
            targetRotation = defaultTransform.rotation;
        }
        else
        {
            // If defaultTransform is null, throw an exception with an appropriate error message.
            throw new ArgumentNullException("defaultTransform", "The defaultTransform is null. Please assign a valid Transform to defaultTransform.");
        }
    }

    private void Update()
    {
        if (doMove)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        // Calculate the distance between current position and desired position
        float positionDistance = Vector3.Distance(transform.position, targetPosition);

        // Calculate the angle difference between current rotation and desired rotation
        float rotationAngleDifference = Quaternion.Angle(transform.rotation, targetRotation);

        // Check if the distance and angle difference are below the threshold
        if (positionDistance > positionThreshold || rotationAngleDifference > rotationThreshold)
        {
            // Smoothly transition to the desired position and rotation
            transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, transitionSpeed * Time.deltaTime);
        }
        else
        {
            doMove = false;
        }
    }

    public void BacktoInitialPosition(float waitSeconds)
    {
        StartCoroutine(DelayedReset(waitSeconds));
    }

    private IEnumerator DelayedReset(float waitSeconds)
    {
        Debug.Log("Back to InitialPosition - Delayed");
        yield return new WaitForSeconds(waitSeconds);

        // Reset the target position and rotation to the initial values
        targetPosition = initialPosition;
        targetRotation = initialRotation;
        doMove = true;
    }

    /*   public void OnSelectCamera(InputValue value)
      {
          Ray ray = Camera.main.ScreenPointToRay(value.Get<Vector2>());
          RaycastHit hit;
          Debug.Log("Camera hitledim gumandan");
          if (Physics.Raycast(ray, out hit))
          {
              // Assign the clicked object as the target object
              targetTransform = hit.transform;
          }
      } */
}