using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whisk : MonoBehaviour
{
    public float addXForce;
    public float addYForce;
    
    // Start is called before the first frame update
    void Start()
    {
        addXForce = 2f;
        addYForce = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //defining the force and direction of the flipper
        Vector2 forceDirection = new Vector2(x: addXForce, y: addYForce);

        //putting the rigidbody of the pinball in the flipper
        Rigidbody2D otherRigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();

        //flipping the pinball up. Impulse is more of a quick motion while force is more uniform
        otherRigidbody2D.AddForce(forceDirection);
    }
}
