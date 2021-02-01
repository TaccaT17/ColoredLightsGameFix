using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    public FadeObj[] fadeGO;

    public GameObject[] turnOffGOs;

    public void TurnOffStartMenu()
    {
        //turn off text
        foreach (GameObject gO in turnOffGOs)
        {
            gO.SetActive(false);
        }

        foreach (FadeObj fade in fadeGO)
        {
            StartCoroutine(fade.FadeIn());
        }

        GameManager.S.LoadNextLevel();

    }
}
