using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtMover : MonoBehaviour
{
    public Transform initialLocation;
    public Transform targetLocation;
    public float moveSpeed = 5f;

    private bool movingToInitial = false;
    private bool movingToTarget = false;

    private void Update()
    {
        if (movingToInitial)
        {
            MoveToInitial();
            //Debug.Log("MoveToInitial");
        }
        else if (movingToTarget)
        {
            MoveToTarget();
            //Debug.Log("MoveToTarget");
        }
    }

    private void MoveToInitial()
    {
        Vector3 intermediateDirection = (initialLocation.position - transform.position).normalized;
        float intermediateDistance = Vector3.Distance(transform.position, initialLocation.position);

        if (intermediateDistance > 0.1f)
        {
            transform.position += intermediateDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            movingToInitial = false;
            movingToTarget = true;
        }
    }

    private void MoveToTarget()
    {
        Vector3 finalDirection = (targetLocation.position - transform.position).normalized;
        float finalDistance = Vector3.Distance(transform.position, targetLocation.position);

        if (finalDistance > 0.1f)
        {
            transform.position += finalDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Object has reached the target, you can handle this if needed
        }
    }

    public void StartMoving()
    {
        movingToInitial = true;
    }

}
