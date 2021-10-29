using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeAfterTime : MonoBehaviour
{
    [SerializeField] float waitTime;
    [SerializeField] float radius;
    [SerializeField] float force;
    [SerializeField] float lift;
    [SerializeField] GameObject boom;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + waitTime)
        {
            Collider[] cs = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider c in cs)
            {
                Debug.Log("pushing1");
                if (c.GetComponent<Rigidbody>())
                {
                    Debug.Log("pushing");
                    c.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, lift);
                }
            }
            Instantiate(boom,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
