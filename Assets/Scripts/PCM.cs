using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PCM : MonoBehaviour
{
    public int sampleFreq = 44100;
    public float frequency = 440;

    private float[] samples = new float[44100];
    public AudioClip ac;
    public AudioSource aud;

    double divisor = Screen.height / 14;

    // Start is called before the first frame update
    void Start()
    {
        aud.Play();
    }

    // Update is called once per frame
    void Update()
    {
        double num = Math.Round(Input.mousePosition.y / divisor);
        switch (num)
        {
            case 0:
                frequency = mtof(48);
                break;

            case 1:
                frequency = mtof(50);
                break;

            case 2:
                frequency = mtof(52);
                break;

            case 3:
                frequency = mtof(53);
                break;
            case 4:
                frequency = mtof(55);
                break;
            case 5:
                frequency = mtof(57);
                break;
            case 6:
                frequency = mtof(59);
                break;
            case 7:
                frequency = mtof(60);
                break;
            case 8:
                frequency = mtof(62);
                break;
            case 9:
                frequency = mtof(64);
                break;
            case 10:
                frequency = mtof(65);
                break;
            case 11:
                frequency = mtof(67);
                break;
            case 12:
                frequency = mtof(69);
                break;
            case 13:
                frequency = mtof(71);
                break;
        }

       //if (Input.GetMouseButtonDown(0))
        //{
            updatewave();
            
        //}
    }

    public void updatewave()
    {
        aud = GetComponent<AudioSource>();
        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = Mathf.Sin(Mathf.PI * 2 * i * frequency / sampleFreq);
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
