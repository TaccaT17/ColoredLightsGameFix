using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    public enum Direction
    {
        North,
        South,
        East,
        West
    }

    public enum ColorOfLight
    {
        red,
        yellow,
        blue
    }

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

}
