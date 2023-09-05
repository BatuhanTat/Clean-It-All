using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDirtMaterial : MonoBehaviour
{
    [SerializeField] Material dirtySurfaceMaterial;
    [SerializeField] GameObject dirtGasEffect_holder;

    private List<ParticleSystem> particleList = new List<ParticleSystem>();

    private void Awake()
    {
        DisableDirtEffect(1.0f);
    }

    private void Start()
    {
        particleList.AddRange(dirtGasEffect_holder.GetComponentsInChildren<ParticleSystem>());

        Debug.Log("Particle  count: " + particleList.Count);
    }

    public void DisableDirtEffect(float value)
    {
        Color color = dirtySurfaceMaterial.color;
        color.a = value;
        dirtySurfaceMaterial.color = color;

        foreach (ParticleSystem particle in particleList)
        {
            particle.gameObject.SetActive(false);
        }
    }



  
}
