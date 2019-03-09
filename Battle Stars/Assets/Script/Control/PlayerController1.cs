using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{

    //This controller includes a FauxGravityBody

    public float speed = 15;
    public float jumpSpeed = 5f;
    public float characterHeight = 2f;
    private Vector3 direction = Vector3.zero;
    public FauxGravityAttractor _FauxGravityAttractor; // Calls the attractor script
    private Transform myTransform;
    float jumpRest = 0.05f; // Sets the ammount of time to "rest" between jumps
    float jumpRestRemaining = 0; //The counter for Jump Rest

    RaycastHit hit;
    private float distToGround;

    void Start()
    {
        GetComponent<Rigidbody>().useGravity = false; // Disables Gravity
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        myTransform = transform;
    }

    void Update()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        jumpRestRemaining -= Time.deltaTime; // Counts down the JumpRest Remaining

        if (direction.magnitude > 1)
        {
            direction = direction.normalized; // stops diagonal movement from being faster than straight movement
        }

        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            distToGround = hit.distance;
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
        }

        if (Input.GetButton("Jump") && distToGround < (characterHeight * .5) && jumpRestRemaining < 0)
        { // If the jump button is pressed and the ground is less the 1/2 the hight of the character away from the character:
            jumpRestRemaining = jumpRest; // Resets the jump counter
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * jumpSpeed * 100); // Adds upward force to the character multitplied by the jump speed, multiplied by 100
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(direction) * speed * Time.deltaTime);
        if (_FauxGravityAttractor)
        {
            _FauxGravityAttractor.Attract(myTransform);
        }
    }
}
