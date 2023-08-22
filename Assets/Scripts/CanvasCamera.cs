using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCamera : MonoBehaviour
{
    GameObject canvas;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        Debug.Log("canvas :" + canvas);
        if (canvas != null)
        {
            canvas.GetComponent<Canvas>().worldCamera = gameObject.GetComponent<Camera>();
        }
    }
}
