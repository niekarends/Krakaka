using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

	public float gravity = 20;
	public float speed = 8;
	public float acceleration = 30;
	public float jumpHeight = 12;
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;

	private PlayerPhysics playerPhysics;
	// Use this for initializatio
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
	}
	
	// Update is called once per frame
	void Update () {

		targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
		currentSpeed = IncrementSpeed( currentSpeed, targetSpeed, acceleration);
		if (playerPhysics.grounded) {
			amountToMove.y = 0;
			if (Input.GetButtonDown ("Jump")) {
				amountToMove.y = jumpHeight;
			}
		}
		
		
		Ray ray = new Ray(new Vector2(amountToMove.x,amountToMove.y), new Vector2(0,10));
		Debug.DrawRay(ray.origin, ray.direction * 100);

		amountToMove.x = currentSpeed;
		//amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.move(amountToMove * Time.deltaTime);
	}
	private float IncrementSpeed( float n, float target, float a){
		if (n == target) {
			return n;
		} else {
			float dir = Mathf.Sign(target - n);
			n += a* Time.deltaTime * dir;
			return( dir == Mathf.Sign(target - n))? n: target;
		}
	}

}
