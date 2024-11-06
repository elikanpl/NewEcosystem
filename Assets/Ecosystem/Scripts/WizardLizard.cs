using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Bobblehead;

public class WizardLizard : MonoBehaviour
{
    public enum lizardState
    {
        Walk,
        Chase,
        Teleport
    }

    private lizardState _currentState = lizardState.Walk;

    private float startLife;
    private float lifespan;
    private float currentTime;
    private float x;
    private float y;
    private float z;
    private float perlinNoise;
    private float noticeBobblehead;
    private float tooClose;
    private float isClose;
    private float spriteHalfHeight;
    private float spriteHalfWidth;
    private float backgroundMinX;
    private float backgroundMinY;
    private float backgroundMaxX;
    private float backgroundMaxY;
    private Vector3 changeDirection;
    private float speed;
    private float perlinA;
    private float perlinB;
    private float perlinC;
    private float perlinD;
    private float changePerlin;
    private bool shouldTeleport;
    private bool startChase;
    private Vector3 direction;
    private Vector3 moveTowards;
    private EcosystemManager ecosystemManager;
    // Start is called before the first frame update
    void Start()
    {
        //how long it stays alive
        startLife = Time.time;
        lifespan = 20.0f;

        //interactions with leopard and bobblehead
        noticeBobblehead = 5.0f;
        tooClose = 1.0f;
        shouldTeleport = false;

        //sprite values
        spriteHalfHeight = 0.98f;
        spriteHalfWidth = 1.64f;

        //background values
        backgroundMaxX = 9.95f;
        backgroundMaxY = 7.97f;
        backgroundMinX = -9.95f;
        backgroundMinY = -7.97f;

        speed = 0.05f;

        perlinA = 0.25f;
        perlinB = 0.5f;
        perlinC = 0.75f;
        perlinD = 1.0f;
        changePerlin = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        //constantly updates current time
        currentTime = Time.time;

        //updates current x and y positions
        x = this.gameObject.transform.position.x;
        y = this.gameObject.transform.position.y;
        z = this.gameObject.transform.position.z;

        //kills creature after a set amount of time
        if (currentTime - startLife >= lifespan)
        {
            ecosystemManager = FindObjectOfType<EcosystemManager>();
            ecosystemManager.canGenerateLizard = false;
            ecosystemManager.lizardTimer = true;
            ecosystemManager.lizardTime = 120;

            Destroy(this.gameObject);
        }

        UpdateState();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //kills bobblehead
        if (other.gameObject.tag == "Bobblehead")
        {
            Destroy(other.gameObject, 0.5f);
        }
    }

    private void ShouldChase()
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
                    direction = bobbleheadPos - myPos;
                    moveTowards = (direction.normalized) * speed;
                    startChase = true;
                }
            }
        }
    }

    //if in range of a bobblehead, will chase it
    private void chaseBobblehead()
    {
        this.gameObject.transform.position = this.gameObject.transform.position + moveTowards;
        startChase = false;
    }

    private void UpdateState()
    {
        switch (_currentState)
        {
            case lizardState.Walk: //can also be numbers
                Movement();
                ShouldLizardTeleport();
                if(shouldTeleport)
                {
                    _currentState = lizardState.Teleport;
                }
                ShouldChase();
                if(startChase)
                {
                    _currentState = lizardState.Chase;
                }
                break;

            case lizardState.Chase: //can also be numbers
                chaseBobblehead();
                ShouldLizardTeleport();
                if (shouldTeleport)
                {
                    _currentState = lizardState.Teleport;
                }
                ShouldChase();
                if(startChase == false)
                {
                    _currentState = lizardState.Walk;
                }
                break;

            case lizardState.Teleport: //can also be numbers
                Teleport();
                if (startChase)
                {
                    _currentState = lizardState.Chase;
                }
                ShouldLizardTeleport();
                if(shouldTeleport == false)
                {
                    _currentState = lizardState.Walk;
                }
                break;
        }
    }

    //random movement generated by Perlin noise
    private void Movement()
    {
        perlinNoise = Mathf.PerlinNoise(x / 10f, y / 10f);
        if (perlinNoise <= perlinA)
        {
            if (x + speed >= backgroundMaxX - spriteHalfWidth)
            {
                x -= speed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
                perlinA -= changePerlin;
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
            if (x - speed <= backgroundMinX + spriteHalfWidth)
            {
                x += speed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
                perlinA += changePerlin;
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
            if (y + speed >= backgroundMaxY - spriteHalfHeight)
            {
                y -= speed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
                perlinC -= changePerlin;
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
            if (y - speed <= backgroundMinY + spriteHalfHeight)
            {
                y += speed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
                perlinC += changePerlin;
            }
            else
            {
                y -= speed;
                changeDirection = new Vector3(x, y, z);
                gameObject.transform.position = changeDirection;
            }
        }
        /*
         input my x and y/any two numbers you can consistently increase or decrease into perlin noise function
        it will produce a number between 0 and 1
        You can do things like have if ( <= 0.25) {move up} etc.
         * */
    }

    private void UpdateMovement()
    {
        //this.transform.position.x = x;
    }

    private void ShouldLizardTeleport()
    {
        JudgementalLeopard[] leopardList = FindObjectsOfType<JudgementalLeopard>();
        for (int i = 0; i < leopardList.Length; i++)
        {
            JudgementalLeopard leopardIndv = leopardList[i];
            {
                Vector3 leopardPos = leopardIndv.gameObject.transform.position;
                Vector3 myPos = this.gameObject.transform.position;
                float howCloseToLeopard = Vector3.Distance(myPos, leopardPos);
                if (howCloseToLeopard <= tooClose)
                {
                    shouldTeleport = true;
                }
            }
        }
    }

    //if in range of a leopard, teleport away
    private void Teleport()
    {
        float randomX = Random.Range(backgroundMinX + spriteHalfWidth, backgroundMaxX - spriteHalfWidth);
        float randomY = Random.Range(backgroundMinY + spriteHalfHeight, backgroundMaxY - spriteHalfHeight);
        float currentZ = this.gameObject.transform.position.z;
        this.gameObject.transform.position = new Vector3(randomX, randomY, currentZ);
        shouldTeleport = false;
    }
}
