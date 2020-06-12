using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat_Calculator : MonoBehaviour
{
    public int bpm = 120;
    public float metro;


    // Start is called before the first frame update
    void Awake()
    {
        metro = 60 / (bpm / 4);
    }

    // Update is called once per frame
    void Update()
    {
        metro = 60 / (bpm / 4);
    }

}
