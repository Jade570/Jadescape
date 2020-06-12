using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pvr_UnitySDKAPI;

public class objectAudio : MonoBehaviour
{
    //GameObject objectgroup = gameObject;
    public Collider coll;
    public AudioSource aud;
    public Camera cam;
    private bool clickToggle;

    public GameObject parentobject;
    public Collider[] asteroids = new Collider [19];

    public GameObject BPM;
    public float metro;

    Ray ray = new Ray();

    public Transform controller0Position; //Assign to 'Pvr_UnitySDK/PvrController0' in inspector.



    void Start()
    {
        coll = this.GetComponent<Collider>();
        aud = this.GetComponent<AudioSource>();
        metro = GameObject.FindWithTag("bpm").GetComponent<BpmCalculate>().metro;
        clickToggle = false;

        
        for (int i = 0; i < parentobject.transform.childCount; i++)
        {
            asteroids[i] = parentobject.transform.GetChild(i).gameObject.GetComponent<Collider>();
        }
        
    }

    void Update()
    {
        metro = GameObject.FindWithTag("bpm").GetComponent<BpmCalculate>().metro;


        if (RaycastFromHandInput())
        {
            PerformRaycast(controller0Position);
        }


    }

    void LoopControl(AudioSource aud)
    {
        if (clickToggle == false)
        {
            aud.Stop();
        }
        else {
            aud.Play();
        }
    }

    private void PerformRaycast(Transform rayStart)
    {
        ray.direction = rayStart.forward;
        ray.origin = rayStart.position;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray); //This can be given a maximum range, if needed.

        if (hits.Length > 0)
        {
            //Debug.Log("x: " + controller0Position.eulerAngles.x + "y: " + controller0Position.eulerAngles.y + "z: " + controller0Position.eulerAngles.z);
            Debug.Log("raycast activated");
        }


        for (int i = 0; i<hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Collider collid = hit.transform.GetComponent<Collider>();
            AudioSource aud = hit.transform.GetComponent<AudioSource>();
            if (collid)
            {
                //InvokeRepeating("LoopControl", Time.time, metro);
                
            }
        }
    }
 
    private bool RaycastFromHandInput()
    {
        if (Pvr_UnitySDKAPI.Controller.UPvr_GetControllerState(0) != ControllerState.Connected) return false;
        return Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER);   
    }

}