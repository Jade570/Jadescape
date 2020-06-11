/*
-----------------------------------------------------
            TAF's Planet Creator
           Copyright © 2018 - TAF
           
  https://assetstore.unity.com/publishers/6837
    https://www.facebook.com/TobisAssetForge
-----------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Description:
 	Rotates the object around the rotationDirection.
*/

public class TAFRotateObject : MonoBehaviour {

	[SerializeField]
	private Vector3 rotationDirection = new Vector3(0,0,0);

	//Sets the rotation direction to a given value.
	public void setRotationDirection(Vector3 rotation) {
		rotationDirection = rotation;
	}

	//Returns the current rotation direction.
	public Vector3 getRotationDirection() {
		return rotationDirection;
	}

	//Rotates the object.
	void FixedUpdate () {
		transform.Rotate(rotationDirection * Time.deltaTime);
	}
}