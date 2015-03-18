using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
	public GameObject exitButton, resumeButton;
    public Vector3 spawnValues;
    public float[] spawnXValues;
    public int hazardCount;
	public float spawnWait, startWait, waveWait, kmh, fuel = 1000;
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
		if (Input.GetButton("Cancel"))
		{
			if(Time.timeScale == 0) {
				resumeGame();
			}
			else {
				pauseGame();
			}
		}

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
            guiElements[2].text = "Fuel: " + (int)fuel;
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
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(spawnXValues[Random.Range(0, spawnXValues.Length)], spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazards[Random.Range(0, hazards.Length)], spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (isGameOver)
            {
                break;
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

    public void addFuel()
    {
        if (!isGameOver)
        {
            fuel += PointsPerPickup;
        }
    }
}
