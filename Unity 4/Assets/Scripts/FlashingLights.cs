using UnityEngine;
using System.Collections;

public class FlashingLights : MonoBehaviour {
	public Light redlight,bluelight;
	public float brightness, frequency;


	public void Start() {
		StartCoroutine (flashLights ());
	}

	public IEnumerator flashLights(){
		while (true) {
			for(int i = 0; i < 3; i++) {
				redlight.range = 0;
				yield return new WaitForSeconds(0.025f);
				redlight.range = brightness;
				yield return new WaitForSeconds(0.025f);
			}
			redlight.range = 0;
			yield return new WaitForSeconds(0.05f);
			for(int i = 0; i < 3; i++) {
				bluelight.range = 0;
				yield return new WaitForSeconds(0.025f);
				bluelight.range = brightness;
				yield return new WaitForSeconds(0.025f);
			}
			bluelight.range = 0;
			yield return new WaitForSeconds(frequency);
		}
	}
}
