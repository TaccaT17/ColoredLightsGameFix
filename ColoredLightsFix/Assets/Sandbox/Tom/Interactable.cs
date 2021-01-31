using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eInteractTypes
{
    move,
    rotClockwise,
    rotAntiClockwise
}

public interface Interactable
{
    void Interact(eInteractTypes type);
}
