using UnityEngine;
using System.Collections;

public class ButtonInteraction : MonoBehaviour {
	public void startGame() {
		Application.LoadLevel ("level1");
		
	}

	public void exitGame() {
		Application.Quit ();
	}
}

