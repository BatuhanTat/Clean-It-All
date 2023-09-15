using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Handler : MonoBehaviour
{
    private ParticleSystem particleSys;

    // Start is called before the first frame update
    void Start()
    {
        particleSys = GetComponent<ParticleSystem>();
    }
    public void PlayEffect()
    {
        particleSys.Play();  
    }
}
