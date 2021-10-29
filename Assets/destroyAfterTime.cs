using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAfterTime : MonoBehaviour
{
    float deathTime;
    // Start is called before the first frame update
    void Start()
    {
        deathTime = Time.time + GetComponent<ParticleSystem>().duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > deathTime)
        {
            Destroy(gameObject);
        }
    }
}
