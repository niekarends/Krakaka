using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	public int speed;
	void Update () {
		transform.Rotate (new Vector3 (0, 0, 15) * Time.deltaTime * speed);
	}
}
