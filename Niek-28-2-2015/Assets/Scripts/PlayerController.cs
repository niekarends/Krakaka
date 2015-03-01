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
	public GameObject explosion;
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

		if(Input.GetKeyDown(KeyCode.Space) && !isAirborne){
			Jump ();
		}

		rigidbody.position = new Vector3
			(
				Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
				rigidbody.position.y,
				Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
				);
		rigidbody.rotation = Quaternion.Euler (0, rigidbody.velocity.x * rotation, rigidbody.velocity.x * -tilt);
	

	}

	void Update() {
		Vector3 down = transform.TransformDirection(-Vector3.up);
		Vector3 fwd = transform.TransformDirection (new Vector3(0,0,0.5f));
		Debug.DrawRay (transform.position, fwd );
		if (Physics.Raycast (transform.position, down, .5f)) {
			isAirborne = false;
		} else {
			isAirborne = true;
		}
		if (Physics.Raycast (new Vector3(transform.position.x,transform.position.y+2, transform.position.z), fwd, 0.5f)) {
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate(explosion,transform.position, spawnRotation);
			DisableChildren();
			Destroy(this);
			gameController.gameOver();
		}
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
