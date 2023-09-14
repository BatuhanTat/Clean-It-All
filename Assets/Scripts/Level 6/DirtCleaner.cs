using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DirtCleaner : MonoBehaviour
{
    [SerializeField] GameObject dirtObjectHolder;
    [SerializeField] GameObject dirtParent;
    [Space(2)]
    [Tooltip("Is there only one object to be cleaned in the scene?")]
    [SerializeField] bool singleObject = true;
    [SerializeField] UnityEvent completeCleaning;

    private List<BoxCollider> dirtList = new List<BoxCollider>();


    public void UpdateDirtList(GameObject _dirtParent)
    {
        dirtList.AddRange(_dirtParent.GetComponentsInChildren<BoxCollider>());
        Debug.Log("Dirt count: " + dirtList.Count);
    }
    private void Start()
    {
       UpdateDirtList(dirtParent);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter");
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
            if (singleObject)
            {
                Debug.Log("Level completed.");
                GameManager.instance.CompleteLevel();
            }
            else
            {
                //Complete_ObjectCleaning();
                completeCleaning.Invoke();
                Debug.Log("Object Cleaned");
            }
        }
    }


}
