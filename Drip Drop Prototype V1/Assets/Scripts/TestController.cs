using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public bool explode = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (explode)
        {
            explode = false;
            TriangleExplosion explosion = gameObject.AddComponent<TriangleExplosion>();
            StartCoroutine(explosion.SplitMesh(true));
        }
    }
}
