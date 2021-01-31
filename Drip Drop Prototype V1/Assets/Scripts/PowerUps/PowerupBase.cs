using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBase : MonoBehaviour
{

    MeshRenderer renderer;
    SphereCollider collider;
    DripController drip;

    public DripController Drip { get => drip; }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        //gameObject.SetActive(false);
        if (other.GetComponent<DripController>())
        {
            drip = other.GetComponent<DripController>();
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
