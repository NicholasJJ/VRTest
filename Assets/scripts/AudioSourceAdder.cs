using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceAdder : MonoBehaviour
{
    [SerializeField] AudioSourceLister asl;
    // Start is called before the first frame update
    void Start()
    {
        asl.AddSoundMaker(GetComponent<AudioSource>());
    }
}
