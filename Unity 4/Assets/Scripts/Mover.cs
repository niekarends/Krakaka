﻿using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Update () {
		rigidbody.velocity = Vector3.forward * speed;
	}
}