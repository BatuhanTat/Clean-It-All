using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tidy : MonoBehaviour
{
    [SerializeField] GameObject tidyObjectHolder;
    [SerializeField] DisableDirtMaterial disableDirtMaterial;

    private List<BoxCollider> tidyObjectList = new List<BoxCollider>();


    TargetMover targetMover;
    Outline outline;
    OutlineAnimator outlineAnimator;

    private void Start()
    {
        tidyObjectList.AddRange(tidyObjectHolder.GetComponentsInChildren<BoxCollider>());

        Debug.Log("Tidy object count: " + tidyObjectList.Count);
    }

    public void TidyObject(InputAction.CallbackContext context)
    {
        // Perform a raycast from the mouse position into the scene
        Ray ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
        RaycastHit hit;

        // Check if the raycast hits an object
        if (Physics.Raycast(ray, out hit))
        {
            // Get the selected object
            GameObject selectedObject = hit.transform.gameObject;

            // Log the name of the selected object to the console
            Debug.Log("Selected Object: " + selectedObject.name);

            // Optionally, you can do further actions with the selected object
            // For example, you can change its color or perform other interactions.

            if (selectedObject.TryGetComponent(out targetMover) && 
                selectedObject.TryGetComponent(out outline) &&
                selectedObject.TryGetComponent(out outlineAnimator))
            {
                targetMover.doMove = targetMover.doMove ? true : true;
                outlineAnimator.doPlayAnimation = false;
                //outline.OutlineWidth = 0.0f;
                outline.enabled = false;           
                RemoveFromList(selectedObject);
            }
        }
    }


    private void RemoveFromList(GameObject tidyobject)
    {
        tidyObjectList.Remove(tidyobject.GetComponent<BoxCollider>());
        if (tidyObjectList.Count <= 0)
        {
            Debug.Log("Level completed.");
            if(disableDirtMaterial != null)
            {
                disableDirtMaterial.DisableDirtEffect(0.0f);
            }
            GameManager.instance.CompleteLevel();
        }
    }
}
