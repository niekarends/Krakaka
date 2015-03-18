using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;	
}
public class PlayerController : MonoBehaviour {

	public float movementSpeed, jumpHeight, tilt, rotation;
	private bool isAirborne;
	public Boundary boundary;
	public float maxRotation, maxTilt;
	public GameObject[] explosion;
	private GameController gameController;


	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		} else {
			Debug.Log ("Cant find gamecontroller");
		}
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
	
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f , moveVertical);
		rigidbody.AddForce (movement * movementSpeed );

		if(Input.GetButtonDown("Jump") && !isAirborne){
			Jump ();
		}

		//Keep the player object within the boundaries of the playfield
		rigidbody.position = new Vector3
			(
				Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
				rigidbody.position.y,
				Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
				);

		rigidbody.rotation = Quaternion.Euler (0, Mathf.Clamp(rigidbody.velocity.x * rotation, -maxRotation, maxRotation) , Mathf.Clamp(rigidbody.velocity.x * -tilt, -maxTilt, maxTilt));

        if (gameController.getFuel() <= 1)
        {
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(explosion[0], transform.position, spawnRotation);
            DisableChildren();
            Destroy(this);
            gameController.gameOver();
        }
	}

	void Update() {

		Vector3 down = transform.TransformDirection(-Vector3.up);
		Vector3 fwd = transform.TransformDirection (new Vector3(0,0,1f));
		//Debug.DrawRay (new Vector3(transform.position.x,transform.position.y+0.5f, transform.position.z), fwd, Color.red);

		//Test if youre standing on the ground
		if (Physics.Raycast (transform.position, down, .5f)) {
			isAirborne = false;
		} else {
			isAirborne = true;
		}
		//Code to test frontal hits with cars using raycast
        Ray ray = new Ray(new Vector3(transform.position.x,transform.position.y+0.5f, transform.position.z), fwd);
        RaycastHit hit; 
		if (Physics.Raycast(ray, out hit, 0.5f )) {
			//Debug.Log("geraakt door: "+ hit.transform.gameObject.name + "raak");
            if(hit.transform.gameObject.tag == "Enemy"){
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(explosion[0], transform.position, spawnRotation);
                DisableChildren();
                Destroy(this);
                gameController.gameOver();
            }else if(hit.transform.gameObject.tag == "Pickup"){
                gameController.addFuel();
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(explosion[1], hit.transform.position, spawnRotation);
                Destroy(hit.transform.gameObject);
            }
		}
		//Checking for collision with pickups

//		RaycastHit hit2;
//		if (Physics.SphereCast (transform.position, 4.0f, transform.forward*10, out hit2)) {
//			if(hit2.transform.tag == "Pickup") {
//
//			}
//		}


	}

	void Jump() {
		rigidbody.AddForce ( Vector3.up * jumpHeight );
		isAirborne = true;
	}

	public void DisableChildren()  
	{    
		foreach (Transform child in transform)     
		{  
			child.gameObject.SetActiveRecursively(false);   
		}   
	}
}
