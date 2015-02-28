using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;


	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		} else {
			Debug.Log ("Cant find gamecontroller");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag != "Boundary") {
			Destroy (other.gameObject);
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
			gameController.addScore(scoreValue);

		}
		if (other.tag == "Player") {
			Instantiate (explosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}
	
	}
}
