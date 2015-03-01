﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public bool isGameOver;
	public GUIText gameoverText;
	
	void Start() {
		StartCoroutine(spawnWaves ());
	}
	
	void Update(){
		if (isGameOver) {
			
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
	
	IEnumerator spawnWaves() {
		yield return new WaitForSeconds (startWait);
		while(true){
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (- spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
			if (isGameOver){
				gameoverText.text = "Press R for restart";
				break;
			}
		}
		
	}

	public void gameOver() {
		isGameOver = true;
	}
}