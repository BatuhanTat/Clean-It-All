using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [SerializeField] GameObject parentObject;

    private List<BoxCollider> dirtList = new List<BoxCollider>();

    private void Start()
    {
        dirtList.AddRange(parentObject.GetComponentsInChildren<BoxCollider>());

        Debug.Log("Thrash Count " + dirtList.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        string objectName = other.gameObject.name; // Get the name of the exited object

        if (objectName.StartsWith("Candy"))
        {
            DirtMover dirt = other.gameObject.GetComponent<DirtMover>();
            if (dirt != null)
            {
                dirt.StartMoving();
                Debug.Log("Start moving");
            }
        }     
    }

    private void OnTriggerExit(Collider other)
    {
        dirtList.Remove(other.GetComponent<BoxCollider>());
        Destroy(other.gameObject);
        if (dirtList.Count <= 0)
        {
            Debug.Log("Level completed.");
            GameManager.instance.CompleteLevel();
        }
    }

}
