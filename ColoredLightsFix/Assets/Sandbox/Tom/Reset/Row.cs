using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{

    public GridUnit[] gridUnit;

    void Start()
    {
        gridUnit = this.gameObject.GetComponentsInChildren<GridUnit>();
    }

}
