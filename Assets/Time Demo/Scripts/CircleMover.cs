using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMover : MonoBehaviour
{
    public float speed;
    public int totalMovingTime;
    private bool isMoving = false;
    private float startingTime;
    //private bool isMoving;
    //When using the frame rate, it becomes unreliable trying to do things based on how long has passed. 
    //Setting things to work based on delta time, or the change in the amount of time and is less impacted by frame rate
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine("DoMove");
            //isMoving = true;
            //startingTime = startingTime.time;
        }

        //if(startingTime.time >= startingTime + totalMovingTime)
        //{
        //    isMoving = false;
        //}

        //if (isMoving)
        //    MoveCircle();
    }

    private void MoveCircle()
    {
        //this.transform.position += new Vector3(speed, 0) * totalMovingTime.deltaTime;
    }

    private void TankFrameRate()
    {
        for (int i = 0; i < 1000; i++)
        {
            Debug.Log("slowing down framerate");
        }
    }

    //type of coroutine, runs alongside regular code so it only happens when relevant
    private IEnumerator DoMove()
    {
        //print(totalMovingTime.time);
        yield return new WaitForSeconds(3f);
        print(Time.time);
        yield return new WaitForSeconds(3f);
        print(Time.time);
        yield return new WaitForSeconds(3f);
        print(Time.time);
        yield return new WaitForSeconds(3f);
        print(Time.time);
        yield return new WaitForSeconds(3f);
        print(Time.time);
    }
}
