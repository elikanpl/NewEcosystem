using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class WaddleDee : MonoBehaviour
{
    public enum WaddleState
    {
        Stand,
        Walk,
        Scared
    }
    
    private WaddleState _currentState = WaddleState.Stand;

    
    
    //*******DON'T CHANGE THESE*******
    public Sprite stand;
    public Sprite walk;
    public Sprite scared;
    public float groundedCastLength;
    public float groundedInFrontDistance;
    private bool prevGrounded = false;
    public LayerMask groundedLayerMask;
    public float accel;
    public float maxSpeed;
    public Vector2 jumpForce;
    private Rigidbody2D rb;
    private Camera mainCam;
    //********************************


    //***USE THESE FOR THE EXERCISE***
    private int facing = -1; //A value of 1 faces Waddle Dee to the right, - 1 faces them to the left
    private bool jumped = false; //True when Waddle Dee has jumped and has not landed yet
    private bool grounded = false; //Is Waddle Dee on the ground?
    private bool groundInFront = false; //Is there ground in front of Waddle Dee?
    private bool wallInFront = false; //Is there a wall in front of Waddle Dee?
    //********************************


    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }

    private void Update()
    {
        UpdateState();
 

    }

    public void StartState(WaddleState newState) //run any code that should run once at the beginning of a new state
    {
        //first run the endstate of our old state
        EndState(_currentState);
        switch (newState)
        {
            case WaddleState.Stand: //can also be numbers
                UpdateGrounded();
                ChangeSprite(stand);
                break;

            case WaddleState.Walk: //can also be numbers
                UpdateGrounded();
                Walk();
                ChangeSprite(walk);
                //walk code
                break;

            case WaddleState.Scared: //can also be numbers
                ChangeSprite(scared);                     //scared code here
                break;
            default: //all other situations run this (like an else)

                break;
        }
        _currentState = newState;
    }

    private void UpdateState()
    {
        switch (_currentState)
        {
            case WaddleState.Stand: //can also be numbers
                UpdateGrounded();
                GetMousePosition();

                //if(Absolute(GetMousePosition().x-this.transform.position.x) < 20 &&)

                if(wallInFront)
                {
                    facing *= -1;
                }
                else
                {
                    _currentState = WaddleState.Walk;
                }

                break;

            case WaddleState.Walk: //can also be numbers
                UpdateGrounded();
                GetMousePosition();
                if (groundInFront == false)
                {
                    Jump();
                }
                else
                {
                    Walk();
                }

                if(wallInFront == false)
                {
                    _currentState = WaddleState.Stand;
                }

                
                //walk code
                break;

            case WaddleState.Scared: //can also be numbers
                
                //scared code here
                break;
            
            default: //all other situations run this (like an else)

                break;
        }
    }

        //stops everything that was happening in the previous state (e.g. during a transition between a walk state to a stand state)
        //Gives you a clean slate with no old data
    private void EndState(WaddleState oldState)
    {
        switch (oldState)
        {
            case WaddleState.Stand: //can also be numbers
                UpdateGrounded();
                
                break;

            case WaddleState.Walk: //can also be numbers
                UpdateGrounded();
                Walk();
                
                //walk code
                break;

            case WaddleState.Scared: //can also be numbers
                
                break;
            default: //all other situations run this (like an else)

                break;
        }
    }


    //Call every frame to update the boolean variables "grounded," "groundInFront," and "wallInFront"
    private void UpdateGrounded()
    {
        prevGrounded = grounded;

        grounded = Physics2D.Raycast(this.transform.position, Vector2.down, groundedCastLength, groundedLayerMask);
        groundInFront = Physics2D.Raycast(this.transform.position + (Vector3.right * groundedInFrontDistance * facing), Vector2.down, groundedCastLength * 1.5f, groundedLayerMask);
        wallInFront = Physics2D.Raycast(this.transform.position, Vector2.right * facing, groundedCastLength * 1.5f, groundedLayerMask);

        Debug.DrawRay(this.transform.position, Vector2.down * groundedCastLength, Color.red);
        Debug.DrawRay(this.transform.position + (Vector3.right * groundedInFrontDistance * facing), Vector2.down * groundedCastLength, Color.red);
        Debug.DrawRay(this.transform.position, Vector2.right * facing * groundedCastLength, Color.red);

        if (!prevGrounded && grounded)
            jumped = false;
    }

    //Call every frame to walk the Waddle Dee forward based on their facing
    private void Walk()
    {
        rb.AddForce(Vector2.right * facing * accel * (maxSpeed - Mathf.Abs(rb.velocity.x)));
    }



    //Call once to make Waddle Dee jump forward at an angle
    private void Jump()
    {
        if (jumped)
            return;

        rb.AddForce(new Vector2(jumpForce.x * facing, jumpForce.y), ForceMode2D.Impulse);
        jumped = true;
    }

    //Call to get the mouse position in world coordinates
    private Vector3 GetMousePosition()
    {
        //Grab the mouse position
        Vector3 mousePos = Input.mousePosition;

        //convert it to world coordinates
        return mainCam.ScreenToWorldPoint(mousePos);
    }

    //Call this and pass it the "stand," "walk," or "scared" sprite variables to change the sprite
    private void ChangeSprite(Sprite newSprite)
    {
        this.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    private void CurrentPosition()
    {
        float currentX = this.transform.position.x;
        float currentY = this.transform.position.y;
        float currentZ = this.transform.position.z;   
    }
}
