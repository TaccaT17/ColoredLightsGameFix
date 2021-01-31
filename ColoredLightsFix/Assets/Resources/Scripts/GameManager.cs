using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    public GameObject endScreen;

    public enum Direction
    {
        North,
        South,
        East,
        West
    }

    public enum ColorOfLight
    {
        none,
        red,
        yellow,
        blue
    }

    public string[] levels;
    private int leveltrack;

    void Awake()
    {
        if(S == null)
        {
            S = this;
        }
        else
        {
            Destroy(this);
        }
        
        leveltrack = -1;

        GridHandler.Init();

    }

    public Component GetOrCreateComponent<T>(out T component, GameObject gO) where T : Component
    {

        if (gO.GetComponent<T>() == null)
        {
            component = gO.gameObject.AddComponent<T>();
        }
        else
        {
            component = gO.GetComponent<T>();
        }

        return component;
    }

    public void LoadNextLevel()
    {
        leveltrack += 1;
        if(leveltrack > levels.Length - 1)
        {
            //endgame
            GridHandler.UnloadLevel();
            endScreen.SetActive(true);
        }
        else
        {
            GridHandler.LoadLevel(levels[leveltrack]);
        }
    }
}
