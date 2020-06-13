using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat_Calculator : MonoBehaviour
{
    public float bpm = 120;
    public float pbpm;
    public float metro;


    // Start is called before the first frame update
    void Start()
    {
        pbpm = bpm;
        float metro = 60 / (bpm / 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (pbpm != bpm)
        {
            pbpm = bpm;
            float metro = 60 / (bpm / 4);
        }

    }
}
