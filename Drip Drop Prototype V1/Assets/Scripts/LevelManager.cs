using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Main Game")]
    [SerializeField]
    Transform gameContainer;
    [SerializeField]
    Transform ball;
    [SerializeField]
    FloatVariable distTraveled;
    float startingY;
    float distanceY;

    [SerializeField]
    Transform startingTransform;

    [Header("Tiles")]
    [SerializeField]
    Vector3 lastSpawnedTilePos;
    List<Transform> tileList;
    [SerializeField]
    GameObject[] _tilePrefabs;

    [SerializeField]
    int maxGenerated = 10;
    int currentGenerated = 0;

    // Start is called before the first frame update
    void Start()
    {
        tileList = new List<Transform>();
        lastSpawnedTilePos = startingTransform.position;
        tileList.Add(startingTransform);
        currentGenerated++;
        startingY = ball.position.y;
        distTraveled.Value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGenerated < maxGenerated)
        {
            int random = Random.Range(0, _tilePrefabs.Length);
            GameObject obj = Instantiate(_tilePrefabs[random], lastSpawnedTilePos + (Vector3.down * 10), startingTransform.rotation);
            obj.transform.parent = gameContainer;
            lastSpawnedTilePos = obj.transform.position;
            tileList.Add(obj.transform);
            currentGenerated++;
        }

        for (int i = 0; i < tileList.Count; i++)
        {
            //Debug.Log("Tile Y pos: " + tileList[i].position.y + "Ball Pos + 15: " + (ball.position.y + 15));
            if (tileList[i].position.y > ball.position.y + 22f)
            {
                if (tileList[i].name == startingTransform.name)
                {
                    startingTransform.GetComponent<MeshRenderer>().enabled = false;
                    currentGenerated--;
                }
                else
                {
                    GameObject obj = tileList[i].gameObject;
                    tileList.Remove(tileList[i]);
                    Destroy(obj);
                    currentGenerated--;
                }
            }
        }

        distanceY = startingY - ball.position.y;

        if (ball.position.y < -2000)
        {
            foreach (Transform t in gameContainer.GetComponentsInChildren<Transform>())
            {
                if (t != gameContainer)
                {
                    t.position = t.position + (Vector3.up * distanceY);
                }
            }

            startingY += distanceY;
            lastSpawnedTilePos = tileList[0].position;
        }

        distTraveled.Value = distanceY;
    }
}
