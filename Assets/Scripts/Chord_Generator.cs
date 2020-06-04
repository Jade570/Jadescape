﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chord_Generator : MonoBehaviour
{
    public int sampleFreq = 48000;
    public int bpm = 120;
    public float metro;

    public int[] chord_progress = new int[4];
    public int currentchord = 0;
    public float[] f1 = new float[8];
    public float[] f2 = new float[8];
    public float[] f3 = new float[8];
    public float[] f4 = new float[8];

    private float[] samples0 = new float[48000];
    private float[] samples1 = new float[48000];
    private float[] samples2 = new float[48000];
    private float[] samples3 = new float[48000];
    public AudioClip[] ac = new AudioClip[4];
    public AudioSource[] aud = new AudioSource[4];
    public GameObject[] audi = new GameObject[4];



    void Start() //set chord midis
    {
        metro = bpmtometro(bpm);

        int mid1 = 48;
        int mid2 = 52;
        int mid3 = 55;
        int mid4 = 59;
        f1[0] = mtof(mid1);
        f2[0] = mtof(mid2);
        f3[0] = mtof(mid3);
        f4[0] = mtof(mid4);

        for (int i = 1; i<f1.Length; i++)
        {
               if (i == 1)
            {
                mid1 += 2;
                mid2 += 1;
                mid3 += 2;
                mid4 += 1;
            }
            else if ( i == 3)
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
        }
        InvokeRepeating("clock", 0f, metro);
        InvokeRepeating("updatewave", 0f, metro);
    }

    // Update is called once per frame
    void Update()
    {
        

           
  

    }

    void clock()
    {
        currentchord += 1;
    }


    public void updatewave()
    {
        for (int i = 0; i< 4; i++)
        {
            aud[i] = audi[i].GetComponent<AudioSource>();
        }

        for (int i = 0; i < samples0.Length; i++)
        {
            samples0[i] = Mathf.Sin(Mathf.PI * 2 * i * f1[chord_progress[currentchord % 4]-1] / sampleFreq);
            samples1[i] = Mathf.Sin(Mathf.PI * 2 * i * f2[chord_progress[currentchord % 4]-1] / sampleFreq);
            samples2[i] = Mathf.Sin(Mathf.PI * 2 * i * f3[chord_progress[currentchord % 4]-1] / sampleFreq);
            samples3[i] = Mathf.Sin(Mathf.PI * 2 * i * f4[chord_progress[currentchord % 4]-1] / sampleFreq);
        }

        for (int i = 0; i<4; i++)
        {
            ac[i] = AudioClip.Create("sine"+i, samples0.Length, 1, sampleFreq, false);
        }
        ac[0].SetData(samples0, 0);
        ac[1].SetData(samples1, 0);
        ac[2].SetData(samples2, 0);
        ac[3].SetData(samples3, 0);
        for (int i = 0; i < 4; i++)
        {
            aud[i].clip = ac[i];
            aud[i].volume = 0.3f;
            aud[i].Play();
            //aud[0].PlayOneShot(ac[i],0.3f);
        }
        //aud[0].PlayOneShot(ac[0], 0.3f);
        //aud[0].PlayOneShot(ac[1], 0.3f);
        //aud[0].PlayOneShot(ac[2], 0.3f);
        //aud[0].PlayOneShot(ac[3], 0.3f);

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
        float metro = 60/(bpm/4);
        return metro;
    }


}
