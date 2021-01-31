using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField, Range(0, 10), Tooltip("Used to figure out if the object can be brocken by hitting it.\nIf it's strength is lower than the drop's strength, it breaks")]
    int _objStrength = 10;
    Rigidbody rb;

    DripController drip;

    [SerializeField]
    float _hitForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<DripController>())
        {
            drip = collision.collider.GetComponent<DripController>();

            if (drip.Strength > _objStrength)
            {
                BreakObj(drip.transform.position);
            }
        }
    }

    void BreakObj(Vector3 dripPos)
    {
        //TriangleExplosion explosion = gameObject.AddComponent<TriangleExplosion>();
        drip.Rb.velocity = drip.PreviousVelocity;
        //StartCoroutine(explosion.SplitMesh(false));
        //if(!GetComponent<Rigidbody>()) rb = gameObject.AddComponent<Rigidbody>();
        //rb.useGravity = false;
        //rb.constraints = RigidbodyConstraints.None;
        //rb.AddExplosionForce(_hitForce, dripPos, 1.3f);
        Destroy(gameObject);
        //WaitForDestroy(0f);
    }

    IEnumerator WaitForDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            /*
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            */
        }
        else
        {
            /*
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            */
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
