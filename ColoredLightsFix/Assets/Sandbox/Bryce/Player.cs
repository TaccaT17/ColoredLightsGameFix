using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    void Interact();
}

public class Player : Movable
{

    private int[] facing;

    public void Init(int x, int z)
    {
        coords = new int[2] { x, z };
        facing = new int[2] { -1, 0 };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            SendMove(-1, 0);
        }
        else if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
        {
            SendMove(1, 0);
        }
        else if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            SendMove(0, -1);
        }
        else if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            SendMove(0, 1);
        }
        /*else if (Input.GetKeyDown("Space"))
        {
            Interact();
        }*/
    }

    //Tell the GridHandler where we want to move
    private void SendMove(int xMove, int zMove)
    {

        GridHandler.AttemptMovePlayer(coords[0], coords[1], xMove, zMove);
        facing = new int[2] { xMove, zMove };
        transform.LookAt(new Vector3(transform.position.x + xMove, transform.position.y, transform.position.z + zMove));

    }

    private void Interact()
    {
        GridHandler.AttemptInteract(coords[0], coords[1], facing[0], facing[1]);
    }

}
