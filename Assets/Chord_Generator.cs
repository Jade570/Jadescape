using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chord_Generator : MonoBehaviour
{
    public int sampleFreq = 48000;
    public int bpm = 120;
    public float metro;

    public int[] chord_progress = new int[4];
    public float[] f1 = new float[7];
    public float[] f2 = new float[7];
    public float[] f3 = new float[7];
    public float[] f4 = new float[7];

    private float[] samples = new float[48000];
    public AudioClip ac;
    public AudioSource aud;


    double divisor = Screen.height / 14;


    void Start() //set chord midis
    {
        int mid1 = 48;
        int mid2 = 52;
        int mid3 = 55;
        int mid4 = 59;
        for (int i = 1; i<=f1.Length; i++)
        { 
            if (i == 4)
            {
                mid1 += 1;
                mid2 += 2;
                mid3 += 1;
                mid4 += 2;
            }
            else if (i == 2 )
            {
                mid1 += 2;
                mid2 += 1;
                mid3 += 2;
                mid4 += 1;
            }
            else if (i == 5)
            {
                mid1 += 2;
                mid2 += 2;
                mid3 += 2;
                mid4 += 1;
            }
            else if (i == 6)
            {
                mid1 += 2;
                mid2 += 1;
                mid3 += 2;
                mid4 += 2;
            }
            else if (i == 7)
            {
                mid1 += 2;
                mid2 += 2;
                mid3 += 1;
                mid4 += 2;
            }
            else
            {
                mid1 += 2;
                mid2 += 2;
                mid3 += 2;
                mid4 += 2;
            }
            f1[i - 1] = mtof(mid1);
            f2[i - 1] = mtof(mid2);
            f3[i - 1] = mtof(mid3);
            f4[i - 1] = mtof(mid4);
        }

        updatewave();
        aud.Play();
    }

    // Update is called once per frame
    void Update()
    {




    }

    public void updatewave()
    {
        aud = GetComponent<AudioSource>();
        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = Mathf.Sin(Mathf.PI * 2 * i * f1[0] / sampleFreq);
            samples[i] += Mathf.Sin(Mathf.PI * 2 * i * f2[0] / sampleFreq);
            //samples[i] += Mathf.Sin(Mathf.PI * 2 * i * f3[0] / sampleFreq);
            //samples[i] += Mathf.Sin(Mathf.PI * 2 * i * f4[0] / sampleFreq);
        }
        ac = AudioClip.Create("sine", samples.Length, 1, sampleFreq, false);
        ac.SetData(samples, 0);
        aud.clip = ac;
    }



    public float mtof(double midi)
    {
        float freq;
        double power = (midi - 69) / 12;

        freq = (float)Math.Pow(2, power) * 440;

        return freq;
    }

    public float bpmtometro(float bpm)
    {
        float metro = 60/bpm;
        return metro;
    }


}
