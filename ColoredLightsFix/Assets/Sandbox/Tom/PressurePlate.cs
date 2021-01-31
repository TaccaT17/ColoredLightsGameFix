using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Lighthouse lightHouseRef;
    
    bool isActivated;

    private void Start()
    {
        lightHouseRef.TurnOffLight();
    }

    public void ActivatePressurePlate()
    {
        lightHouseRef.TurnOnLight();
        isActivated = true;
    }

    public void DeactivatePressurePlate()
    {
        lightHouseRef.TurnOffLight();
        isActivated = false;
    }

}
