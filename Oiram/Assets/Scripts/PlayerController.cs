using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int movementSpeed;
	private bool isJumping;
	public float jumpheight;

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = (movement * movementSpeed);

		if(Input.GetKey("space")  && isJumping == false){
			this.rigidbody.AddForce(Vector3.up * jumpheight);
			isJumping = true;
			Debug.Log("Jumping!");
		}

	}

}
