using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonRock : MonoBehaviour
{
    public GameObject rock;
    public LineRenderer lazorLine;
    public GameObject lazorBomb;
    public Transform worldStatic;
    AudioClip a;
    int sampleWindow = 128;
    public float threshhold;
    public float bangThreshhold;
    float lastSummon;
    public float summonReload;
    string micName;
    float peakVolume;

    float lastRockSummon = 0;
    [SerializeField] int[] volList;
    int volIndex;
    int avgVolume;
    [SerializeField] int SampleFrames;
    [SerializeField] bool isLoud;
    [SerializeField] bool wasLoud;
    float lastLoud;
    bool lazored;
    float loudStart;
    // Start is called before the first frame update
    void Start()
    {
        micName = Microphone.devices[1];
        a = Microphone.Start(micName, true, 1, AudioSettings.outputSampleRate);
        while (!(Microphone.GetPosition(micName) > 0)) { }
        volList = new int[SampleFrames];
        volIndex = 0;
        avgVolume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float v = getVolume();
        volIndex += 1;
        int m = volIndex % SampleFrames;
        avgVolume -= volList[m];
        int n = 0;
        if (v > threshhold)
        {
            n = 1;
        }
        avgVolume += n;
        volList[m] = n;
        isLoud = (avgVolume > SampleFrames / 2);
        
        //Debug.Log(v);
        //isLoud = (v > threshhold);
        if (isLoud)
        {
            lastLoud = Time.time;
        }
        if (!isLoud && Time.time < lastLoud + .1)
        {
            isLoud = true;
        }
        if (isLoud && !wasLoud)
        {
            lazored = false;
            peakVolume = v;
            loudStart = Time.time;
        }
        if (isLoud)
        {
            peakVolume = Mathf.Max(peakVolume, v);
        }
        if (isLoud && Time.time > loudStart + .5 && Time.time > lastRockSummon + 3)
        {
            //lazor
            lazored = true;
            lastSummon = Time.time;
            Debug.Log("BWAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            lazor();
        }
        if(!isLoud && wasLoud)
        {
            unLazor();
        }
        if (!isLoud && wasLoud && !lazored && peakVolume > bangThreshhold && Time.time > lastSummon + summonReload)
        {
            //rock
            lastRockSummon = Time.time;
            lastSummon = Time.time;
            summon();
        }
        wasLoud = isLoud;
    }

    void summon()
    {
        Vector3 point = transform.position + transform.forward + 0.2f*Vector3.up;
        Debug.Log(point);
        GameObject newRock = Instantiate(rock, point + 3.2f * Vector3.down, Quaternion.identity, worldStatic);
        newRock.GetComponent<rockThrow>().target = point;
    }

    void lazor()
    {
        Vector3 lStart = transform.position - 0.1f * transform.up;
        Vector3 lEnd = lStart + 100 * transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(lStart,transform.forward,out hit, 100))
        {
            lEnd = hit.point;
            if (Random.value > 0)
            {
                Instantiate(lazorBomb, hit.point, Quaternion.identity);
            }
        }
        lazorLine.SetPosition(0, lStart);
        lazorLine.SetPosition(1, lEnd);
        lazorLine.enabled = true;
    }
    void unLazor()
    {
        lazorLine.enabled = false;
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
