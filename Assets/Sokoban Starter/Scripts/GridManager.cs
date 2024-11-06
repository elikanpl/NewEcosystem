using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject player;
    private int playerGridX;
    private int playerGridY;
    public int prevGridX;
    public int prevGridY;
    public int movementUnit;
    public bool canMove;
    public bool stickyCanMove;
    
    private GridObject playerGridObject;
    //public List stickyList;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        //get reference to player's GridObject component so it can be used
        playerGridObject = player.GetComponent<GridObject>();

        //player's x and y coordinates on the grid
        playerGridX = playerGridObject.gridPosition.x;
        playerGridY = playerGridObject.gridPosition.y;

        movementUnit = 1;
        canMove = false;
        stickyCanMove = false;
        //stickyList = GameObject.FindGameObjectsWithTag("Sticky");
    }

    // Update is called once per frame
    void Update()
    {
        canMove = false;
        //movement
        //move left
        if (Input.GetKeyDown(KeyCode.A))
        {
            //checks if there is a block to the left
            GridObject[] gridObjects = FindObjectsOfType<GridObject>();
            for (int i = 0; i < gridObjects.Length; i++)
            {
                GridObject gridObject = gridObjects[i];
                bool checkLeftX = gridObject.gridPosition.x == playerGridX - movementUnit;
                bool checkLeftY = gridObject.gridPosition.y == playerGridY;
                bool isNotSticky = gridObject.tag != "Sticky";

                if (checkLeftX && checkLeftY && isNotSticky == false && stickyCanMove == false)
                {
                    return;
                }

                if (checkLeftX && checkLeftY && isNotSticky)
                {
                    return;
                }
            }
            //player cannot move past the left bound of the grid
            if (playerGridX - movementUnit < 1)
            {
                playerGridX = 1;
                playerGridObject.gridPosition.x = playerGridX;
            }
            else
            {
                canMove = true;
                prevGridX = playerGridX;
                playerGridX -= movementUnit;
                playerGridObject.gridPosition.x = playerGridX;
            }
        }
        //move right
        if (Input.GetKeyDown(KeyCode.D))
        {
            //checks if there is a block to the right
            GridObject[] gridObjects = FindObjectsOfType<GridObject>();
            for (int i = 0; i < gridObjects.Length; i++)
            {
                GridObject gridObject = gridObjects[i];
                bool checkRightX = gridObject.gridPosition.x == playerGridX + movementUnit;
                bool checkRightY = gridObject.gridPosition.y == playerGridY;
                bool isNotSticky = gridObject.tag != "Sticky";

                if (checkRightX && checkRightY && isNotSticky == false && stickyCanMove == false)
                {
                    return;
                }

                if (checkRightX && checkRightY && isNotSticky)
                {
                    return;
                }
            }
            //player cannot move past the right bound of the grid
            if (playerGridX + movementUnit > 10)
            {
                playerGridX = 10;
                playerGridObject.gridPosition.x = playerGridX;
            }
            else
            {
                //canMove = true;
                prevGridX = playerGridX;
                playerGridX += movementUnit;
                playerGridObject.gridPosition.x = playerGridX;
            }
        }
        //move down
        if (Input.GetKeyDown(KeyCode.S))
        {
            //checks if there is a block below
            GridObject[] gridObjects = FindObjectsOfType<GridObject>();
            for (int i = 0; i < gridObjects.Length; i++)
            {
                GridObject gridObject = gridObjects[i];
                bool checkDownX = gridObject.gridPosition.x == playerGridX;
                bool checkDownY = gridObject.gridPosition.y == playerGridY + movementUnit;
                bool isNotSticky = gridObject.tag != "Sticky";
                if (checkDownX && checkDownY && isNotSticky == false && stickyCanMove == false)
                {
                    return;
                }
                if (checkDownX && checkDownY && isNotSticky)
                {
                    return;
                }
            }
            //player cannot move past the lower bound of the grid
            if (playerGridY + movementUnit > 5)
            {
                playerGridY = 5;
                playerGridObject.gridPosition.y = playerGridY;
            }
            else
            {
                prevGridY = playerGridY;
                playerGridY += movementUnit;
                playerGridObject.gridPosition.y = playerGridY;
            }
        }
        //move up
        if (Input.GetKeyDown(KeyCode.W))
        {
            //checks if there is a block above
            GridObject[] gridObjects = FindObjectsOfType<GridObject>();
            for (int i = 0; i < gridObjects.Length; i++)
            {
                GridObject gridObject = gridObjects[i];
                bool checkUpX = gridObject.gridPosition.x == playerGridX;
                bool checkUpY = gridObject.gridPosition.y == playerGridY - movementUnit;
                bool isNotSticky = gridObject.tag != "Sticky";
                if (checkUpX && checkUpY && isNotSticky == false && stickyCanMove == false)
                {
                    return;
                }
                if (checkUpX && checkUpY && isNotSticky)
                {
                    return;
                }
            }
            //player cannot move past the lower bound of the grid
            if (playerGridY - movementUnit < 1)
            {
                playerGridY = 1;
                playerGridObject.gridPosition.y = playerGridY;
            }
            else
            {
                prevGridY = playerGridY;
                playerGridY -= movementUnit;
                playerGridObject.gridPosition.y = playerGridY;
            }
        }
    }

    private void MovePosition()
    {
        //player.x = GridMaker.reference.TopLeft.x + GridMaker.reference.cellWidth * (gridPosition.x - 0.5f);
        //player.y = GridMaker.reference.TopLeft.y - GridMaker.reference.cellWidth * (gridPosition.y - 0.5f);
    }
}
