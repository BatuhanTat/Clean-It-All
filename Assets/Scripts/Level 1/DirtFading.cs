using System.Collections;
using UnityEngine;

public class DirtFading : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] string floatPropertyName = "_DirtMask_Multiplier";
    [SerializeField] float default_DirtMask_Multiplier = 1.05f;
    [SerializeField] float targetValue = 0f;
    [SerializeField] float duration = 2f;

    private float startValue;
    private bool isLerping = false;
    private bool hasLerped = false;

    private void Start()
    {    
        // Set the initial value to the default value when the scene is loaded
        material.SetFloat(floatPropertyName, default_DirtMask_Multiplier);
    }

    public void StartLerping()
    {
        
        // Cache the starting value of the float property
        startValue = material.GetFloat(floatPropertyName);

        if (!isLerping && !hasLerped)
        {
            Debug.Log("Start Lerping");
            // Start the coroutine to gradually decrease the float value
            StartCoroutine(LerpFloat());
        }
    }

    private IEnumerator LerpFloat()
    {
        isLerping = true;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the current value of the float property using the lerp function
            float currentValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);

            // Update the material's float property with the new value
            material.SetFloat(floatPropertyName, currentValue);

            // Increase the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure that the final value is set precisely
        material.SetFloat(floatPropertyName, targetValue);

        isLerping = false;
        hasLerped = true;
    }
}