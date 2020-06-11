using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectAudio : MonoBehaviour
{
    public Collider coll;
    public Camera cam;

    void Start()
    {
        coll = GetComponent<Collider>();
    }

    void Update()
    {
        // Move this object to the position clicked by the mouse.
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (coll.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("hi"); 
            }
        }
    }
}
