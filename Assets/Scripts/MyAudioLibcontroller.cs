using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAudioLibcontroller : MonoBehaviour
{
    public Hv_heavy_AudioLib script;
    // Start is called before the first frame update
    void Start()
    {
        script = GetComponent<Hv_heavy_AudioLib>();
        script.SendFloatToReceiver("mouseY",  (float)(Screen.height/154.29));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
