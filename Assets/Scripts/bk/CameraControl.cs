using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public GameObject puck;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - puck.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = puck.transform.position + offset;
	}
}
