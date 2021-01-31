using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eInteractTypes
{
    move,
    rotClockwise,
    rotAntiClockwise,
    exit
}

public interface Interactable
{
    void Interact(eInteractTypes type);
}
