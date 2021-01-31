using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{

    //Variable declaration
    public int[] coords;

    public void Init(int x, int z)
    {
        coords = new int[2] { x, z };
    }

}
