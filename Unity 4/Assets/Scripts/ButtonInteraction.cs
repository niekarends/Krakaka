using UnityEngine;
using System.Collections;

public class ButtonInteraction : MonoBehaviour {
	public GameObject defaultMenu, ControlMenu;

	public void startGame() {
		Application.LoadLevel ("level1");
		
	}

	public void exitGame() {
		Application.Quit ();
	}

	public void openControls() {
		defaultMenu.SetActive (false);
		ControlMenu.SetActive (true);
	}

	public void shutControls() {
		defaultMenu.SetActive (true);
		ControlMenu.SetActive (false);
	}
}

