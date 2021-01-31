using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGrid : MonoBehaviour
{
    //take placed objects

    //Have array of a bunch of squares

    //on start those squares will generate raycast up and grab thing that is in their location making new array of gO

    public GameObject gridGO;

    Row[] rows;

    void Start()
    {
        //get gridGO
        if (gridGO == null)
        {
            print("ERROR: no gridGO");
        }

        rows = gridGO.GetComponentsInChildren<Row>();
    }

    /// <summary>
    /// Rows and columns numbered starting with 0. Returns null if going out of bounds of grid or if no tile there.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    public GameObject GetGridUnitGO(int row, int column)
    {
        if (row < 0 || row > rows.Length)
        {
            return null;
        }
        
        Row tempRow = rows[row];

        if (column < 0 || column > tempRow.gridUnit.Length)
        {
            return null;
        }

        return tempRow.gridUnit[column].gOGrabbed;
    }

    public void CheckMove(int row, int column, GameManager.Direction direction)
    {
        //Get tile trying to move to and return gameobject that's there
    }

    public void Moved(int row, int column, GameManager.Direction direction)
    {
        //Get tile was on and tile it moved to and has them raycast update
    }


}
