using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool mouthClosed = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mouthClosed == false)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }
}
