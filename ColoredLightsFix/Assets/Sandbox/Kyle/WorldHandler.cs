using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using UnityEngine;

public class WorldHandler : MonoBehaviour
{
    public string LevelName;

    GameObject _playerOBJ;
    GameObject _floorOBJ;
    GameObject _yLightHouseOBJ;
    GameObject _bLightHouseOBJ;
    GameObject _rLightHouseOBJ;
    GameObject _yPressurePlateOBJ;
    GameObject _bPressurePlateOBJ;
    GameObject _rPressurePlateOBJ;

    GameObject _bridgeOBJ;
    GameObject _cornerObjectOBJ;
    GameObject _wallOBJ;

    GameObject[,] _floorTracker;
    GameObject[,] _objectTracker;
    private void Awake()
    {
        _playerOBJ = Resources.Load<GameObject>("Prefabs/Player");
        _floorOBJ = Resources.Load<GameObject>("Prefabs/Tile");
        _yLightHouseOBJ = Resources.Load<GameObject>("Prefabs/YellowLighthouse");
        _bLightHouseOBJ = Resources.Load<GameObject>("Prefabs/BlueLighthouse");
        _rLightHouseOBJ = Resources.Load<GameObject>("Prefabs/RedLighthouse");
        _yPressurePlateOBJ = Resources.Load<GameObject>("Prefabs/YellowPressurePlate");
        _bPressurePlateOBJ = Resources.Load<GameObject>("Prefabs/BluePressurePlate");
        _rPressurePlateOBJ = Resources.Load<GameObject>("Prefabs/RedPressurePlate");
        _bridgeOBJ = Resources.Load<GameObject>("Prefabs/Bridge");
        _cornerObjectOBJ = Resources.Load<GameObject>("Prefabs/CornerBridge");
        _wallOBJ = Resources.Load<GameObject>("Prefabs/Wall");

        GridHandler.SetWorldRef = this;
        GridHandler.LoadLevel(LevelName);
    }

    public void Initialize3DArea(int width, int length)
    {
        _floorTracker = new GameObject[width, length];
        _objectTracker = new GameObject[width, length];
    }

    public void Populate3DLevel(int xPos, int zPos, string id)
    {
        Vector3 spawnPos = new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z + zPos);

        GameObject newfloor = null;
        GameObject newobject = null;

        switch (id[0])
        {
            case 'P':
                Debug.Log("player made");
                newobject = Instantiate<GameObject>(_playerOBJ, spawnPos, _playerOBJ.transform.rotation, transform);
                newobject.AddComponent<Player>();
                newobject.GetComponent<Player>().Init(xPos, zPos);
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<Player>());
                newfloor = Instantiate<GameObject>(_floorOBJ, spawnPos, _floorOBJ.transform.rotation, transform);
                break;
            case 'O':
                Debug.Log("tile made");
                newobject = null;
                newfloor = Instantiate<GameObject>(_floorOBJ, spawnPos, _floorOBJ.transform.rotation, transform);
                break;
            case 'L':
                Debug.Log("light source made");
                string[] letters1 = Regex.Split(id, ":");
                newobject = Instantiate<GameObject>(_yLightHouseOBJ, spawnPos, _yLightHouseOBJ.transform.rotation, transform);
                newobject.AddComponent<TestObjectScript>();
                GridHandler.AddObjectToGrid(xPos, zPos, newobject.GetComponent<TestObjectScript>());
                if (letters1[1] == "S")
                {
                    newobject.transform.Rotate(Vector3.up, 90);
                }
                else if (letters1[1] == "W")
                {
                    newobject.transform.Rotate(Vector3.up, 180);
                }
                else if (letters1[1] == "N")
                {
                    newobject.transform.Rotate(Vector3.up, 270);
                }
                newfloor = Instantiate<GameObject>(_floorOBJ, spawnPos, _floorOBJ.transform.rotation, transform);
                break;
            case 'B':
                Debug.Log("bridge made");
                string[] letters2 = Regex.Split(id, ":");
                newobject = null;
                newfloor = Instantiate<GameObject>(_bridgeOBJ, spawnPos, _bridgeOBJ.transform.rotation, transform);
                if (letters2[1] == "V")
                {
                    newfloor.transform.Rotate(Vector3.up, 90);
                }
                //add script to brdige to do cool color stuff
                break;
            case 'W':
                Debug.Log("wall made");

                string[] letters3 = Regex.Split(id, ":");
                newobject = Instantiate<GameObject>(_wallOBJ, spawnPos, _wallOBJ.transform.rotation, transform);
                if (letters3[1] == "N")
                {
                    newobject.transform.Rotate(Vector3.up, 90);
                }
                else if (letters3[1] == "E")
                {
                    newobject.transform.Rotate(Vector3.up, 180);
                }
                else if (letters3[1] == "S")
                {
                    newobject.transform.Rotate(Vector3.up, 270);
                }
                newfloor = null;
                break;
            default:
                Debug.Log("nothing made");
                newobject = null;
                newfloor = null;
                break;
        }

        _objectTracker[xPos, zPos] = newobject;
        _floorTracker[xPos, zPos] = newfloor;
    }

    public void Update3DArea(int xPos, int zPos, int xNew, int zNew)
    {
        _objectTracker[xPos, zPos].transform.position = new Vector3(xNew, 0, zNew);
        GameObject tempObj = _objectTracker[xPos, zPos];
        _objectTracker[xPos, zPos] = null;
        _objectTracker[xNew, zNew] = tempObj;
        tempObj.GetComponent<Movable>().coords = new int[] { xNew, zNew };
    }
}
