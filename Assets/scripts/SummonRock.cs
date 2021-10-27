using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonRock : MonoBehaviour
{
    public GameObject rock;
    public Transform worldStatic;
    AudioClip a;
    int sampleWindow = 128;
    public float threshhold;
    float lastSummon;
    public float summonReload;
    string micName;
    // Start is called before the first frame update
    void Start()
    {
        micName = Microphone.devices[1];
        a = Microphone.Start(micName, true, 1, AudioSettings.outputSampleRate);
        while (!(Microphone.GetPosition(micName) > 0)) { }
    }

    // Update is called once per frame
    void Update()
    {
        float v = getVolume();
        //Debug.Log(v);
        if (v > threshhold && Time.time > lastSummon + summonReload)
        {
            lastSummon = Time.time;
            summon();
        }
    }

    void summon()
    {
        Vector3 point = transform.position + transform.forward + 0.2f*Vector3.up;
        Debug.Log(point);
        GameObject newRock = Instantiate(rock, point + 3.2f * Vector3.down, Quaternion.identity, worldStatic);
        newRock.GetComponent<rockThrow>().target = point;
    }

    float getVolume()
    {
        float[] data = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(micName) - sampleWindow + 1;
        if (micPosition < 0)
            return 0;
        a.GetData(data, micPosition);
        float max = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            float peak = data[i];
            max = Mathf.Max(max, peak);
        }
        return max;
    }
}
