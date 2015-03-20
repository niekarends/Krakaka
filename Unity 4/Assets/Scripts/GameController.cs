using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
	public GameObject exitButton, resumeButton;
    public Vector3 spawnValues;
    public float[] spawnXValues;
    public int hazardCount;
	public float spawnWait, startWait, waveWait, kmh, fuel = 1000, maxfuel;
    public bool isGameOver;

    public Text[] guiElements;
    private float timeDriven, mps;
    public int PointsPerPickup;


    void Start()
    {
        mps = (kmh * 1000) / 3600;
        StartCoroutine(spawnWaves());
		Time.timeScale =1;
        
    }
	void FixedUpdate(){
            fuel -= 0.25f;
	}
    void Update()
    {
		if (Input.GetButtonDown("Cancel"))
		{
			if(Time.timeScale == 0) {
				resumeGame();
			}
			else {
				pauseGame();
			}
		}
		//Makes the low fuel indicator blink
        if (fuel < 100)
        {
            if (fuel % 5 < 3)
            {
                guiElements[0].text = "";
            }
            else
            {
                guiElements[0].text = "Fuel low!";
            }
            
        }
        else
        {

            guiElements[0].text = "";
        }

        if (isGameOver)
        {

            guiElements[1].text = "Press R for restart";
            guiElements[0].text = "FINAL SCORE:" + (int)(timeDriven * mps);

            if (Input.GetButton("Fire1"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }

        }
        else
        {
            timeDriven += Time.deltaTime;
            guiElements[1].text = "Distance: " + (int)(timeDriven * mps);
        }
    }

	public void resumeGame(){
		Time.timeScale =1;
		exitButton.SetActive(false);
		resumeButton.SetActive(false);
	}

	public void pauseGame(){
		Time.timeScale =0;
		exitButton.SetActive(true);
		resumeButton.SetActive(true);	
	}

    IEnumerator spawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
			spawnWaveType1(1);

            yield return new WaitForSeconds(waveWait);
            if (isGameOver)
            {
                break;
            }
        }

    }

	void spawnWaveType1(int amountOfCars) {
		if (amountOfCars > spawnXValues.Length) {
			amountOfCars = spawnXValues.Length;
		} 
		for(int i = 0; i < spawnXValues.Length; i++) {
			float spawnchance = Random.Range (0,100);
			if((100.0f / spawnXValues.Length * amountOfCars) > spawnchance) {
				Vector3 spawnPosition = new Vector3(spawnXValues[i], spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazards[Random.Range(0, hazards.Length-1)], spawnPosition, spawnRotation);
			}
		}
	}

    public void gameOver()
    {
        isGameOver = true;
    }

    public float getFuel()
    {
        return fuel;
    }

	public float getMaxFuel() 
	{
		return maxfuel;
	}

    public void addFuel()
    {
        if (!isGameOver)
        {
            fuel += PointsPerPickup;
			//Make sure you dont go over maximum fuel
			if(fuel > maxfuel) {
				fuel = maxfuel;
			}
        }
    }
}
