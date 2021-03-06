using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundViz : MonoBehaviour
{
    [SerializeField] AudioSourceLister aList;
    public float step;
    public float max;
    public float[] spectrum = new float[128];
    public float midMinF;
    public float maxMinF;
    int midMin;
    int maxMin;
    public float highSum;
    public float midSum;
    public float lowSum;
    public Material ml;
    public Material mm;
    public Material mh;
    public float colorScalar;
    Color lowColor;
    Color midColor;
    Color highColor;

    public LineRenderer line;
    public float circleRadius;
    public float waveScalar;
    public float waveShift;
    AudioSource a;

    public float micStart;

    public int shortRecordSeconds;

    
    private void Start()
    {
        aList.resetSounds();
        micStart = Time.time;
        //set up line renderer in a circle
        line.positionCount = Mathf.FloorToInt(max / step);
        for (int i = 0; i < line.positionCount; i++)
        {
            float theta = ((float)i / (float)line.positionCount) * 2 * Mathf.PI;
            Vector3 xy = new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta));
            xy *= circleRadius;
            //Debug.Log(theta + " " + xy);
            // circle currently centered at origin, if that needs to change do it here
            line.SetPosition(i, xy);
        }

        //Set up audio source
        a = GetComponent<AudioSource>();
        // string micName = Microphone.devices[1];
        string micName = Microphone.devices[0];
        a.clip = Microphone.Start(micName, true, shortRecordSeconds, AudioSettings.outputSampleRate);
        while (!(Microphone.GetPosition(micName) > 0)) { }
        a.Play();
        micStart = Time.time;
        for (int i = 0; i < Microphone.devices.Length; i++)
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!" + i + "  " + Microphone.devices[i].ToString());
        }
        aList.AddSoundMaker(a);
        //Set up box sound cutoffs
        Debug.Log(Microphone.devices);
        midMin = Mathf.FloorToInt(spectrum.Length * midMinF);
        maxMin = Mathf.FloorToInt(spectrum.Length * maxMinF);
        ml.EnableKeyword("_EMISSION");
        mm.EnableKeyword("_EMISSION");
        mh.EnableKeyword("_EMISSION");
    }

    async void Update()
    {
        
        spectrum = new float[128];
        foreach (AudioSource s in aList.GetSoundMakers()) {
            float[] n = new float[128];
            s.GetSpectrumData(n, 0, FFTWindow.Rectangular);
            for (int i = 0; i < 128; i++) spectrum[i] += n[i];
        }
        foreach (AudioSource s in aList.GetLoopingSounds()) {
            float[] n = new float[128];
            s.GetSpectrumData(n, 0, FFTWindow.Rectangular);
            for (int i = 0; i < 128; i++) spectrum[i] += n[i];
        }

        lowSum = 0;
        midSum = 0;
        highSum = 0;
        for (int i = 0; i < spectrum.Length; i++)
        {
            //Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            //Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
            
            if (i < midMin)
            {
                lowSum += spectrum[i];
            }
            else if (i < maxMin)
            {
                midSum += spectrum[i];
            } else
            {
                highSum += spectrum[i];
            }
        }
        lowSum = Mathf.Clamp(lowSum * colorScalar, 0, 1);
        midSum = Mathf.Clamp(midSum * colorScalar, 0, 1);
        highSum = Mathf.Clamp(highSum * colorScalar, 0, 1);
        lowColor = new Color(lowSum, 0, 0);
        midColor = new Color(0, midSum, 0);
        highColor = new Color(0, highSum, highSum);
        ml.color = lowColor;
        ml.SetColor("_EmissionColor", lowColor);
        mm.color = midColor;
        mm.SetColor("_EmissionColor", midColor);
        mh.color = highColor;
        mh.SetColor("_EmissionColor", highColor);
        DynamicGI.UpdateEnvironment();


        float prev = wave(0);
        int index = 0;
        for (float x = step; x < max; x+=step)
        {
            float n = waveScalar * wave(x + waveShift);
            //Debug.DrawLine(new Vector3(x - step, prev, 0), new Vector3(x, n, 0), Color.black);
            Vector3 currLinePos = line.GetPosition(index);
            Vector3 linePos = new Vector3(currLinePos.x, line.transform.position.y + n, currLinePos.z);
            line.SetPosition(index, linePos);
            prev = n;
            index++;
        }
    }
    float wave(float x)
    {
        float sum = 0;
        for (int i = 0; i < spectrum.Length; i++)
        {
            sum += (spectrum[i]) * Mathf.Sin((i + 1) * x);
        }
        return sum;
    }

    public float[] getValues()
    {
        return new float[] { this.highSum, this.midMin, this.lowSum };
    }
}
