using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairCleaner : MonoBehaviour
{
    [SerializeField] GameObject hairObjectHolder;
    [SerializeField] GameObject hairParent;


    private List<BoxCollider> hairList = new List<BoxCollider>();


    private void Start()
    {
        hairList.AddRange(hairParent.GetComponentsInChildren<BoxCollider>());

        Debug.Log("Hair count: " + hairList.Count);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    string objectName = collision.gameObject.name; // Get the name of the exited object
    //    Debug.Log("OnCollisionEnter");
    //    if (objectName.StartsWith("Hair"))
    //    {
    //        collision.gameObject.transform.SetParent(hairObjectHolder.transform);
    //        collision.gameObject.GetComponent<BoxCollider>().enabled = false;
    //        RemoveFromList(collision.gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        string objectName = other.gameObject.name; // Get the name of the exited object
        Debug.Log("OnCollisionEnter");
        if (objectName.StartsWith("Hair"))
        {
            other.gameObject.transform.SetParent(hairObjectHolder.transform);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            RemoveFromList(other.gameObject);
        }
    }

    private void RemoveFromList(GameObject hairObject)
    {
        hairList.Remove(hairObject.GetComponent<BoxCollider>());
        if (hairList.Count <= 0)
        {
            Debug.Log("Level completed.");
            GameManager.instance.IncrementLevelProgress();
        }
    }
}
