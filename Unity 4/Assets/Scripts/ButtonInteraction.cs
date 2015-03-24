using UnityEngine;
using System.Collections;

public class ButtonInteraction : MonoBehaviour {
	public GameObject defaultMenu, controlMenu, difficultyMenu;

	public void startGame() {

		difficultyMenu.SetActive (!difficultyMenu.activeSelf);
		
	}

	public void exitGame() {
		Application.Quit ();
	}

	public void openControls() {
		defaultMenu.SetActive (false);
		controlMenu.SetActive (true);
		difficultyMenu.SetActive (false);
	}

	public void shutControls() {
		defaultMenu.SetActive (true);
		controlMenu.SetActive (false);
	}

	public void easyButtonPressed() {
		PlayerPrefs.SetInt ("Difficulty", 2);
		startLevel ();
	}

	public void hardButtonPressed() {
		PlayerPrefs.SetInt ("Difficulty", 3);
		startLevel ();
	}

	private void startLevel() {
		Application.LoadLevel("level1");
	}
}

