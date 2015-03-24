using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
    public Slider healthSlider;
	private GameController gameController;

	public void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		} else {
			Debug.Log ("Cant find gamecontroller");
		}

        healthSlider.maxValue = gameController.getMaxFuel();
	}

	public void Update() {
        healthSlider.value = gameController.getFuel();
	}

}
