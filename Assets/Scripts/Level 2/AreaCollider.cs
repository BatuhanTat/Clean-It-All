using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollider : MonoBehaviour
{
    [SerializeField] private PushObjectsAway pushObjectsAway;

    private void OnTriggerExit(Collider other)
    {
        // Code to execute when a cube object exits the area
        CubeExitedArea();
        // Call the method in PushObjectsAway to remove the object from the array
        pushObjectsAway.RemoveFromSurroundingRb(other.GetComponent<Rigidbody>());

    }

    private void CubeExitedArea()
    {
        Debug.Log("A cube has exited the area.");
        // Add any additional code you want to execute when a cube exits the area.
    }
}
