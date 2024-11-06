using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFlipper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            gameObject.GetComponent<HingeJoint2D>().useMotor = true;
        }
        else
        {
            gameObject.GetComponent<HingeJoint2D>().useMotor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //defining the force and direction of the flipper
        Vector2 forceDirection = new Vector2(x: 2f, y: 10f);

        //putting the rigidbody of the pinball in the flipper
        Rigidbody2D otherRigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();

        //flipping the pinball up. Impulse is more of a quick motion while force is more uniform
        otherRigidbody2D.AddForce(forceDirection, ForceMode2D.Impulse);
        otherRigidbody2D.AddTorque(100);
        //hinge joint rotates up
        // HingeJoint2D ownHingeJoint2D = gameObject.GetComponent<HingeJoint2D>();

    }
}
