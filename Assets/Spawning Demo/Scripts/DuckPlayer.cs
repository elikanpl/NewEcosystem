using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DuckPlayer : MonoBehaviour
{
    public GameObject duckPrefab;
    
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnDuck(GetMousePosition());
        }
    }

    private void SpawnDuck(Vector3 duckPosition)
    {
        //spawn something at duckPosition
        //capital letter game object instead of lower case. this reference to a specific gameobject that the script is attached to
        //this.gameObject.name does a similar thing. This does not work for other things like colliders or audio sources because
        //there might be multiple of each of those components or none of them. But they always have transforms and are always
        //a gameObject.
        //the capitalized GameObject refers to the whole public class and everything that is a GameObject
        GameObject.Instantiate(duckPrefab, duckPosition, Quaternion.identity);
    }

    //Call to get the mouse position in world coordinates
    private Vector3 GetMousePosition()
    {
        //Grab the mouse position
        Vector3 mousePos = Input.mousePosition;

        //convert it to world coordinates
        mainCam.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        return mousePos;
    }
}
