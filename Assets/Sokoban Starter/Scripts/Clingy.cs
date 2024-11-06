using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clingy : MonoBehaviour
{
    //Can be pulled by another block
    //Cannot be pushed by another block
    //The inverse of Smooth
    public GameObject player;
    private GridObject playerGridObject;
    private int playerGridX;
    private int playerGridY;
    public int movementUnit;
    private GridObject myGridObjScript;
    private int myGridX;
    private int myGridY;
    public bool active;
    public bool stickyIsActive;
    public bool stickyDidNotMove;
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
        //ShouldBeActiveOtherBlocks();
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.A) && playerGridX == myGridX - 1 && playerGridY == myGridY)
            {
                //checks if there is a block to the left
                //GridObject[] gridObjects = FindObjectsOfType<GridObject>();
                //for (int i = 0; i < gridObjects.Length; i++)
                //{
                //    GridObject gridObject = gridObjects[i];
                //    bool a = gridObject.gridPosition.x == myGridX - movementUnit;
                //    bool b = gridObject.gridPosition.y == myGridY;
                //    //bool c = gridObject.gameObject != player;
                //    if (a && b)
                //    {
                //        //print("return + a: " + a + " b: " + b + " c: " + c);
                //        //print(gridObject.gameObject.name);
                //        return;
                //    }
                //}

                //block cannot move past the left bound of the grid
                //if (myGridX - movementUnit < 1)
                //{
                //    myGridX = 1;
                //    myGridObjScript.gridPosition.x = myGridX;
                //}
                //else
                //{
                    myGridX -= movementUnit;
                    myGridObjScript.gridPosition.x = myGridX;
                //}
            }

            //slick is pushed right
            if (Input.GetKeyDown(KeyCode.D) && playerGridX == myGridX + 1 && playerGridY == myGridY)
            {
                ////checks if there is a block to the right
                //GridObject[] gridObjects = FindObjectsOfType<GridObject>();
                //for (int i = 0; i < gridObjects.Length; i++)
                //{
                //    GridObject gridObject = gridObjects[i];
                //    bool a = gridObject.gridPosition.x == myGridX + movementUnit;
                //    bool b = gridObject.gridPosition.y == myGridY;
                //    //bool c = gridObject.gameObject != player;
                //    if (a && b)
                //    {
                //        //print("return + a: " + a + " b: " + b + " c: " + c);
                //        //print(gridObject.gameObject.name);
                //        return;
                //    }
                //}

                ////block cannot move past the right bound of the grid
                //if (myGridX + movementUnit > 10)
                //{
                //    myGridX = 10;
                //    myGridObjScript.gridPosition.x = myGridX;
                //}
                //else
                //{
                    myGridX += movementUnit;
                    myGridObjScript.gridPosition.x = myGridX;
                //}
            }

            //slick is pushed up
            if (Input.GetKeyDown(KeyCode.W) && playerGridX == myGridX && playerGridY == myGridY - 1)
            {
                ////checks if there is a block above
                //GridObject[] gridObjects = FindObjectsOfType<GridObject>();
                //for (int i = 0; i < gridObjects.Length; i++)
                //{
                //    GridObject gridObject = gridObjects[i];
                //    bool a = gridObject.gridPosition.x == myGridX;
                //    bool b = gridObject.gridPosition.y == myGridY - movementUnit;
                //    if (a && b)
                //    {
                //        return;
                //    }
                //}

                ////player cannot move past the upper bound of the grid
                //if (myGridY - movementUnit < 1)
                //{
                //    myGridY = 1;
                //    myGridObjScript.gridPosition.y = myGridY;
                //}
                //else
                //{
                    myGridY -= movementUnit;
                    myGridObjScript.gridPosition.y = myGridY;
                //}
            }

            //slick is pushed down
            if (Input.GetKeyDown(KeyCode.S) && playerGridX == myGridX && playerGridY == myGridY + 1)
            {
                ////checks if there is a block below
                //GridObject[] gridObjects = FindObjectsOfType<GridObject>();
                //for (int i = 0; i < gridObjects.Length; i++)
                //{
                //    GridObject gridObject = gridObjects[i];
                //    bool a = gridObject.gridPosition.x == myGridX;
                //    bool b = gridObject.gridPosition.y == myGridY + movementUnit;
                //    if (a && b)
                //    {
                //        return;
                //    }
                //}

                ////player cannot move past the lower bound of the grid
                //if (myGridY + movementUnit > 5)
                //{
                //    myGridY = 5;
                //    myGridObjScript.gridPosition.y = myGridY;
                //}
                //else
                //{
                    myGridY += movementUnit;
                    myGridObjScript.gridPosition.y = myGridY;
                //}
            }
        }

        ShouldBeActive();
    }

    private void ShouldBeActive()
    {
        //whether clingy is active and should try to follow the player
        //if (myGridX - 1 == playerGridX || myGridX + 1 == playerGridX || myGridY - 1 == playerGridY || myGridY + 1 == playerGridY)
        //{
        //    active = true;
        //}
        //else
        //{
        //    active = false;
        //}
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

    private void ShouldBeActiveOtherBlocks()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            //clingy and slick cannot pull/push each other so the only block that will affect clingy other than the player is sticky
            //checks if there is a sticky to the left
            GridObject[] gridObjects = FindObjectsOfType<GridObject>();
            for (int i = 0; i < gridObjects.Length; i++)
            {
                GridObject gridObject = gridObjects[i];
                bool checkLeftX = gridObject.gridPosition.x == myGridX - movementUnit;
                bool checkLeftY = gridObject.gridPosition.y == myGridY;
                bool isSticky = gridObject.gameObject.tag == "Sticky";
                if (isSticky)
                {
                    GameObject activeSticky = gridObject.gameObject;
                    sticky stickyScript = activeSticky.GetComponent<sticky>();
                    stickyIsActive = stickyScript.active == true;
                    stickyScript.checkStickyDidNotMove();
                }
                if (checkLeftX && checkLeftY && stickyIsActive)
                {
                    if (stickyDidNotMove)
                    {
                        return;
                    }
                    else
                    {
                        myGridX -= movementUnit;
                        myGridObjScript.gridPosition.x = myGridX;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //clingy and slick cannot pull/push each other so the only block that will affect clingy other than the player is sticky
            //checks if there is a sticky to the right
            GridObject[] gridObjects = FindObjectsOfType<GridObject>();
            for (int i = 0; i < gridObjects.Length; i++)
            {
                GridObject gridObject = gridObjects[i];
                bool checkRightX = gridObject.gridPosition.x == myGridX + movementUnit;
                bool checkRightY = gridObject.gridPosition.y == myGridY;
                bool isSticky = gridObject.gameObject.tag == "Sticky";
                if (isSticky)
                {
                    GameObject activeSticky = gridObject.gameObject;
                    sticky stickyScript = activeSticky.GetComponent<sticky>();
                    stickyIsActive = stickyScript.active == true;
                    stickyScript.checkStickyDidNotMove();
                }
                if (checkRightX && checkRightY && stickyIsActive)
                {
                    if (stickyDidNotMove)
                    {
                        return;
                    }
                    else
                    {
                        myGridX += movementUnit;
                        myGridObjScript.gridPosition.x = myGridX;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //clingy and slick cannot pull/push each other so the only block that will affect clingy other than the player is sticky
            //checks if there is a sticky above
            GridObject[] gridObjects = FindObjectsOfType<GridObject>();
            for (int i = 0; i < gridObjects.Length; i++)
            {
                GridObject gridObject = gridObjects[i];
                bool checkUpX = gridObject.gridPosition.x == myGridX;
                bool checkUpY = gridObject.gridPosition.y == myGridY - movementUnit;
                bool isSticky = gridObject.gameObject.tag == "Sticky";
                
                if (isSticky)
                {
                    GameObject activeSticky = gridObject.gameObject;
                    sticky stickyScript = activeSticky.GetComponent<sticky>();
                    stickyIsActive = stickyScript.active == true;
                    stickyScript.checkStickyDidNotMove();
                    Debug.Log(stickyDidNotMove);
                    //stickyDidNotMove = stickyScript.stickyDidNotMove == true;
                    
                }
                //solving this will stop blocks from colliding with one another as well as from moving before they are supposed to 
                if (checkUpX && checkUpY && stickyIsActive)
                {
                    if (stickyDidNotMove)
                    {
                        return;
                    }
                    else
                    {
                        myGridY -= movementUnit;
                        myGridObjScript.gridPosition.y = myGridY;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //clingy and slick cannot pull/push each other so the only block that will affect clingy other than the player is sticky
            //checks if there is a sticky above
            GridObject[] gridObjects = FindObjectsOfType<GridObject>();
            for (int i = 0; i < gridObjects.Length; i++)
            {
                GridObject gridObject = gridObjects[i];
                bool checkDownX = gridObject.gridPosition.x == myGridX;
                bool checkDownY = gridObject.gridPosition.y == myGridY + movementUnit;
                bool isSticky = gridObject.gameObject.tag == "Sticky";

                if (isSticky)
                {
                    GameObject activeSticky = gridObject.gameObject;
                    sticky stickyScript = activeSticky.GetComponent<sticky>();
                    stickyIsActive = stickyScript.active == true;
                    stickyScript.checkStickyDidNotMove();
                    Debug.Log(stickyDidNotMove);
                    //stickyDidNotMove = stickyScript.stickyDidNotMove == true;

                }
                //solving this will stop blocks from colliding with one another as well as from moving before they are supposed to 
                if (checkDownX && checkDownY && stickyIsActive)
                {
                    if (stickyDidNotMove)
                    {
                        return;
                    }
                    else
                    {
                        myGridY += movementUnit;
                        myGridObjScript.gridPosition.y = myGridY;
                    }
                }
            }
        }
    }
}


