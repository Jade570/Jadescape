using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSC_sender : MonoBehaviour
{

    public OSC osc;

    // Start is called before the first frame update
    void Start()
    {

        OscMessage message0 = new OscMessage();
        message0.address = "/toggle";
        int toggle = 1;
        message0.values.Add(toggle);
        osc.Send(message0);


    }

    // Update is called once per frame
    void Update()
    {

        OscMessage message = new OscMessage();
        message.address = "/demo";
        int demo = 1;
        message.values.Add(demo);
        osc.Send(message);

    }
}
