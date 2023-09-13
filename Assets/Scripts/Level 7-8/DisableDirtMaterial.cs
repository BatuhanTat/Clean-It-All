using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDirtMaterial : MonoBehaviour
{
    [SerializeField] Material dirtySurfaceMaterial;
    [SerializeField] GameObject dirtGasEffect_holder;
    [SerializeField] bool instantFade = true;

    [Header("Gradual Fading")]
    [SerializeField] float targetValue = 0f;
    [SerializeField] float duration = 2f;
    private float startValue = 1.0f;
    private bool isLerping = false;
    private bool hasLerped = false;

    private List<ParticleSystem> particleList = new List<ParticleSystem>();



    private void Awake()
    {
        //DisableDirtEffect(1.0f);
        ResetMaterialDirt();
    }

    private void Start()
    {
        if (dirtGasEffect_holder != null)
        {
            particleList.AddRange(dirtGasEffect_holder.GetComponentsInChildren<ParticleSystem>());
        }

        Debug.Log("Particle  count: " + particleList.Count);
    }

    public void DisableDirtEffect(float value)
    {
        Debug.Log("Disable Dirt Effect Called");
        if (instantFade)
        {
            InstantFade(value);
        }
        else
        {
            if (!isLerping && !hasLerped)
            {
                StartCoroutine(GradualFade());
            }
        }

        foreach (ParticleSystem particle in particleList)
        {
            particle.gameObject.SetActive(false);
        }
    }

    private void InstantFade(float value)
    {
        Color color = dirtySurfaceMaterial.color;
        color.a = value;
        dirtySurfaceMaterial.color = color;
    }

    private IEnumerator GradualFade()
    {
        isLerping = true;
        float elapsedTime = 0f;

        Color color = dirtySurfaceMaterial.color;
        Debug.Log("Gradual Fade");
        while (elapsedTime < duration)
        {
            Debug.Log("Fading");
            // Calculate the current value of the float property using the lerp function
            float currentValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);

            // Update the material's float property with the new value
            color.a = currentValue;
            dirtySurfaceMaterial.color = color;

            // Increase the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure that the final value is set precisely
        color.a = targetValue;
        dirtySurfaceMaterial.color = color;
        isLerping = false;
        hasLerped = true;
    }

    private void ResetMaterialDirt()
    {
        Color color = dirtySurfaceMaterial.color;
        color.a = 1.0f;
        dirtySurfaceMaterial.color = color;
    }
}
