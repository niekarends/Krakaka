using UnityEngine;
using System.Collections;

public class ChasePlayer : MonoBehaviour
{
    private GameObject target;
    public float rotation, intensity, maxFollowDistance;

    // Use this for initialization
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject;
        }
        else
        {
            Debug.Log("Cant find player to follow");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            float distance = Mathf.Abs(target.transform.position.z - transform.position.z);
            if (distance < maxFollowDistance)
            {
                //Make the object move into direction of target
                float xForce = target.transform.position.x - transform.position.x;
                rigidbody.AddForce(new Vector3(xForce * intensity, 0, 0));

                //add the rotation in accordance to movement
                rigidbody.rotation = Quaternion.Euler(rigidbody.rotation.x, rigidbody.velocity.x * rotation + 180, rigidbody.rotation.z);
            }
        }
    }
}
