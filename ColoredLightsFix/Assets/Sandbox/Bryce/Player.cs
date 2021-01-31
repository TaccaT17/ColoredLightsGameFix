using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movable
{

    private int[] facing = new int[2] { -1, 0 };
    private bool bHolding = false;


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
        else if (Input.GetKeyDown("space"))
        {
            Grab();
        }
        else if (Input.GetKeyDown("q"))
        {
            RotateClockwise();
        }
        else if (Input.GetKeyDown("e"))
        {
            RotateAntiClockwise();
        }
    }

    //Tell the GridHandler where we want to move
    private void SendMove(int xMove, int zMove)
    {
        //restrict movement to forward and back while holding things
        if (bHolding)
        {
            if ((xMove + facing[0]) % 2 != 0)
            {
                //Debug.Log("Moved sideways! Only move forward/back while holding");
                return;
            }

            if (xMove != facing[0] || zMove != facing[1])
            {
                //Debug.Log("Moved away from facing, moving player first");
                int[] tempCoords = coords;
                if (MovePlayer(xMove, zMove))
                {
                    if (!MoveHeldObject(tempCoords, xMove, zMove))
                    {
                        Drop();
                    }
                }
            }
            else
            {
                //Debug.Log("Moved toward facing, moving object first");
                if (MoveHeldObject(coords, xMove, zMove))
                {
                    MovePlayer(xMove, zMove);
                }
            }
            return;
        }
        MovePlayer(xMove, zMove);
    }

    private bool MoveHeldObject(int[] objectCoords, int xMove, int zMove)
    {
        //Debug.Log("Facing: " + facing[0] + "," + facing[1] + ", Coords: " + objectCoords[0] + "," + objectCoords[1] + ", move: " + xMove + "," + zMove);
        return (GridHandler.AttemptMoveObject(objectCoords[0] + facing[0], objectCoords[1] + facing[1], xMove, zMove));
    }

    private bool MovePlayer(int xMove, int zMove)
    {
        bool returnValue = GridHandler.AttemptMoveObject(coords[0], coords[1], xMove, zMove);
        if (!bHolding)
        {
            //Debug.Log("Facing new direction!");
            facing = new int[2] { xMove, zMove };
            transform.LookAt(new Vector3(transform.position.x + xMove, transform.position.y, transform.position.z + zMove));
        }
        return returnValue;
    }

    private void Grab()
    {
        if (!bHolding)
        {
            if (GridHandler.AttemptInteract(coords[0], coords[1], facing[0], facing[1], eInteractTypes.move))
            {
                Debug.Log("Grabbed an object!");
                bHolding = true;
            }
            else
            {
                Debug.Log("Nothing to grab!");
            }
        }
        else
        {
            Drop();
        }
    }

    private void Drop()
    {
        bHolding = false;
        //Debug.Log("Dropping held object!");
    }

    private void RotateClockwise()
    {
        GridHandler.AttemptInteract(coords[0], coords[1], facing[0], facing[1], eInteractTypes.rotClockwise);
    }

    private void RotateAntiClockwise()
    {
        GridHandler.AttemptInteract(coords[0], coords[1], facing[0], facing[1], eInteractTypes.rotAntiClockwise);
    }

}
