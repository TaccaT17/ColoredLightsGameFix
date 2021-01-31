using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSpace
{
    string _identifier;

    public DataSpace(string id)
    {
        _identifier = id;
    }

    public string GetID { get { return _identifier; } }

}
