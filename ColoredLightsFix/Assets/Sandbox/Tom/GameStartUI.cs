using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    public FadeObj[] fadeGO;

    public GameObject text;

    public void TurnOffStartMenu()
    {
        //turn off text
        text.SetActive(false);

        foreach (FadeObj fade in fadeGO)
        {
            StartCoroutine(fade.FadeIn());
        }
    }
}
