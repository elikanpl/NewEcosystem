using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPan : MonoBehaviour
{
    public float timeBetweenActivations;

    public bool flipFood;

    public float heightChange;

    public float stopActivation;

    public float timeCounter;

    new float startHeight;
    new float currentHeight;
    new float stopHeight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        //defining the force and direction of the flipper
        Vector2 forceDirection = new Vector2(x: 0f, y: 5f);

        //putting the rigidbody of the pinball in the flipper
        Rigidbody2D otherRigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();

        //flipping the pinball up. Impulse is more of a quick motion while force is more uniform
        otherRigidbody2D.AddForce(forceDirection, ForceMode2D.Impulse);

        otherRigidbody2D.AddTorque(10000);
    }
}
