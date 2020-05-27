using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class SoundMaker : MonoBehaviour {

    public LibPdInstance pdPatch;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (Pvr_UnitySDKAPI.Controller.UPvr_GetKey (0, Pvr_KeyCode.TRIGGER)) {
            pdPatch.SendFloat ("Playing", 1);
            pdPatch.SendFloat ("mouseY", (int) ((17) / 2.43));
        } else if (Input.GetMouseButton (0)) {
            pdPatch.SendFloat ("Playing", 1);
            pdPatch.SendFloat ("mouseY", (int) (( 17) / 2.43));
        } else {
            // 입력이 없을 때는 퓨어데이터모듈에 데이터0을 보냄
            pdPatch.SendFloat ("Playing", 0);
        }
    }

}