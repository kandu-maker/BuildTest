using UnityEngine;
using System.Collections;

public class RotateCube : MonoBehaviour {

	float rot = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (0f,rot,0f);
	}
}
