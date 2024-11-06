using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slick : MonoBehaviour
{
    //Your “normal” sokoban block
    //Can be pushed by another block
    //Presumably can only be pushed and not pulled by the player
    public GameObject player;
    private GridObject playerGridObject;
    private int playerGridX;
    private int playerGridY;
    public int movementUnit;
    private GridObject myGridObjScript;
    private int myGridX;
    private int myGridY;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerGridObject = player.GetComponent<GridObject>();
        playerGridX = playerGridObject.gridPosition.x;
        playerGridY = playerGridObject.gridPosition.y;
        movementUnit = 1;
        myGridObjScript = this.GetComponent<GridObject>();
        myGridX = myGridObjScript.gridPosition.x;
        myGridY = myGridObjScript.gridPosition.y;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerGridX = playerGridObject.gridPosition.x;
        playerGridY = playerGridObject.gridPosition.y;

        if (active)
        {
            if (Input.GetKeyDown(KeyCode.A) && playerGridX == myGridX+1 && playerGridY == myGridY)
            {
                //checks if there is a block to the left
                GridObject[] gridObjects = FindObjectsOfType<GridObject>();
                for (int i = 0; i < gridObjects.Length; i++)
                {
                    GridObject gridObject = gridObjects[i];
                    bool a = gridObject.gridPosition.x == myGridX - movementUnit;
                    bool b = gridObject.gridPosition.y == myGridY;
                    //bool c = gridObject.gameObject != player;
                    if (a && b)
                    {
                        //print("return + a: " + a + " b: " + b + " c: " + c);
                        //print(gridObject.gameObject.name);
                        return;
                    }
                }

                //player cannot move past the left bound of the grid
                if (myGridX - movementUnit < 1)
                {
                    myGridX = 1;
                    myGridObjScript.gridPosition.x = myGridX;
                }
                else
                {
                    myGridX -= movementUnit;
                    myGridObjScript.gridPosition.x = myGridX;
                }
            }
            
            //slick is pushed right
            if (Input.GetKeyDown(KeyCode.D) && playerGridX == myGridX - 1 && playerGridY == myGridY)
            {
                //checks if there is a block to the right
                GridObject[] gridObjects = FindObjectsOfType<GridObject>();
                for (int i = 0; i < gridObjects.Length; i++)
                {
                    GridObject gridObject = gridObjects[i];
                    bool a = gridObject.gridPosition.x == myGridX + movementUnit;
                    bool b = gridObject.gridPosition.y == myGridY;
                    //bool c = gridObject.gameObject != player;
                    if (a && b)
                    {
                        //print("return + a: " + a + " b: " + b + " c: " + c);
                        //print(gridObject.gameObject.name);
                        return;
                    }
                }

                //player cannot move past the right bound of the grid
                if (myGridX + movementUnit > 10)
                {
                    myGridX = 10;
                    myGridObjScript.gridPosition.x = myGridX;
                }
                else
                {
                    myGridX += movementUnit;
                    myGridObjScript.gridPosition.x = myGridX;
                }
            }

            //slick is pushed up
            if (Input.GetKeyDown(KeyCode.W) && playerGridX == myGridX && playerGridY == myGridY + 1)
            {
                //checks if there is a block above
                GridObject[] gridObjects = FindObjectsOfType<GridObject>();
                for (int i = 0; i < gridObjects.Length; i++)
                {
                    GridObject gridObject = gridObjects[i];
                    bool a = gridObject.gridPosition.x == myGridX;
                    bool b = gridObject.gridPosition.y == myGridY - movementUnit;
                    if (a && b)
                    {
                        return;
                    }
                }

                //player cannot move past the upper bound of the grid
                if (myGridY - movementUnit < 1)
                {
                    myGridY = 1;
                    myGridObjScript.gridPosition.y = myGridY;
                }
                else
                {
                    myGridY -= movementUnit;
                    myGridObjScript.gridPosition.y = myGridY;
                }
            }

            //slick is pushed down
            if (Input.GetKeyDown(KeyCode.S) && playerGridX == myGridX && playerGridY == myGridY - 1)
            {
                //checks if there is a block below
                GridObject[] gridObjects = FindObjectsOfType<GridObject>();
                for (int i = 0; i < gridObjects.Length; i++)
                {
                    GridObject gridObject = gridObjects[i];
                    bool a = gridObject.gridPosition.x == myGridX;
                    bool b = gridObject.gridPosition.y == myGridY + movementUnit;
                    if (a && b)
                    {
                        return;
                    }
                }

                //player cannot move past the lower bound of the grid
                if (myGridY + movementUnit > 5)
                {
                    myGridY = 5;
                    myGridObjScript.gridPosition.y = myGridY;
                }
                else
                {
                    myGridY += movementUnit;
                    myGridObjScript.gridPosition.y = myGridY;
                }
            }
        }

        ShouldBeActive();
    }

    private void ShouldBeActive()
    {
        //whether slick is active and should be pushed by the player
        if (myGridX - 1 == playerGridX && myGridY == playerGridY)
        {
            active = true;
        }
        else if (myGridX + 1 == playerGridX && myGridY == playerGridY)
        {
            active = true;
        }
        else if (myGridY - 1 == playerGridY && myGridX == playerGridX)
        {
            active = true;
        }
        else if (myGridY + 1 == playerGridY && myGridX == playerGridX)
        {
            active = true;
        }
        else
        {
            active = false;
        }

    }
}
