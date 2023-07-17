using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] float transitionSpeed = 2f;
    [SerializeField] float positionThreshold = 0.1f;
    [SerializeField] float rotationThreshold = 1.0f;
    [SerializeField] Transform defaultTransform;


    private Transform targetTransform;

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;

    private void Start()
    {
        desiredPosition = defaultTransform.position;
        desiredRotation = defaultTransform.rotation;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (targetTransform != null && !GameManager.instance.canClean)
        {
            // Calculate the distance between current position and desired position
            float positionDistance = Vector3.Distance(transform.position, desiredPosition);

            // Calculate the angle difference between current rotation and desired rotation
            float rotationAngleDifference = Quaternion.Angle(transform.rotation, desiredRotation);

            // Check if the distance and angle difference are below the threshold
            if (positionDistance > positionThreshold || rotationAngleDifference > rotationThreshold)
            {
                // Smoothly transition to the desired position and rotation
                transform.position = Vector3.Lerp(transform.position, desiredPosition, transitionSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, transitionSpeed * Time.deltaTime);
            }
            else
            {
                GameManager.instance.canClean = true;
            }
        }
    }

    public void OnSelectCamera(InputValue value)
    {
        Ray ray = Camera.main.ScreenPointToRay(value.Get<Vector2>());
        RaycastHit hit;
        Debug.Log("Camera hitledim gumandan");
        if (Physics.Raycast(ray, out hit))
        {
            // Assign the clicked object as the target object
            targetTransform = hit.transform;
        }
    }
}