﻿using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update

    int tutorial_count = 0;

    string object_name;

    public GameObject[] tutorial_image = new GameObject[3];

    void Start()
    {
        tutorial_image[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER) || Input.GetMouseButtonDown(0)))
        {
            tutorial_image[tutorial_count].SetActive(false);
            if (tutorial_count < 7)
            {
                tutorial_count++;
                tutorial_image[tutorial_count].SetActive(true);
            }
        }

        if (Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.APP) || Input.GetMouseButtonDown(1))
        {
            tutorial_image[tutorial_count].SetActive(false);
            tutorial_count = 7;
        }
    }

}