using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pvr_UnitySDKAPI;

public class Melody_Generator : MonoBehaviour
{
    public int sampleFreq = 48000;
    public float frequency = 440;
    public float pfrequency;
    public GameObject dot2;

    private float[] samples = new float[48000];
    float colorPos;
    public AudioClip ac;
    public AudioSource aud;

    double divisor = 1600 / 14;


    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        colorPos = GameObject.FindWithTag("GameController").GetComponent<Line_Renderer_Aurora>().ColorPos; //hue 0~1
        pfrequency = frequency;
        updatewave();
        aud.volume = 0.2f;

    }

    // Update is called once per frame
    void Update()
    {
        float num = (dot2.transform.position.y+4);

        if (num < 0.643)
        {
            frequency = mtof(48);
        }
        else if (num < 0.643 * 2)
        {
            frequency = mtof(50);
        }
        else if (num < 0.643 * 3)
        {
            frequency = mtof(52);
        }
        else if (num < 0.643 * 4)
        {
            frequency = mtof(53);
        }
        else if (num < 0.643 * 5)
        {
            frequency = mtof(55);
        }
        else if (num < 0.643 * 6)
        {
            frequency = mtof(57);
        }
        else if (num < 0.643 * 7)
        {
            frequency = mtof(59);
        }
        else if (num < 0.643 * 8)
        {
            frequency = mtof(60);
        }
        else if (num < 0.643 * 9)
        {
            frequency = mtof(62);
        }
        else if (num < 0.643 * 10)
        {
            frequency = mtof(64);
        }
        else if (num < 0.643 * 11)
        {
            frequency = mtof(65);
        }
        else if (num < 0.643 * 12)
        {
            frequency = mtof(67);
        }
        else if (num < 0.643 * 13)
        {
            frequency = mtof(69);
        }
        else
        {
            frequency = mtof(71);
        }

        if (Pvr_UnitySDKAPI.Controller.UPvr_GetKey(0, Pvr_KeyCode.TRIGGER) || Input.GetMouseButton(0))
        {

            if (pfrequency != frequency)
            {               
                updatewave();
                pfrequency = frequency;
                aud.Play();

            }

        }
        else if(Pvr_UnitySDKAPI.Controller.UPvr_GetKeyUp(0, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonUp(0))
        {
            aud.Stop();
        }

    }

    public void updatewave()
    {
        

        for (int i = 0; i < samples.Length; i++)
        {

                samples[i] = (Mathf.Repeat(i * frequency / sampleFreq, 1) * colorPos * 2 - colorPos) * 0.7f;
                samples[i] += (Mathf.Repeat(i * frequency * 2 / sampleFreq, 1) * colorPos * 2 - colorPos) * 0.1f;
                samples[i] += (Mathf.Repeat(i * frequency * 3 / sampleFreq, 1) * colorPos * 2 - colorPos) * 0.025f;
                samples[i] += (Mathf.Repeat(i * frequency * 4 / sampleFreq, 1) * colorPos * 2 - colorPos) * 0.07f;
                samples[i] += (Mathf.Repeat(i * frequency * 5 / sampleFreq, 1) * colorPos * 2 - colorPos) * 0.03f;
                samples[i] += (Mathf.Repeat(i * frequency * 8 / sampleFreq, 1) * colorPos * 2 - colorPos) * 0.05f;
                samples[i] += (Mathf.Repeat(i * frequency * 16 / sampleFreq, 1) * colorPos * 2 - colorPos) * 0.025f;

                samples[i] += (Mathf.Repeat(i * frequency / sampleFreq, 1) > 0.5f) ? (1 - colorPos)*0.7f : (-(1 - colorPos))*0.7f;
                samples[i] += (Mathf.Repeat(i * frequency / sampleFreq, 1) > 0.5f) ? (1 - colorPos)*0.1f : (-(1 - colorPos))*0.1f;
                samples[i] += (Mathf.Repeat(i * frequency / sampleFreq, 1) > 0.5f) ? (1 - colorPos)*0.025f : (-(1 - colorPos))*0.025f;
                samples[i] += (Mathf.Repeat(i * frequency / sampleFreq, 1) > 0.5f) ? (1 - colorPos)*0.07f : (-(1 - colorPos))*0.07f;
                samples[i] += (Mathf.Repeat(i * frequency / sampleFreq, 1) > 0.5f) ? (1 - colorPos)*0.03f : (-(1 - colorPos))*0.03f;
                samples[i] += (Mathf.Repeat(i * frequency / sampleFreq, 1) > 0.5f) ? (1 - colorPos)*0.05f : (-(1 - colorPos))*0.05f;
                samples[i] += (Mathf.Repeat(i * frequency / sampleFreq, 1) > 0.5f) ? (1 - colorPos)*0.025f : (-(1 - colorPos))*0.025f;

        }

        ac = AudioClip.Create("sine", samples.Length, 1, sampleFreq, false);
        ac.SetData(samples, 0);
        aud.clip = ac;
    }



    public float mtof(double midi)
    {
        float freq;
        double power = (midi-69) / 12;

        freq = (float)Math.Pow(2, power) * 440;

        return freq;
    }
}
