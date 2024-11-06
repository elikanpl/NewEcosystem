using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static WaddleDee;
using Random = UnityEngine.Random;

public class Bobblehead : MonoBehaviour
{
    public enum bobbleheadState
    {
        Walk,
        Scared,
        Confused
    }

    private bobbleheadState _currentState = bobbleheadState.Walk;
    
    private float startLife;
    private float lifespan;
    private float currentTime;
    private float tooClose;
    private int direction;
    private int otherDirection;
    //private string left;
    //private string right;
    //private string up;
    //private string down;
    private float speed;
    private float otherSpeed;
    private float x;
    private float y;
    private float z;
    private Vector3 changeDirection;
    private float spriteHalfHeight;
    private float spriteHalfWidth;
    private float backgroundMinX;
    private float backgroundMinY;
    private float backgroundMaxX;
    private float backgroundMaxY;
    public SpriteRenderer spriteRenderer;
    private Bounds bounds;
    private bool back;
    private bool forth;
    private float shake;
    private bool shouldTremble;
    private bool isConfused;
    private float tooCloseLeopard;
    private Vector3 rotation;
    private Vector3 zRotation;
    private EcosystemManager ecosystemManager;
    // Start is called before the first frame update
    void Start()
    {
        startLife = Time.time;
        lifespan = 10.0f;
        tooClose = 4.5f;
        shake = 0.75f;
        shouldTremble = false;
        isConfused = false;
        tooCloseLeopard = 2.5f;
        rotation = new Vector3 (0f, 0f, 15.0f);
        back = true;
        forth = false;
        direction = Random.Range(1, 5); //new List<string>();
        otherDirection = Random.Range(5, 9);
        //direction.Add(left);
        //direction.Add(right);   
        //direction.Add(up);
        //direction.Add(down);
        speed = 0.05f;
        otherSpeed = 0.035f;
        x = this.gameObject.transform.position.x;
        y = this.gameObject.transform.position.y;
        z = this.gameObject.transform.position.z;

        //sprite values
        spriteHalfHeight = 1.96f/2;
        spriteHalfWidth = 3.28f/2;

        //background values
        backgroundMaxX = 9.95f;
        backgroundMaxY = 7.97f;
        backgroundMinX = -9.95f;
        backgroundMinY = -7.97f;

        bounds = spriteRenderer.bounds;
        print(bounds.size);
        print(bounds.min);
        print(bounds.max);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        if (currentTime - startLife >= lifespan)
        {
            ecosystemManager = FindObjectOfType<EcosystemManager>();
            ecosystemManager.canGenerateBobblehead = false;
            ecosystemManager.bobbleheadTimer = true;
            ecosystemManager.bobbleheadTime = 25;
            Destroy(this.gameObject);
        }

        UpdateState();

        //CheckDisoriented();
        //if (isConfused)
        //{
        //    Disoriented();
        //}
        //print(zRotation);

        //CheckTremble();
        //if(shouldTremble)
        //{
        //    Tremble();
        //}

        //Movement();
        //SecondaryMovement();
        //CheckTremble();
        //if (shouldTremble)
        //{
        //    Tremble();
        //}
    }

    //public void StartState(bobbleheadState newState) //run any code that should run once at the beginning of a new state
    //{
    //    //first run the endstate of our old state
    //    EndState(_currentState);
    //    switch (newState)
    //    {
    //        case bobbleheadState.Walk: //can also be numbers
    //            Movement();
    //            //UpdateGrounded();
    //            //ChangeSprite(stand);
    //            break;

    //        case bobbleheadState.Scared: //can also be numbers
    //            Tremble();
    //            //UpdateGrounded();
    //            //Walk();
    //            //ChangeSprite(walk);
    //            //walk code
    //            break;

    //        case bobbleheadState.Confused: //can also be numbers
    //            Disoriented();
    //            //ChangeSprite(scared);                     //scared code here
    //            break;
    //        default: //all other situations run this (like an else)

    //            break;
    //    }
    //    _currentState = newState;
    //}

    private void UpdateState()
    {
        switch (_currentState)
        {
            case bobbleheadState.Walk: //can also be numbers
                Movement();
                SecondaryMovement();
                CheckTremble();
                if(shouldTremble)
                {
                    _currentState = bobbleheadState.Scared;
                }
                CheckDisoriented();
                if(isConfused)
                {
                    _currentState = bobbleheadState.Confused;
                }
                break;

            case bobbleheadState.Scared: //can also be numbers
                Tremble();
                CheckDisoriented();
                if (isConfused)
                {
                    _currentState = bobbleheadState.Confused;
                }
                CheckTremble();
                if (shouldTremble == false)
                {
                    _currentState = bobbleheadState.Walk;
                }
                break;

            case bobbleheadState.Confused: //can also be numbers
                Disoriented();
                CheckTremble();
                if (shouldTremble)
                {
                    _currentState = bobbleheadState.Scared;
                }
                CheckDisoriented();
                if (isConfused == false)
                { 
                    _currentState = bobbleheadState.Walk;
                }
                break;
        }
    }

