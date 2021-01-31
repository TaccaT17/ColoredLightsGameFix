using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : Movable, Interactable
{
    [SerializeField]
    private float unitDistance = 1f;
    public bool bCanRotate = false;

    /*
    public void Move(GameManager.Direction direction)
    {
        switch (direction)
        {
            case GameManager.Direction.North:
                this.gameObject.transform.position += new Vector3(unitDistance, 0, 0);
                break;
            case GameManager.Direction.South:
                this.gameObject.transform.position += new Vector3(-unitDistance, 0, 0);
                break;
            case GameManager.Direction.East:
                this.gameObject.transform.position += new Vector3(0, 0, -unitDistance);
                break;
            case GameManager.Direction.West:
                this.gameObject.transform.position += new Vector3(0, 0, unitDistance);
                break;
            default:
                break;
        }
    }*/

    public void Interact(eInteractTypes type)
    {
        switch (type)
        {
            case eInteractTypes.move:
                //Debug.Log("Moved!");
                break;
            case eInteractTypes.rotClockwise:
                if (bCanRotate)
                {
                    //Debug.Log("Rotated clockwise!");
                    transform.Rotate(Vector3.up, 90);
                }
                break;
            case eInteractTypes.rotAntiClockwise:
                if (bCanRotate)
                {
                    //Debug.Log("Rotated counter clockwise!");
                    transform.Rotate(Vector3.up, -90);
                }
                break;
            default:
                break;
        }
    }

}
