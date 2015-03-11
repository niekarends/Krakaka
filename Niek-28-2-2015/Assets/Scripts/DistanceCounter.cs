using UnityEngine;
using System.Collections;

public class DistanceCounter : MonoBehaviour
{

    private GameController gameController;
    private float timeDriven;
    // Use this for initialization
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cant find gamecontroller");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeDriven += Time.deltaTime;
        gameController.guiElements[1].text = "Distance: " + (int)(timeDriven * 27);
    }
}
