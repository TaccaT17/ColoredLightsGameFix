using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    public void TurnOnLight()
    {
        //do thingy
    }
    public void TurnOffLight()
    {
        //do thingy
    }

    public GameManager.ColorOfLight color;

    public void Rotate(GameManager.Direction direction)
    {
        switch (direction)
        {
            case GameManager.Direction.North:
                this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case GameManager.Direction.South:
                this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
                break;
            case GameManager.Direction.East:
                this.gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            case GameManager.Direction.West:
                this.gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
                break;
            default:
                break;
        }
    }

}
