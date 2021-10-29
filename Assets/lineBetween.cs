using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineBetween : MonoBehaviour
{
    [SerializeField] Transform o1;
    [SerializeField] Transform o2;
    LineRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.SetPosition(0, o1.localPosition);
        renderer.SetPosition(1, o2.localPosition);
    }
}
