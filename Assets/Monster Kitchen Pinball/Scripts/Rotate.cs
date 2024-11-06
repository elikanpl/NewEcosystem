using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float timeBetweenActivations;
    public float timeCounter;

    public bool countTime;

    public float angleChange;
    
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenActivations = 3f;
        timeCounter = 0f;
        countTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (countTime)
        {
            timeCounter += 1;
        }

        if(timeCounter > timeBetweenActivations)
        {
            timeCounter = 0;
            transform.eulerAngles += new Vector3(0f, 0f, angleChange);
        }
    }

}
