using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtCleaner : MonoBehaviour
{
    [SerializeField] GameObject dirtObjectHolder;
    [SerializeField] GameObject dirtParent;

    private List<BoxCollider> dirtList = new List<BoxCollider>();

    private void Start()
    {
        dirtList.AddRange(dirtParent.GetComponentsInChildren<BoxCollider>());

        Debug.Log("Dirt count: " + dirtList.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        //string objectName = other.gameObject.name; // Get the name of the exited object
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("Dirt"))
        {
            other.gameObject.transform.SetParent(dirtObjectHolder.transform);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            RemoveFromList(other.gameObject);
        }
    }

    private void RemoveFromList(GameObject dirtObject)
    {
        dirtList.Remove(dirtObject.GetComponent<BoxCollider>());
        if (dirtList.Count <= 0)
        {
            Debug.Log("Level completed.");
            //GameManager.instance.CompleteLevel();
        }
    }
}
