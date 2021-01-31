using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    public Canvas canvas;

    public void Start()
    {
        if(canvas == null)
        {
            canvas = GameObject.FindObjectOfType<Canvas>().GetComponent<Canvas>();
        }
    }

    public void TurnOffStartMenu()
    {
        canvas.enabled = false;
    }
}
