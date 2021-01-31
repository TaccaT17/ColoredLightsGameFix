using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Lighthouse lightHouseRef;
    
    bool isActivated;

    public void ActivatePressurePlate()
    {
        lightHouseRef.TurnOnLight();
        isActivated = true;
    }

    public void DeavtivatePressurePlate()
    {
        lightHouseRef.TurnOffLight();
        isActivated = false;
    }

}
