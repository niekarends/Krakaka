using UnityEngine;
using System.Collections;

public class ExplosionBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {		
		GetComponent<Animation>().Play("Default Take");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
