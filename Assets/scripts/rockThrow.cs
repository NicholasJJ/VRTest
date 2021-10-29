using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockThrow : MonoBehaviour
{
    public float speed;
    public float floatSpeed;
    bool punched;
    public Vector3 target;
    Transform player;
    Rigidbody r;
    // Start is called before the first frame update
    void Start()
    {
        punched = false;
        r = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!punched)
        {
            r.velocity = (0.5f * r.velocity) + (floatSpeed * (target - transform.position));
            if (Vector3.Distance(transform.position,target) < .1)
            {
                GetComponent<Collider>().enabled = true;
            }
            transform.Rotate(new Vector3(0, 30 * Time.deltaTime, 0), Space.World);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.IsChildOf(player))
        {
            punched = true;
            Vector3 dir = transform.position - collision.gameObject.transform.position;
            r.velocity = speed * dir.normalized;
        }
    }
}
