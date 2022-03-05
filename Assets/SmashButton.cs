using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashButton : MonoBehaviour
{
    [SerializeField] float pushDist;
    [SerializeField] bool pressed;

    [SerializeField] bool EditorClick;
    bool lastPressed;
    Rigidbody r;
    Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        start = transform.position;
        lastPressed = pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        r.velocity = 8*(start - transform.position);
        pressed = (start.y - transform.position.y > pushDist);
        if ((pressed && !lastPressed) || EditorClick)
        {
            EditorClick = false;
            GetComponent<func>().run();
        }
        lastPressed = pressed;
    }
    
 
}
