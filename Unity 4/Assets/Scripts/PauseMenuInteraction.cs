using UnityEngine;
using System.Collections;

public class PauseMenuInteraction : MonoBehaviour {
	public GameController gameController;

	public void exitToMainMenu() {
		Application.LoadLevel ("MainMenu");
	}

	public void resumeGame() {
		gameController.resumeGame ();
	}

}
