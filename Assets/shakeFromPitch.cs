using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeFromPitch : MonoBehaviour
{
    public enum Pitch
    {
        High,
        Medium,
        Low
    }
    public soundViz t;
    public float scalar;
    public Pitch HML;
    Vector3 start;
    [SerializeField] float p;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (HML == Pitch.Low)
        {
            p = t.lowSum;
        }
        else if (HML == Pitch.Medium)
        {
            p = t.midSum;
        }
        else if (t)
        {
            p = t.highSum;
        }
        
        p *= scalar;
        Vector2 shift = Random.insideUnitCircle * p;
        transform.position = new Vector3(start.x + shift.x, start.y, start.z + shift.y);
    }
}
