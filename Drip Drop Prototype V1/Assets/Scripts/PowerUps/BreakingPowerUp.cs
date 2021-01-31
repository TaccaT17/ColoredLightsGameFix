using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPowerUp : PowerupBase
{
    [SerializeField]
    float _duration;
    [SerializeField]
    float _force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.GetComponent<DripController>())
        {
            Debug.Log("Start Coroutine");
            StartCoroutine(BreakPowerUp(_duration));
        }
    }

    IEnumerator BreakPowerUp(float duration)
    {
        int initStrength = Drip.Strength;
        Drip.Strength = 11;
        float startTime = Time.time;
        while (Time.time - startTime < duration * 0.60)
        {
            Drip.Rb.AddForce(Vector3.down * _force * Time.deltaTime, ForceMode.Force);
            yield return null;
        }

        while (Time.time - startTime < duration)
        {
            Debug.Log("Reduce Speed");
            if (Drip.Rb.velocity.magnitude > 5)
            {
                Drip.Rb.AddForce(Vector3.up * _force * 10, ForceMode.Force);
            }
            yield return null;
        }
        //Drip.Rb.AddForce(Drip.transform.forward * _force, ForceMode.Impulse);
        //yield return new WaitForSeconds(duration);
        Vector3 velocity = Drip.Rb.velocity;
        velocity *= 0.5f;
        Drip.Rb.velocity = velocity;
        Drip.Strength = initStrength;
        yield return new WaitForEndOfFrame();
    }
}
