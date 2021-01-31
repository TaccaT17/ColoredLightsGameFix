using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Interactable
{
    void Interactable.Interact(eInteractTypes type)
    {
        //animate chest


        //fade
        GameObject.FindObjectOfType<FadeObj>().GetComponent<FadeObj>().Fade();
    }
}
