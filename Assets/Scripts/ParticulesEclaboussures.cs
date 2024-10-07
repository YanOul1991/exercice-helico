using System;
using UnityEngine;

public class ParticulesEclaboussures : MonoBehaviour
{
    public Rigidbody helico;
    ParticleSystem systemeParticules; // Reference au component ParticleSystem du GameObject;
    ParticleSystem.EmissionModule moduleEmission; // Reference au Module Emission du ParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        /* ================== ASSIGNATION AUX REFERENCES ================== */

        // ParticleSystem
        systemeParticules = GetComponent<ParticleSystem>();

        // EmissionModule
        moduleEmission = systemeParticules.emission;
    }

    void Update()
    {
        moduleEmission.rateOverTime = MathF.Abs(helico.velocity.x) * 100;
    }
}
