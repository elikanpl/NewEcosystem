using UnityEngine;

public class sticky : MonoBehaviour
{
    //If an adjacent block moves, Sticky moves with them in the same direction
    //If Sticky’s move is impossible (is blocked) then they will not move
    //Moves in the same direction as the player no matter which way it moves
    public GameObject player;
    private GridObject playerGridObject;
    private int playerGridX;
    private int playerGridY;
    private int playerPrevGridX;
    private int playerPrevGridY;
    public int movementUnit;
    private GridObject myGridObjScript;
    private int myGridX;
    private int myGridY;
    public bool active;
    public GridManager playerScript;
    public bool playerCanMove;
    public bool stickyMoved;
    public bool stickyDidNotMove;
    public bool clingyIsActive;
    public bool stickyShouldFollow;
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

        playerScript = GameObject.FindWithTag("Grid").GetComponent<GridManager>();
        playerCanMove = playerScript.stickyCanMove;
    }

    // Update is called once per frame
    void Update()
    {
        playerGridX = playerGridObject.gridPosition.x;
        playerGridY = playerGridObject.gridPosition.y;
        playerPrevGridX = GameObject.FindWithTag("Grid").GetComponent<GridManager>().prevGridX;
        playerPrevGridY = GameObject.FindWithTag("Grid").GetComponent<GridManager>().prevGridY;
        playerCanMove = false;

        GameObject.FindWithTag("Grid").GetComponent<GridManager>().stickyCanMove = playerCanMove;
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                print("A pressed in sticky");
                //checks if there is a block to the left that is not the player
                GridObject[] gridObjects = FindObjectsOfType<GridObject>();
                for (int i = 0; i < gridObjects.Length; i++)
                {
                    GridObject gridObject = gridObjects[i];
                    bool a = gridObject.gridPosition.x == myGridX - movementUnit;
                    bool b = gridObject.gridPosition.y == myGridY;
                    bool c = gridObject.gameObject != player;
                    if (a && b && c)
                    {
                        return;
                    }
                }

                if (playerGridX == playerPrevGridX && playerGridY == playerPrevGridY)
                {
                    return;
                }

                //player cannot move past the left bound of the grid
                if (myGridX - movementUnit < 1)
                {
                    myGridX = 1;
                    myGridObjScript.gridPosition.x = myGridX;
                }
                else
                {
                    stickyMoved = true;
                    playerCanMove = true;
                    GameObject.FindWithTag("Grid").GetComponent<GridManager>().stickyCanMove = playerCanMove;
                    myGridX -= movementUnit;
                    myGridObjScript.gridPosition.x = myGridX;
                }
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                //checks if there is a block to the right that is not the player
                GridObject[] gridObjects = FindObjectsOfType<GridObject>();
                for (int i = 0; i < gridObjects.Length; i++)
                {
                    GridObject gridObject = gridObjects[i];
                    bool checkRightX = gridObject.gridPosition.x == myGridX + movementUnit;
                    bool checkRightY = gridObject.gridPosition.y == myGridY;
                    bool isNotPlayer = gridObject.gameObject != player;
                    if (checkRightX && checkRightY && isNotPlayer)
                    {
                        return;
                    }
                }

                if (playerGridX == playerPrevGridX && playerGridY == playerPrevGridY)
                {
                    return;
                }

                //player cannot move past the left bound of the grid
                if (myGridX + movementUnit > 10)
                {
                    myGridX = 10;
                    myGridObjScript.gridPosition.x = myGridX;
                }
                else
                {
                    playerCanMove = true;
                    GameObject.FindWithTag("Grid").GetComponent<GridManager>().stickyCanMove = playerCanMove;
                    myGridX += movementUnit;
                    myGridObjScript.gridPosition.x = myGridX;
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                //checks if there is a block above that is not the player
                GridObject[] gridObjects = FindObjectsOfType<GridObject>();
                for (int i = 0; i < gridObjects.Length; i++)
                {
                    GridObject gridObject = gridObjects[i];
                    bool checkUpX = gridObject.gridPosition.x == myGridX;
                    bool checkUpY = gridObject.gridPosition.y == myGridY - movementUnit;
                    bool isNotPlayer = gridObject.gameObject != player;
                    if (checkUpX && checkUpY && isNotPlayer)
                    {
                        stickyMoved = false;
                        stickyDidNotMove = true;
                        return;
                    }
                }

                if (playerGridX == playerPrevGridX && playerGridY == playerPrevGridY)
                {
                    stickyMoved = false;
                    stickyDidNotMove = true;
                    return;
                }

                //player cannot move past the upper bound of the grid
                if (myGridY - movementUnit < 1)
                {
                    stickyMoved = false;
                    stickyDidNotMove = true;
                    myGridY = 1;
                    myGridObjScript.gridPosition.y = myGridY;
                }
                else
                {
                    stickyMoved = true;
                    stickyDidNotMove = false;
                    playerCanMove = true;
                    GameObject.FindWithTag("Grid").GetComponent<GridManager>().stickyCanMove = playerCanMove;
                    myGridY -= movementUnit;
                    myGridObjScript.gridPosition.y = myGridY;
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                //checks if there is a block below that is not the player
                GridObject[] gridObjects = FindObjectsOfType<GridObject>();
                for (int i = 0; i < gridObjects.Length; i++)
                {
                    GridObject gridObject = gridObjects[i];
                    bool checkDownX = gridObject.gridPosition.x == myGridX;
                    bool checkDownY = gridObject.gridPosition.y == myGridY + movementUnit;
                    bool isNotPlayer = gridObject.gameObject != player;
                    if (checkDownX && checkDownY && isNotPlayer)
                    {
                        return;
                    }
                }

                if (playerGridX == playerPrevGridX && playerGridY == playerPrevGridY)
                {
                    return;
                }

                //sticky cannot move past the lower bound of the grid
                if (myGridY + movementUnit > 5)
                {
                    myGridY = 5;
                    myGridObjScript.gridPosition.y = myGridY;
                }
                else
                {
                    playerCanMove = true;
                    GameObject.FindWithTag("Grid").GetComponent<GridManager>().stickyCanMove = playerCanMove;
                    myGridY += movementUnit;
                    myGridObjScript.gridPosition.y = myGridY;
                }
            }
        }
        ShouldBeActive();
        //ShouldStickyFollow();
        //if (stickyShouldFollow)
        //{
        //    stickyShouldFollow = false;
        //    if (Input.GetKeyDown(KeyCode.W))
        //    {
        //        myGridY -= movementUnit;
        //        myGridObjScript.gridPosition.y = myGridY;
        //        ////checks if there is a block above that is not the player
        //        //GridObject[] gridObjects = FindObjectsOfType<GridObject>();
        //        //for (int i = 0; i < gridObjects.Length; i++)
        //        //{
        //        //    GridObject gridObject = gridObjects[i];
        //        //    bool checkUpX = gridObject.gridPosition.x == myGridX;
        //        //    bool checkUpY = gridObject.gridPosition.y == myGridY - movementUnit;
        //        //    bool isNotPlayer = gridObject.gameObject != player;
        //        //    if (checkUpX && checkUpY && isNotPlayer)
        //        //    {
        //        //        stickyMoved = false;
        //        //        stickyDidNotMove = true;
        //        //        return;
        //        //    }
        //        //}

        //        if (playerGridX == playerPrevGridX && playerGridY == playerPrevGridY)
        //        {
        //            //stickyMoved = false;
        //            //stickyDidNotMove = true;
        //            return;
        //        }

        //        //player cannot move past the upper bound of the grid
        //        if (myGridY - movementUnit < 1)
        //        {
        //            //stickyMoved = false;
        //            //stickyDidNotMove = true;
        //            myGridY = 1;
        //            myGridObjScript.gridPosition.y = myGridY;
        //        }
        //        else
        //        {
        //            //stickyMoved = true;
        //            //stickyDidNotMove = false;
        //            //playerCanMove = true;
        //            //GameObject.FindWithTag("Grid").GetComponent<GridManager>().stickyCanMove = playerCanMove;
        //            myGridY -= movementUnit;
        //            myGridObjScript.gridPosition.y = myGridY;
        //        }
        //    }
        //}
    }

    private void ShouldBeActive()
    {
        //whether sticky is active and should try to follow the player
        if (myGridX - 1 == playerGridX || myGridX + 1 == playerGridX || myGridY - 1 == playerGridY || myGridY + 1 == playerGridY)
        {
            active = true;
        }
        else
        {
            active = false;
        }
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

    public void checkStickyDidNotMove()
    {
        //checks if there is a block above that is not the player
        GridObject[] originalGridObjects = FindObjectsOfType<GridObject>();
        for (int i = 0; i < originalGridObjects.Length; i++)
        {
            GridObject gridObject = originalGridObjects[i];
            bool checkUpX = gridObject.gridPosition.x == myGridX;
            bool checkUpY = gridObject.gridPosition.y == myGridY - movementUnit;
            bool isNotPlayer = gridObject.gameObject != player;
            if (checkUpX && checkUpY && isNotPlayer)
            {
                stickyMoved = false;
                stickyDidNotMove = true;
                return;
            }
            bool checkDownX = gridObject.gridPosition.x == myGridX;
            bool checkDownY = gridObject.gridPosition.y == myGridY + movementUnit;
            if (checkDownX && checkDownY && isNotPlayer)
            { 
                stickyMoved = false;
                stickyDidNotMove = true;
                return;
            }
            bool checkLeftX = gridObject.gridPosition.x == myGridX - movementUnit;
            bool checkLeftY = gridObject.gridPosition.y == myGridY;
            if (checkLeftX && checkLeftY && isNotPlayer)
            {
                stickyMoved = false;
                stickyDidNotMove = true;
                return;
            }
            bool checkRightX = gridObject.gridPosition.x == myGridX + movementUnit;
            bool checkRightY = gridObject.gridPosition.y == myGridY;
            if (checkLeftX && checkLeftY && isNotPlayer)
            {
                stickyMoved = false;
                stickyDidNotMove = true;
                return;
            }
        }

        if (playerGridX == playerPrevGridX && playerGridY == playerPrevGridY)
        {
            stickyMoved = false;
            stickyDidNotMove = true;
            return;
        }


        //player cannot move past the upper bound of the grid
        if (myGridY - movementUnit < 1)
        {
            stickyMoved = false;
            stickyDidNotMove = true;
        }
        else if (myGridY + movementUnit > 5)
        {
            stickyMoved = false;
            stickyDidNotMove = true;
        }
        else if (myGridX + movementUnit > 10)
        {
            stickyMoved = false;
            stickyDidNotMove = true;
        }
        else if (myGridX - movementUnit < 1)
        {
            stickyMoved = false;
            stickyDidNotMove = true;
        }
        else
        {
            stickyMoved = true;
            stickyDidNotMove = false;
            playerCanMove = true;
            GameObject.FindWithTag("Grid").GetComponent<GridManager>().stickyCanMove = playerCanMove;
        }

        if (stickyDidNotMove)
        {
            GridObject[] gridObjects = FindObjectsOfType<GridObject>();
            for (int i = 0; i < gridObjects.Length; i++)
            {
                GridObject gridObject = gridObjects[i];
                bool checkDownX = gridObject.gridPosition.x == myGridX;
                bool checkDownY = gridObject.gridPosition.y == myGridY + movementUnit;
                bool isClingy = gridObject.gameObject.tag == "Clingy";

                if (isClingy)
                {
                    GameObject activeClingy = gridObject.gameObject;
                    Clingy clingyScript = activeClingy.GetComponent<Clingy>();
                    clingyScript.stickyDidNotMove = true;
                }
            }
        }
        else
        {
            GridObject[] gridObjects = FindObjectsOfType<GridObject>();
            for (int i = 0; i < gridObjects.Length; i++)
            {
                GridObject gridObject = gridObjects[i];
                bool checkDownX = gridObject.gridPosition.x == myGridX;
                bool checkDownY = gridObject.gridPosition.y == myGridY + movementUnit;
                bool isClingy = gridObject.gameObject.tag == "Clingy";

                if (isClingy)
                {
                    GameObject activeClingy = gridObject.gameObject;
                    Clingy clingyScript = activeClingy.GetComponent<Clingy>();
                    clingyScript.stickyDidNotMove = false;
                }
            }
        }
    }

    public void ShouldStickyFollow()
    {
        GridObject[] gridObjects = FindObjectsOfType<GridObject>();
        for (int i = 0; i < gridObjects.Length; i++)
        {
            GridObject gridObject = gridObjects[i];
            bool checkUpX = gridObject.gridPosition.x == myGridX;
            bool checkUpY = gridObject.gridPosition.y == myGridY - movementUnit;
            bool isClingy = gridObject.gameObject.tag == "Clingy";
            if (isClingy)
            {
                GameObject activeClingy = gridObject.gameObject;
                Clingy clingyScript = activeClingy.GetComponent<Clingy>();
                clingyIsActive = clingyScript.active == true;
            }
            if (checkUpX && checkUpY && isClingy && clingyIsActive)
            {
                stickyShouldFollow = true;
            }
        }
        
    }
}