    //stops everything that was happening in the previous state (e.g. during a transition between a walk state to a stand state)
    //Gives you a clean slate with no old data
    //private void EndState(bobbleheadState oldState)
    //{
    //    switch (oldState)
    //    {
    //        case bobbleheadState.Walk: //can also be numbers
    //            //UpdateGrounded();

    //            break;

    //        case bobbleheadState.Scared: //can also be numbers
    //            //UpdateGrounded();
    //            //Walk();

    //            //walk code
    //            break;

    //        case bobbleheadState.Confused: //can also be numbers

    //            break;
    //        default: //all other situations run this (like an else)

    //            break;
    //    }
    //}

    private void CheckDisoriented()
    {
        JudgementalLeopard[] leopardList = FindObjectsOfType<JudgementalLeopard>();
        for (int i = 0; i < leopardList.Length; i++)
        {
            JudgementalLeopard leopardIndv = leopardList[i];
            {
                Vector3 leopardPos = leopardIndv.gameObject.transform.position;
                Vector3 myPos = this.gameObject.transform.position;
                float howCloseToLeopard = Vector3.Distance(myPos, leopardPos);
                if (howCloseToLeopard <= tooCloseLeopard)
                {
                    isConfused = true;
                }
                else
                {
                    isConfused = false;
                    gameObject.transform.rotation = Quaternion.identity;
                }
            }
        }
    }

    
    //will spin around a random number of times if it touches the leopard
    private void Disoriented()
    {
        print("rotated");
        //zRotation = gameObject.transform.rotation+rotation;
        gameObject.transform.Rotate(rotation, Space.Self); //= new Vector3 (0, 0, zRotation+rotation);

    }

    //moves around randomly and erratically
    private void Movement()
    {
        if (direction == 1) //left
        {
            if (x+2*speed <= backgroundMinX + spriteHalfWidth)
            {
                direction = Random.Range(1, 5);
            }
            else
            {
                x -= speed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
            }
        }
        else if (direction == 2) //right
        {
            if (x+2*speed >= backgroundMaxX - spriteHalfWidth)
            {
                direction = Random.Range(1, 5);
            }
            else
            {
                x += speed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
            }
        }
        else if (direction == 3) //up
        {
            if (y+2*speed >= backgroundMaxY - spriteHalfHeight)
            {
                direction = Random.Range(1, 5);
            }
            else
            {
                y += speed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
            }
        }
        else if (direction == 4) //down
        {
            if (y+2*speed <= backgroundMinY + spriteHalfHeight)
            {
                direction = Random.Range(1, 5);
            }
            else
            {
                y -= speed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
            }
        }
    }

    private void SecondaryMovement()
    {
        if (otherDirection == 5) //left
        {
            if (x + 2 * speed <= backgroundMinX + spriteHalfWidth)
            {
                otherDirection = Random.Range(5, 9);
            }
            else
            {
                x -= otherSpeed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
            }
        }
        else if (otherDirection == 6) //right
        {
            if (x + 2 * speed >= backgroundMaxX - spriteHalfWidth)
            {
                otherDirection = Random.Range(5, 9);
            }
            else
            {
                x += otherSpeed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
            }
        }
        else if (otherDirection == 7) //up
        {
            if (y + 2 * speed >= backgroundMaxY - spriteHalfHeight)
            {
                otherDirection = Random.Range(5, 9);
            }
            else
            {
                y += otherSpeed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
            }
        }
        else if (otherDirection == 8) //down
        {
            if (y + 2 * speed <= backgroundMinY + spriteHalfHeight)
            {
                otherDirection = Random.Range(5, 9);
            }
            else
            {
                y -= otherSpeed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.tag == "Wall")
        //{
        //    direction = Random.Range(1, 5);
        //}
    }

    private void CheckTremble()
    {
        WizardLizard[] lizardList = FindObjectsOfType<WizardLizard>();
        for (int i = 0; i < lizardList.Length; i++)
        {
            WizardLizard lizardIndv = lizardList[i];
            {
                Vector3 lizardPos = lizardIndv.gameObject.transform.position;
                Vector3 myPos = this.gameObject.transform.position;
                float howCloseToLizard = Vector3.Distance(myPos, lizardPos);
                if (howCloseToLizard <= tooClose)
                {
                    shouldTremble = true;
                }
                else
                {
                    shouldTremble = false;
                }
            }


        }
    }
    //if close to the lizard, will shake back and forth
    private void Tremble()
    {

        if (back)
        {
            float leftShakeX = x - shake;
            this.gameObject.transform.position = new Vector3(leftShakeX, y, z);
            back = false;
            forth = true;
        }
        else if(forth)
        { 
            float leftShakeX = x + shake;
            this.gameObject.transform.position = new Vector3(leftShakeX, y, z);
            back = true;
            forth = false;
        }
    }
}
