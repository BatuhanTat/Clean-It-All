using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotClean : MonoBehaviour
{
    [SerializeField] GameObject parentObject;

    private List<BoxCollider> thrashList = new List<BoxCollider>();

    private void Start()
    {
        thrashList.AddRange(parentObject.GetComponentsInChildren<BoxCollider>());

        Debug.Log("Thrash Count " + thrashList.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        thrashList.Remove(other.GetComponent<BoxCollider>());
        Destroy(other.gameObject);
        if (thrashList.Count <= 0)
        {
            Debug.Log("Level completed.");
            GameManager.instance.IncrementLevelProgress();
        }
    }
}
