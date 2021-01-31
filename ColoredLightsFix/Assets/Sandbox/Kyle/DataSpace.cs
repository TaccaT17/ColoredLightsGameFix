using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSpace
{
    string _identifierType;
    string _identifierDetail;

    public DataSpace(string type, string detail)
    {
        _identifierType = type;
        _identifierDetail = detail;
    }

    public string GetIDType { get { return _identifierType; } }
    public string GetIDSpecific { get { return _identifierDetail; } }

}
