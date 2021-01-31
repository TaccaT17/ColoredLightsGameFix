using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Rendering;

public class WorldHandler : MonoBehaviour
{
    public string LevelName;

    GameObject _playerOBJ;
    GameObject _grassTileFloorOBJ;
    GameObject _pathTileFloorOBJ;
    GameObject _lighthouseTileFloorOBJ;
    GameObject _yLightHouseOBJ;
    GameObject _bLightHouseOBJ;
    GameObject _rLightHouseOBJ;
    GameObject _yPressurePlateOBJ;
    GameObject _bPressurePlateOBJ;
    GameObject _rPressurePlateOBJ;
    GameObject _bridgeOBJ;
    GameObject _cornerObjectOBJ;
    GameObject _wallOBJ;
    GameObject _rockOBJ;

    GameObject[,] _floorTracker;
    GameObject[,] _objectTracker;
    private void Awake()
    {
        _playerOBJ = Resources.Load<GameObject>("Prefabs/Player");
        _grassTileFloorOBJ = Resources.Load<GameObject>("Prefabs/TileGrass");
        _pathTileFloorOBJ = Resources.Load<GameObject>("Prefabs/TilePath");
        _lighthouseTileFloorOBJ = Resources.Load<GameObject>("Prefabs/TileLighthouse");
        _yLightHouseOBJ = Resources.Load<GameObject>("Prefabs/YellowLighthouse");
        _bLightHouseOBJ = Resources.Load<GameObject>("Prefabs/BlueLighthouse");
        _rLightHouseOBJ = Resources.Load<GameObject>("Prefabs/RedLighthouse");
        _yPressurePlateOBJ = Resources.Load<GameObject>("Prefabs/YellowPressurePlate");
        _bPressurePlateOBJ = Resources.Load<GameObject>("Prefabs/BluePressurePlate");
        _rPressurePlateOBJ = Resources.Load<GameObject>("Prefabs/RedPressurePlate");
        _bridgeOBJ = Resources.Load<GameObject>("Prefabs/Bridge");
        _cornerObjectOBJ = Resources.Load<GameObject>("Prefabs/CornerBridge");
        _wallOBJ = Resources.Load<GameObject>("Prefabs/Wall");
        _rockOBJ = Resources.Load<GameObject>("Prefabs/Rock");

        GridHandler.SetWorldRef = this;
        GridHandler.LoadLevel(LevelName);
    }

    public void Initialize3DArea(int width, int length)
    {
        _floorTracker = new GameObject[width, length];
        _objectTracker = new GameObject[width, length];
    }

    public void Populate3DLevel(int xPos, int zPos, string[] id)
    {
        Vector3 spawnPos = new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z + zPos);

        GameObject newfloor = null;
        GameObject newobject = null;



        switch (id[0])
        {
            case "P":
                //Debug.Log("player made");
                newobject = Instantiate<GameObject>(_playerOBJ, spawnPos, _playerOBJ.transform.rotation, transform);
                newobject.AddComponent<Player>();
                newobject.GetComponent<Player>().Init(xPos, zPos);
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<Player>());
                newfloor = Instantiate<GameObject>(_grassTileFloorOBJ, spawnPos, _grassTileFloorOBJ.transform.rotation, transform);
                break;
            case "F":
                //Debug.Log("tile made");
                newobject = null;
                newfloor = CheckWhatFloor(id[1], spawnPos);
                break;
            case "RL":
                //Debug.Log("light source made");

