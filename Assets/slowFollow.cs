using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowFollow : MonoBehaviour
{
    [SerializeField] Transform targ;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(targ.localPosition.y);
        if (targ.localPosition.y > .2f)
        {
            Vector3 dir = targ.position - transform.position;
            float dist = dir.magnitude;
            if (dir.y < 0)
            {
                transform.position += Mathf.Min(2 * Time.deltaTime, dist) * dir.normalized;
            }
            else
            {
                transform.position += Mathf.Min(speed * Time.deltaTime, dist) * dir.normalized;
            }
        }
        
        
    }
}
