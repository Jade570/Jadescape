using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pvr_UnitySDKAPI;

public class picomelodysample : MonoBehaviour
{
    public int sampleFreq = 48000;
    public float frequency = 440;

    private float[] samples = new float[48000];
    public AudioClip ac;
    public AudioSource aud;


    // Start is called before the first frame update
    void Start()
    {
        updatewave();
        //aud.Play();
    }

    // Update is called once per frame
    void Update()
    {

        if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0))
        {
            //updatewave();
            aud.Play();
        }
        else if (Pvr_UnitySDKAPI.Controller.UPvr_GetKeyUp(0, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonUp(0))
        {
            aud.Stop();
        }
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
}
