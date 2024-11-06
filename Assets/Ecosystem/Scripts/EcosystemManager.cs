using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcosystemManager : MonoBehaviour
{
    private int noOfLeopards;
    private int noOfLizards;
    private int noOfBobbleheads;
    public GameObject leopard;
    public bool canGenerateLeopard;
    public bool leopardTimer;
    public int leopardTime;
    public bool canGenerateLizard;
    public bool lizardTimer;
    public int lizardTime;
    private int maxLizardTime;
    public bool canGenerateBobblehead;
    public bool bobbleheadTimer;
    public int bobbleheadTime;
    private int maxBobbleheadTime;
    private int maxLeopardTime;
    public GameObject leopardPrefab;
    public GameObject lizardPrefab;
    public GameObject bobbleheadPrefab;

    public SpriteRenderer spriteRenderer;
    private Bounds bounds;
    // Start is called before the first frame update
    void Start()
    {
        bounds = spriteRenderer.bounds;
        print("start");
        print(bounds.size);
        print(bounds.min);
        print(bounds.max);
        print("end");

        canGenerateLeopard = true;
        leopardTimer = false;
        leopardTime = 0;
        maxLeopardTime = 750;

        canGenerateLizard = true;
        lizardTimer = false;
        lizardTime = 0;
        maxLeopardTime = 600;

        canGenerateBobblehead = true;
        bobbleheadTimer = false;
        bobbleheadTime = 0;
        maxBobbleheadTime = 85;

        Random.Range(-5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        checkLeopardNo();
        if(noOfLeopards < 2 && canGenerateLeopard)
        {
            print("new leopard");
            Instantiate(leopardPrefab, new Vector3 (Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
            canGenerateLeopard = false;
            leopardTimer = true;
            leopardTime = 0;
        }

        if(leopardTimer)
        {
            leopardTime++;
            if (leopardTime > maxLeopardTime)
            {
                canGenerateLeopard=true;
                leopardTimer = false;
                leopardTime = 0;
            }
        }

        checkLizardNo();
        if (noOfLizards < 3 && canGenerateLizard)
        {
            print("new lizard");
            Instantiate(lizardPrefab, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
            canGenerateLizard = false;
            lizardTimer = true;
            lizardTime = 0;
        }

        if (lizardTimer)
        {
            lizardTime++;
            if (lizardTime > maxLizardTime)
            {
                canGenerateLizard = true;
                lizardTimer = false;
                lizardTime = 0;
            }
        }

        checkBobbleheadNo();
        if (noOfBobbleheads < 7 && canGenerateBobblehead)
        {
            print("new bobblehead");
            Instantiate(bobbleheadPrefab, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
            Instantiate(bobbleheadPrefab, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
            canGenerateBobblehead = false;
            bobbleheadTimer = true;
            bobbleheadTime = 0;
        }

        if (bobbleheadTimer)
        {
            bobbleheadTime++;
            if (bobbleheadTime > maxBobbleheadTime)
            {
                canGenerateBobblehead = true;
                bobbleheadTimer = false;
                bobbleheadTime = 0;
            }
        }
    }

    private void checkLeopardNo()
    {
        JudgementalLeopard[] judgementalLeopards = FindObjectsOfType<JudgementalLeopard>();
        noOfLeopards = judgementalLeopards.Length;
    }

    private void checkLizardNo()
    {
        WizardLizard[] wizardLizards = FindObjectsOfType<WizardLizard>();
        noOfLizards = wizardLizards.Length;
    }

    private void checkBobbleheadNo()
    {
        Bobblehead[] bobbleheads = FindObjectsOfType<Bobblehead>();
        noOfBobbleheads = bobbleheads.Length;
    }

    private void updateLeopardNo()
    {

    }

    /*
     Checklist
    - Choose a background from Flickr that is Commercial use [DONE] - 10 points
    - WizardLizard - 30 points
        - Spawns in based on rules/systems/context
        - Unique movement 
        - Three unique behaviours
            - Chase Bobblehead [DONE]
            - Movement 
            - Teleport [DONE]
        - Spawns out based on rules/systems/context [DONE]
    
    - JudgementalLeopard - 30 points
        - Spawns in based on rules/systems/context
        - Unique movement
        - Three unique behaviours
            - Move [DONE]
            - Pounce [DONE]
            - Leave[DONE]
        - Spawns out based on rules/systems/context [DONE]
   
    - Bobblehead - 30 points
        - Spawns in based on rules/systems/context
        - Unique movement [DONE]
        - Three unique behaviours [DONE]
            - Disoriented [DONE]
            - Movement [DONE]
            - Tremble [DONE]
        - Spawns out based on rules/systems/context [DONE]

    NEED HELP WITH
    - Lizard disappearing randomly + clipping through boundaries: check order in layers and z depth
    - Only rotating 15 degrees then stopping + only moves in one direction when trembling: check if disoriented() is being called every update and if the quarternion 
    thing is resetting the rotateion

     */
}
