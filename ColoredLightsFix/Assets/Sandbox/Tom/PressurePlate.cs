using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Lighthouse lightHouseRef;

    public ParticleSystem smokeParticles;

    bool isActivated;

    private void Start()
    {
        lightHouseRef.TurnOffLight();

        smokeParticles = GetComponentInChildren<ParticleSystem>();
    }

    public void ActivatePressurePlate()
    {
        lightHouseRef.TurnOnLight();
        smokeParticles.Play();
        isActivated = true;
    }

    public void DeactivatePressurePlate()
    {
        lightHouseRef.TurnOffLight();
        smokeParticles.Stop();
        isActivated = false;
    }

}
