using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPlay : MonoBehaviour
{
    public AudioSource aud;
    public bool clickTrigger;
    public bool clickToggle;
    public float scale;
    public Transform origScale;
    public float time;
    public float metro;
    public int counter;


    // Start is called before the first frame update
    void Start()
    {
        metro = GameObject.FindWithTag("bpm").GetComponent<Beat_Calculator>().metro;
        aud = this.gameObject.GetComponent<AudioSource>();
        aud.volume = 0.8f;
        origScale = this.gameObject.GetComponent<Transform>();
        scale = origScale.localScale.x / 500f;
        clickTrigger = false;
        clickToggle = false;
        time = 0;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        metro = GameObject.FindWithTag("bpm").GetComponent<Beat_Calculator>().metro;

        if (counter == 1)
        {
            aud.Play();
        }



        if (clickToggle == false)
        {
            if (clickTrigger == true)
            {
                clickToggle = true;

                time = Time.time;
                counter = 0;
                clickTrigger = false;
            }

            else
            {
                aud.Stop();
            }
        }
        else
        {
            if (clickTrigger == true)
            {
                clickToggle = false;

                time = 0;
                counter = 0;
                clickTrigger = false;
            }
            else
            {
                if ((Time.time - time )% metro < metro/2)
                {
                    bigger();
                }
                else
                {
                    smaller();
                }
            }

        }
        
    }

    void bigger()
    {
        origScale.localScale += new Vector3(scale, scale, scale);
        counter += 1;
    }

    void smaller()
    {
        origScale.localScale -= new Vector3(scale, scale, scale);
        counter = 0;
    }

}
