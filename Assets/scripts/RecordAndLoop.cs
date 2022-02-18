using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordAndLoop : MonoBehaviour, func
{
    [SerializeField] AudioSource micAudio;
    [SerializeField] Material recordingMat;
    [SerializeField] Material playingMat;
    [SerializeField] AudioClip defaultClip;
    public bool recording;
    private float recordStartTime;
    private void Start() {
        recording = false;
    }

    private void BeginRecord(){
        recordStartTime = Time.time;
    }
    private void EndRecord(){
        AudioClip recorded = micAudio.clip;
        float timePassed = Time.time - recordStartTime;
        int samplesInRecording = Mathf.FloorToInt(recorded.frequency * timePassed);
        float[] data = new float[samplesInRecording * recorded.channels];
        recorded.GetData(data,recorded.samples - samplesInRecording);
        AudioClip newSound = AudioClip.Create("new",samplesInRecording,recorded.channels,recorded.frequency,false,false);
        newSound.SetData(data,0);


        AudioSource a = gameObject.AddComponent<AudioSource>();
        a.clip = newSound;
        // a.clip = defaultClip;
        a.loop = true;
        a.Play();
    }
    public void run(){
        recording = !recording;
        if (recording) {
            BeginRecord();
            GetComponent<Renderer>().material = recordingMat;
        } else {
            EndRecord();
            GetComponent<Renderer>().material = playingMat;
        }
    }
}
