using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordAndLoop : MonoBehaviour, func
{
    [SerializeField] AudioSource micAudio;
    [SerializeField] Material recordingMat;
    [SerializeField] Material playingMat;
    [SerializeField] AudioClip defaultClip;

    [SerializeField] float[] data;
    public bool recording;

    [SerializeField] private int shortStoreSamples;
    private float shortStoreTime;
    private float recordStartTime;
    private void Start() {
        recording = false;
    }

    private void Update()
    {

    }

    private void BeginRecord(){
        recordStartTime = Time.time;
    }
    private void EndRecord(){
        AudioClip recorded = micAudio.clip;
        int samplesInRecording = Mathf.FloorToInt(recorded.frequency * (Time.time - recordStartTime));
        data = new float[samplesInRecording * recorded.channels];
        recorded.GetData(data,recorded.samples - samplesInRecording);
        float dSum = 0;
        for (int i = 0; i < data.Length; i++) dSum += data[i];
        Debug.Log("!!!!!!!!!!!!!! Data is = " + dSum + "    length: " + data.Length);
        AudioClip newSound = AudioClip.Create("new", data.Length, recorded.channels, recorded.frequency, false);
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
