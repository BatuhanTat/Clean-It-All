using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineAnimator : MonoBehaviour
{
    private float startWidth = 4.0f;
    private float endWidth = 8.0f;
    private float animationDuration = 1.0f; // Duration of the animation in seconds

    Outline outline;

    public bool doPlayAnimation = true;

    private void Start()
    {
        if (TryGetComponent(out outline))
        {
            StartCoroutine(AnimateOutlineWidth());
        }
        else
        {
            // If defaultTransform is null, throw an exception with an appropriate error message.
            throw new ArgumentNullException("outline", "The outline variable is null. Please assign a valid Outline script to outline.");
        }
    }

    private IEnumerator AnimateOutlineWidth()
    {
        while (doPlayAnimation) // Create an infinite loop
        {
            // Forward animation (4 to 8)
            float elapsedTime = 0.0f;
            float currentWidth = startWidth;

            while (elapsedTime < animationDuration)
            {
                currentWidth = Mathf.Lerp(startWidth, endWidth, elapsedTime / animationDuration);
                outline.OutlineWidth = currentWidth;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure that the final width is exactly the end width
            outline.OutlineWidth = endWidth;

            // Pause for a moment at the end width
            yield return new WaitForSeconds(0.4f);

            // Backward animation (8 to 4)
            elapsedTime = 0.0f;
            currentWidth = endWidth;

            while (elapsedTime < animationDuration)
            {
                currentWidth = Mathf.Lerp(endWidth, startWidth, elapsedTime / animationDuration);
                outline.OutlineWidth = currentWidth;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure that the final width is exactly the start width
            //outline.OutlineWidth = endWidth;

            // Pause for a moment at the start width
            yield return new WaitForSeconds(0.4f);
        }
    }
}

