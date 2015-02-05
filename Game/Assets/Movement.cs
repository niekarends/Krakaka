using UnityEngine;
using System.Collections;


public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			transform.position += new Vector3(0, 3, 0) * Time.deltaTime;
		} 
	}
}
