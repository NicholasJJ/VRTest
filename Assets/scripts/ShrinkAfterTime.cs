using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkAfterTime : MonoBehaviour
{
    [SerializeField] float lifeTime;
    float shrinkTime;
    // Start is called before the first frame update
    void Start()
    {
        shrinkTime = Time.time + lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > shrinkTime)
        {
            transform.localScale -= Time.deltaTime * Vector3.one;
            if (transform.localScale.x < .01)
            {
                Destroy(gameObject);
            }
        }
    }
}
