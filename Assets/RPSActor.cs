using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float randy = Random.Range(0f, 1f);

        if (randy < 0.25f) //whether you can have floats or ints is determined by the min and max number
        {
            this.transform.position += new Vector3(moveSpeed, 0, 0);
        }
        else if (randy < 0.5f) //whether you can have floats or ints is determined by the min and max number
        {
            this.transform.position += new Vector3(-moveSpeed, 0, 0);
        }
        else if (randy < 0.75f) //whether you can have floats or ints is determined by the min and max number
        {
            this.transform.position += new Vector3(0, moveSpeed, 0);
        }
        else if (randy < 1f) //whether you can have floats or ints is determined by the min and max number
        {
            this.transform.position += new Vector3(0, -moveSpeed, 0);
        }
    }
}