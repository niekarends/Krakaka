﻿using UnityEngine;
using System.Collections;

public class FixedRotator : MonoBehaviour {
    public Vector3 tumble;
	// Use this for initialization
	void Update () {
		GetComponent<Rigidbody>().angularVelocity = tumble;
	}
}
