using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL : MonoBehaviour
{

    public float moveSpeed = 15;
    private Vector3 moveDir;
    private bool isInAir = false;
    public Rigidbody rb;

    float jumpRest = 0.05f; // Sets the ammount of time to "rest" between jumps
    float jumpRestRemaining = 0; //The counter for Jump Rest
    private float distToGround;
    public float characterHeight = 2f;
    public float jumpSpeed = 5f;
    private Vector3 direction = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        jumpRestRemaining -= Time.deltaTime; // Counts down the JumpRest Remaining

        if (direction.magnitude > 1)
        {
            direction = direction.normalized; // stops diagonal movement from being faster than straight movement
        }

        if (Input.GetKeyDown("space") && distToGround < (characterHeight * .5) && jumpRestRemaining < 0 && !isInAir)
        { // If the jump button is pressed and the ground is less the 1/2 the hight of the character away from the character:
            isInAir = true;
            jumpRestRemaining = jumpRest; // Resets the jump counter
            rb.AddRelativeForce(Vector3.up * jumpSpeed * 100); // Adds upward force to the character multitplied by the jump speed, multiplied by 100
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter()
    {
        isInAir = false;
    }
}
