using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chord_Generator : MonoBehaviour
{
    public int sampleFreq = 48000;
    public GameObject BPM;
    public float metro;

    public int[] chord_progress = new int[4];
    public int currentchord = 0;
    public float[] f1 = new float[8];
    public float[] f2 = new float[8];
    public float[] f3 = new float[8];
    public float[] f4 = new float[8];
    public float[] fbass = new float[8];

    private float[] samples0 = new float[48000];
    private float[] samples1 = new float[48000];
    private float[] samples2 = new float[48000];
    private float[] samples3 = new float[48000];
    private float[] bass = new float[48000];
    public AudioClip[] ac = new AudioClip[5];
    public AudioSource[] aud = new AudioSource[5];
    public GameObject[] audi = new GameObject[5];



    void Start() //set chord midis
    {
        metro = GameObject.FindWithTag("bpm").GetComponent<Beat_Calculator>().metro;

        int mid1 = 48;
        int mid2 = 52;
        int mid3 = 55;
        int mid4 = 59;
        f1[0] = mtof(mid1);
        f2[0] = mtof(mid2);
        f3[0] = mtof(mid3);
        f4[0] = mtof(mid4);
        fbass[0] = mtof(mid1 - 12);

        for (int i = 1; i < f1.Length; i++)
        {
            if (i == 1)
            {
                mid1 += 2;
                mid2 += 1;
                mid3 += 2;
                mid4 += 1;
            }
            else if (i == 3)
            {
                mid1 += 1;
                mid2 += 2;
                mid3 += 1;
                mid4 += 2;
            }
            else if (i == 4)
            {
                mid1 += 2;
                mid2 += 2;
                mid3 += 2;
                mid4 += 1;
            }
            else if (i == 5)
            {
                mid1 += 2;
                mid2 += 1;
                mid3 += 2;
                mid4 += 2;
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
                mid1 += 1;
                mid2 += 2;
                mid3 += 2;
                mid4 += 2;
            }
            else
            {
                mid1 += 2;
                mid2 += 2;
                mid3 += 2;
                mid4 += 2;
            }
            f1[i] = mtof(mid1);
            f2[i] = mtof(mid2);
            f3[i] = mtof(mid3);
            f4[i] = mtof(mid4);
            fbass[i] = mtof(mid1 - 12);
        }

        for (int i = 0; i < 5; i++)
        {
            aud[i] = audi[i].GetComponent<AudioSource>();
            aud[i].volume = 0.2f;
        }




        InvokeRepeating("clock", 0f, metro);
        InvokeRepeating("updatewave", 0f, metro);
        InvokeRepeating("bassPlay", 0f, metro/2);
        InvokeRepeating("volumeup", 0f, metro);
        InvokeRepeating("volumedown", metro / 2, metro);
    }

    // Update is called once per frame
    void Update()
    {
        metro = GameObject.FindWithTag("bpm").GetComponent<Beat_Calculator>().metro;

        if ((Time.time % metro < metro / 4 && Time.time % metro >= 0) ||
            (Time.time % metro < metro / 4 * 3 && Time.time % metro >= metro / 4 * 2))
        {
            volumeup();
        }
        else
        {
            //Debug.Log("here");
            volumedown();
        }

    }

    void volumeup()
    {
        for (int i = 0; i < 4; i++)
        {
            aud[i].volume += 0.0015f;
        }
    }
    void volumedown()
    {
        for (int i = 0; i < 4; i++)
        {
            aud[i].volume -= 0.003f;
        }
    }



    void clock()
    {
        currentchord += 1;

    }


    public void updatewave()
    {
        for (int i = 0; i < samples0.Length; i++)
        {
            samples0[i] = Mathf.Sin(Mathf.PI * 2 * i * f1[chord_progress[currentchord % 4] - 1] / sampleFreq) * 0.8f;
            samples1[i] = Mathf.Sin(Mathf.PI * 2 * i * f2[chord_progress[currentchord % 4] - 1] / sampleFreq) * 0.8f;
            samples2[i] = Mathf.Sin(Mathf.PI * 2 * i * f3[chord_progress[currentchord % 4] - 1] / sampleFreq) * 0.8f;
            samples3[i] = Mathf.Sin(Mathf.PI * 2 * i * f4[chord_progress[currentchord % 4] - 1] / sampleFreq) * 0.8f;
            samples0[i] += (Mathf.Repeat(i * f1[chord_progress[currentchord % 4] - 1] / sampleFreq, 1) > 0.5f) ? 0.15f : -0.2f;
            samples1[i] += (Mathf.Repeat(i * f2[chord_progress[currentchord % 4] - 1] / sampleFreq, 1) > 0.5f) ? 0.15f : -0.2f;
            samples2[i] += (Mathf.Repeat(i * f3[chord_progress[currentchord % 4] - 1] / sampleFreq, 1) > 0.5f) ? 0.15f : -0.2f;
            samples3[i] += (Mathf.Repeat(i * f4[chord_progress[currentchord % 4] - 1] / sampleFreq, 1) > 0.5f) ? 0.15f : -0.2f;
            bass[i] = Mathf.Sin(Mathf.PI * 2 * i * fbass[chord_progress[currentchord % 4] - 1] / sampleFreq);
        }

        for (int i = 0; i < 5; i++)
        {
            ac[i] = AudioClip.Create("sine" + i, samples0.Length, 1, sampleFreq, false);
        }
        ac[0].SetData(samples0, 0);
        ac[1].SetData(samples1, 0);
        ac[2].SetData(samples2, 0);
        ac[3].SetData(samples3, 0);
        ac[4].SetData(bass, 0);
        for (int i = 0; i < 4; i++)
        {
            aud[i].clip = ac[i];
            aud[i].volume = 0.15f;
            aud[i].Play();

        }

    }

    void bassPlay(){
        aud[4].clip = ac[4];
        aud[4].volume = 1f;
        aud[4].Play();
        
    }


    public float mtof(double midi)
    {
        float freq;
        double power = (midi - 69) / 12;

        freq = (float)Math.Pow(2, power) * 440;

        return freq;
    }


}
