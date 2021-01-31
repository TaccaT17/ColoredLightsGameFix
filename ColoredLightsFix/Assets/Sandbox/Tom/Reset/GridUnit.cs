using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnit : MonoBehaviour
{
    public GameObject gOGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        UpdateGrabbedGO();
    }

    public void UpdateGrabbedGO()
    {
        //shoot a raycast up and grab the object it hits

        RaycastHit[] hits = Physics.RaycastAll(this.gameObject.transform.position + new Vector3(0, 0.1f, 0), Vector3.up, 10f, 0, QueryTriggerInteraction.Collide);

        //go through hits and if you find something that isn't ground grab that, else grab ground, else be null

        if(hits.Length < 1)
        {
            gOGrabbed = null;
            return;
        }

        foreach (RaycastHit hit in hits)
        {
            GameObject tempGO = hit.collider.gameObject;

            if (gameObject.GetComponent<Player>() != null || gameObject.GetComponent<Lighthouse>() != null || gameObject.GetComponent<PressurePlate>() != null || gameObject.GetComponent<LightObject>() != null || gameObject.GetComponent<Obstacle>() != null || gameObject.GetComponent<Wall>() != null)
            {
                gOGrabbed = tempGO;
            }
        }



        //gOGrabbed = hits.collider.gameObject;
    }

}
