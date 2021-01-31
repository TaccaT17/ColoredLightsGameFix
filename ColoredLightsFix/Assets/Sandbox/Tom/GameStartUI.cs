using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    public FadeObj[] fadeGO;

    public GameObject button;

    public void TurnOffStartMenu()
    {
        //turn off text
        button.SetActive(false);

        foreach (FadeObj fade in fadeGO)
        {
            StartCoroutine(fade.FadeIn());
        }

        GameManager.S.LoadNextLevel();

    }
}
