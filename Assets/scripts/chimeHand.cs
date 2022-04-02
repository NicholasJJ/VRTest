using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chimeHand : MonoBehaviour
{
    public OVRHand relatedHand;
    public bool rightHand;
    MeshRenderer r;
    public MeshRenderer th;
    Collider col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        r = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    
    private void OnCollisionEnter(Collision collision)
    {
        startShake();
        Debug.Log("in chime");
        if (collision.gameObject.CompareTag("Chime"))
        {
            AudioSource audioSource = collision.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            Debug.Log("play");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        endShake();
    }
    private void OnTriggerEnter(Collider other)
    {
        startShake();
        if (other.gameObject.CompareTag("Chime"))
        {
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            Debug.Log("is trigger");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        endShake();
    }
    void startShake()
    {
        if (rightHand)
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        else
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
    }
    void endShake()
    {
        if (rightHand)
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        else
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }
}
