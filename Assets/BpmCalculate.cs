using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BpmCalculate : MonoBehaviour
{

    public int bpm = 120;
    private int pbpm;
    public float metro;


    // Start is called before the first frame update
    void Awake()
    {
        pbpm = bpm;
        metro = 60 / (bpm / 4);
    }

    private void Update()
    {
        if (pbpm != bpm)
        {
            metro = 60 / (bpm / 4);
            pbpm = bpm;
        }
    }

}
