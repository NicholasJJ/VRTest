using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordAndLoop : MonoBehaviour, func
{
    [SerializeField] AudioSourceLister aList;
    [SerializeField] soundViz sv;
    [SerializeField] AudioSource micAudio;
    [SerializeField] Material recordingMat;
    [SerializeField] Material playingMat;
    [SerializeField] AudioClip defaultClip;

    [SerializeField] float[] data;
    public bool recording;

    [SerializeField] private int shortStoreSamples;
    private float shortStoreTime;
    private int recordStartSample;
    private int recordEndSample;

    [SerializeField] float[] sums;
    private void Start() {
        recording = false;
    }

    private async void Update()
    {
        // AudioClip recorded = micAudio.clip;
        // int samplesInRecording = recorded.samples;
        // data = new float[samplesInRecording];
        // recorded.GetData(data,0);
        // sums = new float [4];
        // for (int i = 0; i < data.Length-1; i++) {
        //     int bin = Mathf.FloorToInt(i*4/data.Length);
        //     sums[bin] += data[i];
        // }
    }

    private void BeginRecord(){
        AudioClip recorded = micAudio.clip;
        recordStartSample = Mathf.FloorToInt((Time.time + sv.micStart + 2f) * recorded.frequency);
        recordStartSample = recordStartSample%(recorded.frequency * sv.shortRecordSeconds);
    }
    private async void EndRecord(){
        AudioClip recorded = micAudio.clip;
        recordEndSample = Mathf.FloorToInt((Time.time + sv.micStart + 2f) * recorded.frequency);
        recordEndSample = recordEndSample%(recorded.frequency * sv.shortRecordSeconds);
        data = new float[recorded.samples * recorded.channels];
        recorded.GetData(data,0);
        float dSum = 0;
        for (int i = 0; i < data.Length; i++) dSum += data[i];
        Debug.Log("!!!!!!!!!!!!!! Data is = " + dSum + "    length: " + data.Length);
        
        int clipSamples = recordEndSample - recordStartSample;
        if (clipSamples < 0) clipSamples = (data.Length - recordStartSample) + recordEndSample;
        Debug.Log("Clip is " + clipSamples + " long, from " + recordStartSample + " to " + recordEndSample);
        float[] clip = new float[clipSamples];
        for (int i  = 0; i < clipSamples; i++) {
            clip[i] = data[(recordStartSample + i)%data.Length];
        }

        AudioClip newSound = AudioClip.Create("new", clip.Length, recorded.channels, recorded.frequency, false);
        newSound.SetData(clip,0);


        AudioSource a = gameObject.AddComponent<AudioSource>();
        a.clip = newSound;
        // a.clip = defaultClip;
        a.loop = true;
        a.Play();
        aList.AddLoopingSound(a);
    }
    public void run(){
        recording = !recording;
        Debug.Log("Recording changed to " + recording);
        if (recording) {
            BeginRecord();
            GetComponent<Renderer>().material = recordingMat;
        } else {
            EndRecord();
            GetComponent<Renderer>().material = playingMat;
        }
    }
}
