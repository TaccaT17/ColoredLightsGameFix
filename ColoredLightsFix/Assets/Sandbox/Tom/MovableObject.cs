using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : Movable, Interactable
{
    [SerializeField]
    private float unitDistance = 1f;

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
    }

    public void Interact()
    {
        Debug.Log("Interacted with!");
    }

}
