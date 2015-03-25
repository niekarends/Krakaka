using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
	public GameObject exitButton, resumeButton, highscoretext;
    public Vector3 spawnValues;
    public float[] spawnXValues;
    public int hazardCount;
	public float spawnWait, startWait, waveWait, kmh, fuel = 1000, maxfuel;
    public bool isGameOver;

    public Text[] guiElements;
    private float timeDriven, mps;
    public int PointsPerPickup;
	private int gameDifficulty;


    void Start()
    {
		gameDifficulty = PlayerPrefs.GetInt("Difficulty");
        mps = (kmh * 1000) / 3600;
        StartCoroutine(spawnWaves());
		Time.timeScale =1;
        
    }
	void FixedUpdate(){
        if (!isGameOver) { 
            fuel -= 0.25f;
        }
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
			if((timeDriven * mps) > PlayerPrefs.GetFloat("highscore")) {
				highscoretext.SetActive(true);
				PlayerPrefs.SetFloat("highscore", (timeDriven * mps));
			}
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
			spawnWaveType3();
			yield return new WaitForSeconds (2);

			spawnWaveType1(PlayerPrefs.GetInt("Difficulty"));
			yield return new WaitForSeconds(2);

			StartCoroutine( spawnWaveType2());
				
            yield return new WaitForSeconds(7);

			spawnWaveType1(PlayerPrefs.GetInt("Difficulty"));
			yield return new WaitForSeconds(2);



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
				Instantiate(hazards[Random.Range(0, hazards.Length-2)], spawnPosition, spawnRotation);
			}
		}
	}

	IEnumerator spawnWaveType2() {
		int spawnlane = Random.Range (0, spawnXValues.Length);
		Vector3 spawnPosition = new Vector3(spawnXValues[spawnlane], spawnValues.y, spawnValues.z);
		Vector3 spawnPositionPickup = new Vector3(spawnXValues[spawnlane], 3.8f, spawnValues.z);	
		Quaternion spawnRotation = Quaternion.identity;
		//Instantiate(hazards[3], spawnPosition, spawnRotation);
		yield return new WaitForSeconds (2);
		Instantiate(hazards[0], spawnPosition, spawnRotation);
		yield return new WaitForSeconds (1.75f);
		Instantiate(hazards[1], spawnPosition, spawnRotation);

		if (PlayerPrefs.GetInt ("Difficulty") == 3) {
			Instantiate (hazards [2], spawnPositionPickup, spawnRotation);
		} else if (PlayerPrefs.GetInt ("Difficulty") == 2){
			yield return new WaitForSeconds (1.5f);
			Instantiate (hazards [2], spawnPosition, spawnRotation);
		}
	}

	void spawnWaveType3() {
		int spawnlane = Random.Range (0, spawnXValues.Length);
		Vector3 spawnPosition = new Vector3(spawnXValues[spawnlane], spawnValues.y, spawnValues.z);
		Quaternion spawnRotation = Quaternion.identity;

		Instantiate(hazards[3], spawnPosition, spawnRotation);
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
