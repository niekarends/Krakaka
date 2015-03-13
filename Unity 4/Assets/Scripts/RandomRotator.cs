using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {
    public float tumble = 5;
	// Use this for initialization
	void Start () {
        rigidbody.angularVelocity = Random.insideUnitSphere * 5;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
