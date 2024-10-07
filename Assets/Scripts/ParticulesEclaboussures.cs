using System;
using UnityEngine;

public class ParticulesEclaboussures : MonoBehaviour
{
    public ParticleSystem systemeParticules; // Reference au component ParticleSystem du GameObject;
    public ParticleSystem.EmissionModule moduleEmission; // Reference au Module Emission du ParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        /* ================== ASSIGNATION AUX REFERENCES ================== */

        // ParticleSystem
        systemeParticules = GetComponent<ParticleSystem>();

        // EmissionModule
        moduleEmission = systemeParticules.emission;
    }
}
