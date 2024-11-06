using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TreeEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Bobblehead;

public class JudgementalLeopard : MonoBehaviour
{
    public enum leopardState
    {
        Walk,
        GoAway,
        Jump
    }

    private leopardState _currentState = leopardState.Walk;

    private float startLife;
    private float lifespan;
    private float currentTime;
    private float speed;
    private float pounce;
    private float noticeLizard;
    private Vector3 direction;
    private Vector3 moveTowards;
    private bool startPounce;
    private float walkAway;
    private float noticeBobblehead;
    private Vector3 newDirection;
    private Vector3 goTowards;
    private bool goAway;
    private Vector3 newPosition;
    private float spriteHalfHeight;
    private float spriteHalfWidth;
    private float backgroundMinX;
    private float backgroundMinY;
    private float backgroundMaxX;
    private float backgroundMaxY;
    private float upperLimit;
    private float lowerLimit;
    private float leftLimit;
    private float rightLimit;
    private Vector3 changeDirection;
    private float perlinA;
    private float perlinB;
    private float perlinC;
    private float perlinD;
    private float perlinNoise;
    private float changePerlin;
    private float x;
    private float y;
    private float z;
    private EcosystemManager ecosystemManager;

    // Start is called before the first frame update
    void Start()
    {
        startLife = Time.time;
        lifespan = 30.0f;
        speed = 0.02f;
        pounce = 1.5f;
        noticeLizard = 4.50f;
        walkAway = -0.3f;
        noticeBobblehead = 3f;
        goAway = false;

        //sprite values
        spriteHalfHeight = 2.28f / 2;
        spriteHalfWidth = 2.81f / 2;

        //background values
        backgroundMaxX = 9.95f;
        backgroundMaxY = 7.97f;
        backgroundMinX = -9.95f;
        backgroundMinY = -7.97f;

        //limits
        upperLimit = backgroundMaxY - spriteHalfHeight;
        lowerLimit = backgroundMinY + spriteHalfHeight;
        leftLimit = backgroundMinX + spriteHalfWidth;
        rightLimit = backgroundMaxX - spriteHalfWidth;

        perlinA = 0.25f;
        perlinB = 0.5f;
        perlinC = 0.75f;
        perlinD = 1.0f;
        changePerlin = 0.1f;

        _currentState = leopardState.Walk;
    }

    // Update is called once per frame
    void Update()
    {
        x = this.gameObject.transform.position.x;
        y = this.gameObject.transform.position.y;
        z = this.gameObject.transform.position.z;
        currentTime = Time.time;
        if (currentTime - startLife >= lifespan)
        {
            ecosystemManager = FindObjectOfType<EcosystemManager>();
            ecosystemManager.canGenerateLeopard = false;
            ecosystemManager.leopardTimer = true;
            ecosystemManager.leopardTime = 150;

            Destroy(this.gameObject);
        }
        UpdateState();

        //ShouldPounce();
        //if (startPounce)
        //{
        //    Pounce();
        //}

        //ShouldLeave();
        //if (goAway)
        //{
        //    Leave();
        //}
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //kills lizard
        if (other.gameObject.tag == "WizardLizard")
        { 
            Destroy(other.gameObject, 0.5f);
        }
    }

    private void UpdateState()
    {
        switch (_currentState)
        {
            case leopardState.Walk: //can also be numbers
                Move();
                ShouldLeave();
                if (goAway)
                {
                    _currentState = leopardState.GoAway;
                }
                ShouldPounce();
                if (startPounce)
                {
                    _currentState = leopardState.Jump;
                }
                break;

            case leopardState.GoAway: //can also be numbers
                Leave();
                ShouldPounce();
                if (startPounce)
                {
                    _currentState = leopardState.Jump;
                }
                ShouldLeave();
                if (goAway == false)
                {
                    _currentState = leopardState.Walk;
                }
                break;

            case leopardState.Jump: //can also be numbers
                Pounce();
                ShouldLeave();
                if (goAway)
                {
                    _currentState = leopardState.GoAway;
                }
                ShouldPounce();
                if (startPounce == false)
                {
                    _currentState = leopardState.Walk;
                }
                break;
        }
    }

    private void Move()
    {
        perlinNoise = Mathf.PerlinNoise(x / 2f, y / 2f);
        if (perlinNoise <= perlinA)
        {
            if (x + speed >= rightLimit)
            {
                return;
            }
            else
            {
                x += speed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
            }
        }
        else if (perlinNoise > perlinA && perlinNoise <= perlinB)
        {
            if (x - speed <= leftLimit)
            {
                return;
            }
            else
            {
                x -= speed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
            }
        }
        else if (perlinNoise > perlinB && perlinNoise <= perlinC)
        {
            if (y + speed >= upperLimit)
            {
                return;
            }
            else
            {
                y += speed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
            }
        }
        else if (perlinNoise > perlinC && perlinNoise <= perlinD)
        {
            if (y - speed <= lowerLimit)
            {
                return;
            }
            else
            {
                y -= speed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
            }
        }
    }

    private void ShouldLeave()
    {
        Bobblehead[] bobbleheadList = FindObjectsOfType<Bobblehead>();
        for (int i = 0; i < bobbleheadList.Length; i++)
        {
            Bobblehead bobbleheadIndv = bobbleheadList[i];
            {
                Vector3 bobbleheadPos = bobbleheadIndv.gameObject.transform.position;
                Vector3 myPos = this.gameObject.transform.position;
                float howCloseToBobblehead = Vector3.Distance(myPos, bobbleheadPos);
                if (howCloseToBobblehead <= noticeBobblehead)
                {
                    newDirection = bobbleheadPos - myPos;
                    goTowards = (newDirection.normalized) * walkAway;
                    goAway = true;
                }
            }
        }
    }

    private void Leave()
    {
        newPosition = this.gameObject.transform.position + goTowards;
        //this.gameObject.transform.position = this.gameObject.transform.position + goTowards;
        if (newPosition.y < upperLimit && newPosition.y > lowerLimit && newPosition.x > leftLimit && newPosition.x < rightLimit)
        {
            this.gameObject.transform.position = this.gameObject.transform.position + goTowards;
        }

        goAway = false;
    }

    private void ShouldPounce()
    {
        WizardLizard[] lizardList = FindObjectsOfType<WizardLizard>();
        for (int i = 0; i < lizardList.Length; i++)
        {
            WizardLizard lizardIndv = lizardList[i];
            {
                Vector3 lizardPos = lizardIndv.gameObject.transform.position;
                Vector3 myPos = this.gameObject.transform.position;
                float howCloseToLizard = Vector3.Distance(myPos, lizardPos);
                if (howCloseToLizard <= noticeLizard)
                {
                    direction = lizardPos - myPos;
                    moveTowards = (direction.normalized) * pounce;
                    startPounce = true;
                }
            }
        }
    }
    //lunge towards WizardLizard if close enough 
    private void Pounce()
    {
        this.gameObject.transform.position = this.gameObject.transform.position + moveTowards;
        startPounce = false;
    }
}
