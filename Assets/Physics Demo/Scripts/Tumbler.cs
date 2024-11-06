using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tumbler : MonoBehaviour
{
    public float tumbleForce;

    public List<KeyCode> keyOrder = new List<KeyCode>();
    private int desiredKey = 0;

    //the rigibody on this object
    private Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        int tumbleInput = ReadTumbleInput();

        rb.AddTorque(-tumbleForce * tumbleInput, ForceMode2D.Impulse);
    }

    //Returns 1 for correct input, 0 for no input, and -1 for wrong input
    private int ReadTumbleInput()
    {
        for (int i = 0; i < keyOrder.Count; i++)
        {
            //Actually start with the desired key, then wrap around the order
            int index = (i + desiredKey) % keyOrder.Count;

            if (Input.GetKeyDown(keyOrder[index]))
            {
                //If the correct key is pressed update the desiredKey and return 1
                if (index == desiredKey)
                {
                    //move one more down the order, wrap back to 0 if needed
                    desiredKey = (desiredKey + 1) % keyOrder.Count;
                    return 1;
                }

                //If a key was pressed, but it was the wrong one, return -1
                return -1;
            }
        }

        //If no keys were pressed, return 0
        return 0;
    }
}
