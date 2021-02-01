using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Rendering;

public class WorldHandler : MonoBehaviour
{
    //public string LevelName;

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
    GameObject _chestOBJ;

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
        _chestOBJ = Resources.Load<GameObject>("Prefabs/Chest");

        GridHandler.SetWorldRef = this;
        //GridHandler.LoadLevel(LevelName);
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
            case "E":
                newobject = Instantiate<GameObject>(_chestOBJ, spawnPos, _chestOBJ.transform.rotation, transform);
                newfloor = Instantiate<GameObject>(_pathTileFloorOBJ, spawnPos, _pathTileFloorOBJ.transform.rotation, transform);
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<Exit>());
                break;
            case "RL":
                //Debug.Log("light source made");

                newobject = Instantiate<GameObject>(_rLightHouseOBJ, spawnPos, _rLightHouseOBJ.transform.rotation, transform);
                newobject.AddComponent<MovableObject>();
                newobject.GetComponent<MovableObject>().bCanRotate = true;
                newobject.AddComponent<Lighthouse>();
                newobject.GetComponent<Lighthouse>().color = GameManager.ColorOfLight.red;
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<Lighthouse>());
                CheckObjectOrientation(id[1], newobject);
                FindCorrectPressurePlate("RPP", newobject.GetComponent<Lighthouse>());

                newfloor = Instantiate<GameObject>(_lighthouseTileFloorOBJ, spawnPos, _lighthouseTileFloorOBJ.transform.rotation, transform);
                break;
            case "BL":
                //Debug.Log("light source made");

                newobject = Instantiate<GameObject>(_bLightHouseOBJ, spawnPos, _bLightHouseOBJ.transform.rotation, transform);
                newobject.AddComponent<MovableObject>();
                newobject.GetComponent<MovableObject>().bCanRotate = true;
                newobject.AddComponent<Lighthouse>();
                newobject.GetComponent<Lighthouse>().color = GameManager.ColorOfLight.blue;
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<Lighthouse>());
                CheckObjectOrientation(id[1], newobject);
                FindCorrectPressurePlate("BPP", newobject.GetComponent<Lighthouse>());

                newfloor = Instantiate<GameObject>(_lighthouseTileFloorOBJ, spawnPos, _lighthouseTileFloorOBJ.transform.rotation, transform);
                break;
            case "YL":
                //Debug.Log("light source made");

                newobject = Instantiate<GameObject>(_yLightHouseOBJ, spawnPos, _yLightHouseOBJ.transform.rotation, transform);
                newobject.AddComponent<MovableObject>();
                newobject.GetComponent<MovableObject>().bCanRotate = true;
                newobject.AddComponent<Lighthouse>();
                newobject.GetComponent<Lighthouse>().color = GameManager.ColorOfLight.yellow;
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<Lighthouse>());
                CheckObjectOrientation(id[1], newobject);
                FindCorrectPressurePlate("YPP", newobject.GetComponent<Lighthouse>());

                newfloor = Instantiate<GameObject>(_lighthouseTileFloorOBJ, spawnPos, _lighthouseTileFloorOBJ.transform.rotation, transform);
                break;
            case "B":
                //Debug.Log("bridge made");
                newobject = null;
                newfloor = Instantiate<GameObject>(_bridgeOBJ, spawnPos, _bridgeOBJ.transform.rotation, transform);
                SetUpBridge(id[1], id[2], newfloor);
                break;
            case "CB":
                //Debug.Log("bridge made");
                newobject = null;

                newfloor = Instantiate<GameObject>(_cornerObjectOBJ, spawnPos, _cornerObjectOBJ.transform.rotation, transform);
                SetUpCornerBridge(id[1], id[2], newfloor);
                break;
            case "W":
                //Debug.Log("wall made");
                newobject = Instantiate<GameObject>(_wallOBJ, spawnPos, _wallOBJ.transform.rotation, transform);
                CheckObjectOrientation(id[1], newobject);
                newobject.AddComponent<Obstacle>();
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<Obstacle>());

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
                thingtospawn.AddComponent<PressurePlate>();
                FindCorrectLighthouse(GameManager.ColorOfLight.red, thingtospawn.GetComponent<PressurePlate>());

                break;
            case "BPP":
                thingtospawn = Instantiate<GameObject>(_bPressurePlateOBJ, spawnPos, _bPressurePlateOBJ.transform.rotation, transform);
                thingtospawn.AddComponent<PressurePlate>();
                FindCorrectLighthouse(GameManager.ColorOfLight.blue, thingtospawn.GetComponent<PressurePlate>());
                break;
            case "YPP":
                thingtospawn = Instantiate<GameObject>(_yPressurePlateOBJ, spawnPos, _yPressurePlateOBJ.transform.rotation, transform);
                thingtospawn.AddComponent<PressurePlate>();
                FindCorrectLighthouse(GameManager.ColorOfLight.blue, thingtospawn.GetComponent<PressurePlate>());
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

    private void SetUpBridge(string color, string orientaion, GameObject bridge)
    {
        switch (color)
        {
            case "R":
                bridge.GetComponent<LightObjectSeer>().lightobject.redLitsNeeded = true;
                break;
            case "Y":
                bridge.GetComponent<LightObjectSeer>().lightobject.yellowLitsNeeded = true;
                break;
            case "G":
                bridge.GetComponent<LightObjectSeer>().lightobject.yellowLitsNeeded = true;
                bridge.GetComponent<LightObjectSeer>().lightobject.blueLitsNeeded = true;
                break;
            case "B":
                bridge.GetComponent<LightObjectSeer>().lightobject.blueLitsNeeded = true;
                break;
            case "O":
                bridge.GetComponent<LightObjectSeer>().lightobject.redLitsNeeded = true;
                bridge.GetComponent<LightObjectSeer>().lightobject.yellowLitsNeeded = true;
                break;
            case "P":
                bridge.GetComponent<LightObjectSeer>().lightobject.redLitsNeeded = true; 
                bridge.GetComponent<LightObjectSeer>().lightobject.blueLitsNeeded = true;
                break;
            default:
                break;
        }

        if(orientaion == "V")
        {
            bridge.transform.Rotate(Vector3.up, 90);
        }
    }

    private void SetUpCornerBridge(string color, string orientaion, GameObject cornerBridge)
    {
        switch (color)
        {
            case "R":
                cornerBridge.GetComponent<LightObjectSeer>().lightobject.redLitsNeeded = true;
                break;
            case "Y":
                cornerBridge.GetComponent<LightObjectSeer>().lightobject.yellowLitsNeeded = true;
                break;
            case "G":
                cornerBridge.GetComponent<LightObjectSeer>().lightobject.yellowLitsNeeded = true;
                cornerBridge.GetComponent<LightObjectSeer>().lightobject.blueLitsNeeded = true;
                break;
            case "B":
                cornerBridge.GetComponent<LightObjectSeer>().lightobject.blueLitsNeeded = true;
                break;
            case "O":
                cornerBridge.GetComponent<LightObjectSeer>().lightobject.redLitsNeeded = true;
                cornerBridge.GetComponent<LightObjectSeer>().lightobject.yellowLitsNeeded = true;
                break;
            case "P":
                cornerBridge.GetComponent<LightObjectSeer>().lightobject.redLitsNeeded = true;
                cornerBridge.GetComponent<LightObjectSeer>().lightobject.blueLitsNeeded = true;
                break;
            default:
                break;
        }

        if (orientaion == "DL")
        {
            cornerBridge.transform.Rotate(Vector3.up, 90);
        }
        else if (orientaion == "UL")
        {
            cornerBridge.transform.Rotate(Vector3.up, 180);
        }
        else if (orientaion == "UR")
        {
            cornerBridge.transform.Rotate(Vector3.up, 270);
        }
    }

    private void FindCorrectPressurePlate(string id, Lighthouse lighthouse)
    {
        for (int i = 0; i < _floorTracker.GetLength(0); i++)
        {
            for (int j = 0; j < _floorTracker.GetLength(1); j++)
            {
                if(GridHandler.CheckFloorLayout[i, j] != null && GridHandler.CheckFloorLayout[i,j].GetIDSpecific == id)
                {
                    _floorTracker[i, j].GetComponent<PressurePlate>().lightHouseRef = lighthouse;
                }
            }
        }
    }

    private void FindCorrectLighthouse(GameManager.ColorOfLight color, PressurePlate plate)
    {
        for (int i = 0; i < _floorTracker.GetLength(0); i++)
        {
            for (int j = 0; j < _floorTracker.GetLength(1); j++)
            {
                if ( GridHandler.CheckObjectLayout[i, j] != null && GridHandler.CheckObjectLayout[i, j].GetType() == typeof(Lighthouse))
                {
                    if(_objectTracker[i,j].GetComponent<Lighthouse>().GetLighthouseColor == color)
                    {
                        plate.lightHouseRef = _objectTracker[i, j].GetComponent<Lighthouse>();
                    }
                }
            }
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
        //Debug.Log("Is lighthouse: " + (_objectTracker[xPos, zPos].GetComponent<Lighthouse>() != null));
        return _objectTracker[xPos, zPos].GetComponent<Lighthouse>() != null;
    }
    
    public void ActivatePressurePlate(int xPos, int zPos)
    {
        _floorTracker[xPos, zPos].GetComponent<PressurePlate>().ActivatePressurePlate();
    }

    public void DeactivatePressurePlate(int xPos, int zPos)
    {
        _floorTracker[xPos, zPos].GetComponent<PressurePlate>().DeactivatePressurePlate();
    }

    public bool IsBridgeActive(int xPos, int zPos)
    {
        return _floorTracker[xPos, zPos].GetComponent<LightObjectSeer>().lightobject.Stepable;
    }

    public void ResetLevel()
    {
        if(_floorTracker.Length > 0)
        {
            for (int i = 0; i < _floorTracker.GetLength(0); i++)
            {
                for (int j = 0; j < _floorTracker.GetLength(1); j++)
                {
                    if (_floorTracker[i, j] != null)
                    {
                        Destroy(_floorTracker[i, j].gameObject);
                        _floorTracker[i, j] = null;
                    }

                    if (_objectTracker[i, j] != null)
                    {
                        Destroy(_objectTracker[i, j].gameObject);
                        _objectTracker[i, j] = null;
                    }
                }
            }
        }
        
    }
}
