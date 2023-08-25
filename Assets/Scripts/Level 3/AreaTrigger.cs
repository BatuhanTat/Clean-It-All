using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] GameObject parentObject;

    private List<Rigidbody> sandBalls = new List<Rigidbody>();


    private bool hasCompleted = false;

    private void Start()
    {
        Rigidbody[] allRigidbodies = parentObject.GetComponentsInChildren<Rigidbody>();
        
        foreach(Rigidbody rb in allRigidbodies)
        {
            if(rb.gameObject.name.StartsWith("SandBall"))
            {
                sandBalls.Add(rb);
            }
        }
        Debug.Log("SandBall Count " + sandBalls.Count);
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Count " + sandBalls.Count);
        string objectName = other.gameObject.name; // Get the name of the exited object

        if (objectName.StartsWith("SandBall"))
        {
            if (sandBalls.Count <= 1)
            {
                if (!hasCompleted)
                {
                    GameManager.instance.CompleteLevel();
                    hasCompleted = true;
                }
            }
            else
            {
                // Remove the exitted object from the sandBalls list.
                sandBalls.Remove(other.GetComponent<Rigidbody>());
                //Debug.Log("Ball exited.");
            }
        }
        else if(objectName.StartsWith("Poop") && !hasCompleted)
        {
            Debug.Log("Restart Level");
            GameManager.instance.LoadLevel(SceneManager.GetActiveScene().name);
        }
    }
}