                newobject = Instantiate<GameObject>(_rLightHouseOBJ, spawnPos, _rLightHouseOBJ.transform.rotation, transform);
                newobject.AddComponent<MovableObject>();
                newobject.GetComponent<MovableObject>().bCanRotate = true;
                newobject.AddComponent<Lighthouse>();
                newobject.GetComponent<Lighthouse>().color = GameManager.ColorOfLight.red;
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<MovableObject>());
                CheckObjectOrientation(id[1], newobject);

                newfloor = Instantiate<GameObject>(_lighthouseTileFloorOBJ, spawnPos, _lighthouseTileFloorOBJ.transform.rotation, transform);
                break;
            case "BL":
                //Debug.Log("light source made");

                newobject = Instantiate<GameObject>(_bLightHouseOBJ, spawnPos, _bLightHouseOBJ.transform.rotation, transform);
                newobject.AddComponent<MovableObject>();
                newobject.GetComponent<MovableObject>().bCanRotate = true;
                newobject.AddComponent<Lighthouse>();
                newobject.GetComponent<Lighthouse>().color = GameManager.ColorOfLight.blue;
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<MovableObject>());
                CheckObjectOrientation(id[1], newobject);

                newfloor = Instantiate<GameObject>(_lighthouseTileFloorOBJ, spawnPos, _lighthouseTileFloorOBJ.transform.rotation, transform);
                break;
            case "YL":
                //Debug.Log("light source made");

                newobject = Instantiate<GameObject>(_yLightHouseOBJ, spawnPos, _yLightHouseOBJ.transform.rotation, transform);
                newobject.AddComponent<MovableObject>();
                newobject.GetComponent<MovableObject>().bCanRotate = true;
                newobject.AddComponent<Lighthouse>();
                newobject.GetComponent<Lighthouse>().color = GameManager.ColorOfLight.yellow;
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<MovableObject>());
                CheckObjectOrientation(id[1], newobject);

                newfloor = Instantiate<GameObject>(_lighthouseTileFloorOBJ, spawnPos, _lighthouseTileFloorOBJ.transform.rotation, transform);
                break;
            case "B":
                //Debug.Log("bridge made");
                newobject = null;
                newfloor = Instantiate<GameObject>(_bridgeOBJ, spawnPos, _bridgeOBJ.transform.rotation, transform);
                if (id[1] == "V")
                {
                    newfloor.transform.Rotate(Vector3.up, 90);
                }
                //add script to brdige to do cool color stuff
                break;
            case "CB":
                //Debug.Log("bridge made");
                newobject = null;

                newfloor = Instantiate<GameObject>(_cornerObjectOBJ, spawnPos, _cornerObjectOBJ.transform.rotation, transform);
                CheckCornerBridgeOrientation(id[1], newfloor);
                break;
            case "W":
                //Debug.Log("wall made");
                newobject = Instantiate<GameObject>(_wallOBJ, spawnPos, _wallOBJ.transform.rotation, transform);
                CheckObjectOrientation(id[1], newobject);

                newfloor = Instantiate<GameObject>(_grassTileFloorOBJ, spawnPos, _grassTileFloorOBJ.transform.rotation, transform);
                break;
            case "R":
                //Debug.Log("rock made");
                newobject = Instantiate<GameObject>(_rockOBJ, spawnPos, _rockOBJ.transform.rotation, transform);
                newobject.AddComponent<MovableObject>();
                newobject.GetComponent<MovableObject>().Init(xPos, zPos);
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<MovableObject>());
                newfloor = Instantiate<GameObject>(_grassTileFloorOBJ, spawnPos, _grassTileFloorOBJ.transform.rotation, transform);
                break;
            default:
                //Debug.Log("nothing made");
                newobject = null;
                newfloor = null;
                break;
        }

        _objectTracker[xPos, zPos] = newobject;
        _floorTracker[xPos, zPos] = newfloor;
    }

    private GameObject CheckWhatFloor(string type, Vector3 spawnPos)
    {
        GameObject thingtospawn = null;
        switch (type)
        {
            case "RPP":
                thingtospawn = Instantiate<GameObject>(_rPressurePlateOBJ, spawnPos, _rPressurePlateOBJ.transform.rotation, transform);
                break;
            case "BPP":
                thingtospawn = Instantiate<GameObject>(_bPressurePlateOBJ, spawnPos, _bPressurePlateOBJ.transform.rotation, transform);
                break;
            case "YPP":
                thingtospawn = Instantiate<GameObject>(_yPressurePlateOBJ, spawnPos, _yPressurePlateOBJ.transform.rotation, transform);
                break;
            case "G":
                thingtospawn = Instantiate<GameObject>(_grassTileFloorOBJ, spawnPos, _grassTileFloorOBJ.transform.rotation, transform);
                break;
            case "WP":
                thingtospawn = Instantiate<GameObject>(_pathTileFloorOBJ, spawnPos, _pathTileFloorOBJ.transform.rotation, transform);
                break;
            case "LP":
                thingtospawn = Instantiate<GameObject>(_lighthouseTileFloorOBJ, spawnPos, _lighthouseTileFloorOBJ.transform.rotation, transform);
                break;
            default:
                break;
        }

        return thingtospawn;
    }

    private void CheckObjectOrientation(string dir, GameObject toRotate)
    {
        if (dir == "S")
        {
            toRotate.transform.Rotate(Vector3.up, 90);
        }
        else if (dir == "W")
        {
            toRotate.transform.Rotate(Vector3.up, 180);
        }
        else if (dir == "N")
        {
            toRotate.transform.Rotate(Vector3.up, 270);
        }
    }

    private void CheckCornerBridgeOrientation(string dir, GameObject toRotate)
    {
        if (dir == "DL")
        {
            toRotate.transform.Rotate(Vector3.up, 90);
        }
        else if (dir == "UL")
        {
            toRotate.transform.Rotate(Vector3.up, 180);
        }
        else if (dir == "UR")
        {
            toRotate.transform.Rotate(Vector3.up, 270);
        }
    }

    public void Update3DArea(int xPos, int zPos, int xNew, int zNew)
    {
        _objectTracker[xPos, zPos].transform.position = new Vector3(xNew, 0, zNew);
        GameObject tempObj = _objectTracker[xPos, zPos];
        _objectTracker[xPos, zPos] = null;
        _objectTracker[xNew, zNew] = tempObj;
        tempObj.GetComponent<Movable>().coords = new int[] { xNew, zNew };
    }

    public bool AttemptInteract(int xPos, int zPos, eInteractTypes type)
    {
        if (_objectTracker[xPos, zPos].GetComponent<Interactable>() != null)
        {
            _objectTracker[xPos, zPos].GetComponent<Interactable>().Interact(type);
            return true;
        }
        return false;
    }

    public bool IsObjectLighthouse(int xPos, int zPos)
    {
        Debug.Log("Is lighthouse: " + (_objectTracker[xPos, zPos].GetComponent<Lighthouse>() != null));
        return _objectTracker[xPos, zPos].GetComponent<Lighthouse>() != null;
    }

}
