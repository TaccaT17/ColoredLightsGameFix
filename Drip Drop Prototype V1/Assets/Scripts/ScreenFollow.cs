using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFollow : MonoBehaviour
{
    [SerializeField]
    Transform ball;
    [SerializeField]
    float lerpSpeed = 4;
    Vector3 startingPos;
    float yOffSet;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        yOffSet = transform.position.y - ball.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float Ypos = Mathf.Lerp(transform.position.y, ball.position.y + yOffSet, lerpSpeed * Time.deltaTime);

        Vector3 newPos = new Vector3(startingPos.x, Ypos, startingPos.z);
        transform.position = newPos;
    }
}
