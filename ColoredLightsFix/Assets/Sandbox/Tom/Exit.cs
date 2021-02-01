using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour , Interactable
{
    Animator anim;

    void Interactable.Interact(eInteractTypes type)
    {
        //animate chest
        anim = GetComponent<Animator>();
        anim.SetTrigger("OpenChest");

        //fade
        GameObject.FindObjectOfType<FadeObj>().GetComponent<FadeObj>().Fade();
    }
}
