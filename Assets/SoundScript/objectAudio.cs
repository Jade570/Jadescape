using System.Collections;
using System.Collections.Generic;
using Pvr_UnitySDKAPI;
using UnityEngine;

public class objectAudio : MonoBehaviour {
    public Camera cam;

    public GameObject parentobject;
    public float metro;

    public bool clickTrigger;

    public Transform controller0Position; //Assign to 'Pvr_UnitySDK/PvrController0' in inspector.

    void Start () {

        metro = GameObject.FindWithTag ("bpm").GetComponent<Beat_Calculator> ().metro;
    }

    void Update () {
        metro = GameObject.FindWithTag ("bpm").GetComponent<Beat_Calculator> ().metro;

        //vr
        if (Controller.UPvr_GetKeyDown (0, Pvr_KeyCode.APP)) {

            Ray ray1 = new Ray ();
            ray1.direction = controller0Position.transform.TransformDirection(0,-0.33f,1);
            ray1.origin = controller0Position.position;

            RaycastHit[] hits;
            hits = Physics.RaycastAll (ray1);

          
            if (hits.Length > 0) {
                Debug.Log (hits[0].transform.name);
                GameObject.FindWithTag(hits[0].transform.name).GetComponent<objectPlay>().clickTrigger = true;
            }


            
        }

        //pc
        else if (Input.GetMouseButtonDown(0))
        {

            Ray ray1 = new Ray();
            ray1.direction = controller0Position.transform.TransformDirection(0, 0, 1);
            ray1.origin = controller0Position.position;

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray1);


            if (hits.Length > 0)
            {
                Debug.Log(hits[0].transform.name);
                GameObject.FindWithTag(hits[0].transform.name).GetComponent<objectPlay>().clickTrigger = true;
            }
        }
    } 
}