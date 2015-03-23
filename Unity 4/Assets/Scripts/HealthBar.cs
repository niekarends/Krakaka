using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public GameObject healthBar;
	private GameController gameController;
	private float xNormal, xEmpty = 245.0f;

	public void Start() {
		xNormal = healthBar.transform.position.x;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		} else {
			Debug.Log ("Cant find gamecontroller");
		}
	}

	public void Update() {
		float percentage = (100.0f / gameController.getMaxFuel()) * gameController.getFuel();
		//calc where healthbar has to be
		float xPosition = Mathf.Abs((xEmpty - xNormal)) * (percentage / 100.0f);
		healthBar.transform.position = new Vector3(xPosition + xEmpty, healthBar.transform.position.y, healthBar.transform.position.z);
	}

}
