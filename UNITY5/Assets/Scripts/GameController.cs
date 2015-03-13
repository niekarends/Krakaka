using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public float[] spawnXValues;
    public int hazardCount;
    public float spawnWait, startWait, waveWait, kmh;
    public bool isGameOver;

    public Text[] guiElements;
    private float timeDriven, fuel, mps;
    public int score;

    void Start()
    {
        mps = (kmh * 1000) / 3600;
        StartCoroutine(spawnWaves());
        addFuel();
    }

    void Update()
    {
        if (isGameOver)
        {

            guiElements[0].text = "Press R for restart";
            guiElements[1].text = "FINAL SCORE:" + (int)(timeDriven * mps);

            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }

        }
        else
        {
            
            timeDriven += Time.deltaTime;
            fuel -= 0.5f;
            guiElements[1].text = "Distance: " + (int)(timeDriven * mps);
            guiElements[2].text = "Fuel: " + (int)fuel;
        }
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
    public void addFuel()
    {
        if (!isGameOver)
        {
            fuel += score;
        }
    }
}
