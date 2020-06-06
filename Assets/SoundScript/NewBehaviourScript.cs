using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource tam;
    void Start (){
        tam = GetComponent<AudioSource> ();
    }
    
    void OnMouseDown(){
        Debug.Log("i ckicked");
        
        tam.Play ();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
            {}
    }
}
