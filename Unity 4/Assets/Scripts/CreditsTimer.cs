using UnityEngine;
using System.Collections;

public class CreditsTimer : MonoBehaviour {
	public float duration = 2;
	// Use this for initialization
	void Start () {
		StartCoroutine (loadMainMenu ());
	}

	public IEnumerator loadMainMenu() {
		yield return new WaitForSeconds (duration);
		Application.LoadLevel ("MainMenu");

	}
	// Update is called once per frame
	void Update () {
	
	}
}
