using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairCleaner : MonoBehaviour
{
    [SerializeField] GameObject parentObject;
    //[SerializeField] GameObject childObject;


    private void OnCollisionEnter(Collision collision)
    {
        string objectName = collision.gameObject.name; // Get the name of the exited object
        Debug.Log("OnCollisionEnter");
        if (objectName.StartsWith("Hair"))
        {
            collision.gameObject.transform.SetParent(parentObject.transform);
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    string objectName = other.gameObject.name; // Get the name of the exited object
    //    Debug.Log("OnCollisionEnter");
    //    if (objectName.StartsWith("Hair"))
    //    {
    //        other.gameObject.transform.SetParent(parentObject.transform);
    //    }
    //}

}
