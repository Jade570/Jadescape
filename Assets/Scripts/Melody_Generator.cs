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

    float divisor = Screen.height / 14;


    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        colorPos = GameObject.FindWithTag("GameController").GetComponent<Line_Renderer_Aurora>().ColorPos; //hue 0~1
        pfrequency = frequency;
        updatewave();
        aud.volume = 0.8f;

    }

    // Update is called once per frame
    void Update()
    {


        float num = (Pvr_UnitySDKAPI.Controller.UPvr_GetControllerState(0) == Pvr_UnitySDKAPI.ControllerState.Connected) ? (dot2.transform.position.y+4) : Mathf.Round(Input.mousePosition.y);

        if (Pvr_UnitySDKAPI.Controller.UPvr_GetControllerState(0) == Pvr_UnitySDKAPI.ControllerState.Connected) //vr
        {
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
        }

        else //pc
        {
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
                default:
                    frequency = mtof(72);
                    break;
               

            }

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
            samples[i] = Mathf.Sin(Mathf.PI * 2 * i * frequency / sampleFreq) * 0.2f;
            samples[i] += Mathf.Sin(Mathf.PI * 4 * i * frequency / sampleFreq) * 0.2f * colorPos;
            samples[i] += Mathf.Sin(Mathf.PI * 7 * i * frequency / sampleFreq) * 0.2f * (1-colorPos);
            samples[i] += Mathf.Sin(Mathf.PI * 3 * i * frequency / sampleFreq) * 0.2f * colorPos;
            samples[i] += Mathf.Sin(Mathf.PI * 5 * i * frequency / sampleFreq) * 0.2f * (1-colorPos);


            /*
            samples[i] += (Mathf.Repeat(i * frequency / sampleFreq, 1) * colorPos * 2 - colorPos) * 0.4f;
            samples[i] += (Mathf.Repeat(i * frequency / sampleFreq, 1) > 0.5f) ? (1 - colorPos)*0.7f : (-(1 - colorPos))*0.4f;
            */
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
