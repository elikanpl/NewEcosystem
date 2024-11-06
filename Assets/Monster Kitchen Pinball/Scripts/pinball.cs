using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinball : MonoBehaviour
{
    public PointHUD pointsHUD;
    new bool nextCourse;
    public int currentCourse;
    new bool endGame;
    new Vector3 startPosition;
    public GameObject monsterMouth;
    new float shrinkPinball;
    new Vector3 currentScale;
    new Vector3 testScale;
    public AudioSource bellSound;
    public Sprite appetizer;
    public Sprite mainCourse;
    public Sprite dessert;
    public int winScore1;
    public int winScore2;
    public int winScore3;
    new bool eatingAllowed1;
    new bool eatingAllowed2;
    new bool eatingAllowed3;
    //private monsterScript;

    // Start is called before the first frame update
    void Start()
    {
        //to make sure that you don't need to drag the component into the slot every time you change its name but this only works if you have one PointsHUD
        //pointsHUD = FindObjectOfType<PointHUD>();
        nextCourse = false;
        currentCourse = 0;
        endGame = false;
        startPosition = new Vector3(0f, 13f, 0f);
        currentScale = this.transform.localScale;
        shrinkPinball = 0.95f;
        testScale = new Vector3(0.25f, 0.25f, 0.25f);
        //monsterMouth = monsterMouth.GetComponent<Monster>().mouthClosed;
        eatingAllowed1 = false;
        eatingAllowed2 = false;
        eatingAllowed3 = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Reset")
        {
            nextCourse = true;
            transform.position = startPosition;
            bellSound.Play();
        }

        if(other.gameObject.tag == "Mouth")
        {
            if (eatingAllowed1)
            {
                monsterMouth.SetActive(false);
                nextCourse = true;
                transform.position = startPosition;
                bellSound.Play();
                pointsHUD.Points += 500;
            }

        }

        if (other.gameObject.tag == "Knife")
        {
            print("contact");
            currentScale *= shrinkPinball;
            this.transform.localScale = currentScale;
            //currentScale = testScale;
            pointsHUD.Points += 300;
        }

        if (other.gameObject.tag == "Whisk")
        {
            pointsHUD.Points += 10;
        }

        if (other.gameObject.tag == "Bowl")
        {
            pointsHUD.Points += 5;
        }

        if (other.gameObject.tag == "FryingPan")
        {
            pointsHUD.Points += 50;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(endGame)
        {
            transform.position = startPosition;

            if (Input.GetKeyDown(KeyCode.R))
            {
                currentCourse = 0;
                nextCourse = false;
                endGame = false;
            }
        }
        
        if (nextCourse)
        {
            currentCourse += 1;
            nextCourse = false;
            monsterMouth.SetActive(false);
        }

        if (currentCourse == 0 && pointsHUD.Points >= 1000 && pointsHUD.Points < 3000)
        {
            eatingAllowed1 = true;
            monsterMouth.SetActive(true);
        }

        if (currentCourse == 2 && pointsHUD.Points >= 3000 && pointsHUD.Points < 6000)
        {
            eatingAllowed2 = true;
            monsterMouth.SetActive(true);
        }

        if (currentCourse == 3 && pointsHUD.Points >= 6000)
        {
            eatingAllowed3 = true;
            monsterMouth.SetActive(true);
        }
        //if (currentCourse == 0)
        //{
        //    spriteRenderer.sprite = appetizer;
        //}
        //else if (currentCourse == 1)
        //{
        //    spriteRenderer.sprite = mainCourse;
        //}
        //else if (currentCourse == 2)
        //{
        //    spriteRenderer.sprite = dessert;
        //}
        //if (currentCourse == 0)
        //{
        //    this.SpriteRenderer.sprite = appetizer;
        //}
        //else if (currentCourse == 1)
        //{
        //    this.SpriteRenderer.sprite = mainCourse;
        //}
        //else if (currentCourse == 2)
        //{
        //    this.SpriteRenderer.sprite = dessert;
        //}

        if (currentCourse >= 3)
        {
            endGame = true;
        }
    }

    //void ChangeSprite()
    //{
    //    if (currentCourse == 0)
    //    {
    //        this.SpriteRenderer.sprite = appetizer;
    //    }
    //    else if (currentCourse == 1)
    //    {
    //        this.SpriteRenderer.sprite = mainCourse;
    //    }
    //    else if (currentCourse == 2)
    //    {
    //        this.SpriteRenderer.sprite = dessert;
    //    }
    //}
}