using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFramer : MonoBehaviour
{
    //The thing we're going to have the camera follow
    public Transform target;

    //How much padding there is on the camera
    public float padding;

    //If the target isn't set, we won't follow it
    private bool doFollowTarget;

    //the camera we're moving
    private Camera mainCam;

    //saved information at the beginning of the game to reset the camera
    private Vector3 resetPos;
    private float resetSize;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (target == null)
            doFollowTarget = false;
        else
            doFollowTarget = true;

        resetPos = mainCam.transform.position;
        resetSize = mainCam.orthographicSize;
    }

    private void Update()
    {
        if (doFollowTarget)
        {
            //calculate the camera's edges
            float vExtent = mainCam.orthographicSize;
            float hExtent = vExtent * Screen.width / Screen.height;

            float minX = mainCam.transform.position.x - hExtent;
            float maxX = mainCam.transform.position.x + hExtent;
            float minY = mainCam.transform.position.y - vExtent;
            float maxY = mainCam.transform.position.y + vExtent;

            //grab the difference between the target's position and the camera's bounds
            float xDif = 0f, yDif = 0f;
            if (target.position.x + padding > maxX)
                xDif = target.position.x - maxX + padding; //will be positive
            
            if (target.position.y + padding > maxY)
                yDif = target.position.y - maxY + padding;

            //increase the camera's bounds if it goes off the top or the right
            float increaseAmount = Mathf.Max(xDif, yDif) / 2f;

            //increase the size of the camera, and move it so the bottom left stays in the same spot
            mainCam.orthographicSize += increaseAmount;
            mainCam.transform.position += new Vector3(increaseAmount * Screen.width / Screen.height, increaseAmount, 0f);
        }
    }
}
