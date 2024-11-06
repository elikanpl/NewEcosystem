using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rFlipper : MonoBehaviour
{
    public float hingeUpperLimit = 45;
    public float hingeLowerLimit = -45;


    //Vector3 hingeRotation = new Vector3(x: 0f, y: 0f, z: );

    //public float flipperForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //this shold be on the anchor not the flipper
        if (Input.GetKeyDown(KeyCode.M))
        {
            transform.eulerAngles += new Vector3(0f, 0f, -15f);

            //Vector2 forceDirection = new Vector2(x: addXForce, y: addYForce);

            ////putting the rigidbody of the pinball in the flipper
            //Rigidbody2D otherRigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();

            ////flipping the pinball up. Impulse is more of a quick motion while force is more uniform
            //otherRigidbody2D.AddForce(forceDirection);
        }


    }

    //when the flipper collides with 
    private void OnCollisionEnter2D(Collision2D other)
    {
        //defining the force and direction of the flipper
        Vector2 forceDirection = new Vector2(x: 0f, y: 10f);

        //putting the rigidbody of the pinball in the flipper
        Rigidbody2D otherRigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();

        //flipping the pinball up. Impulse is more of a quick motion while force is more uniform
        otherRigidbody2D.AddForce(forceDirection, ForceMode2D.Impulse);

        //hinge joint rotates up
        HingeJoint2D ownHingeJoint2D = gameObject.GetComponent<HingeJoint2D>();

    }
}

