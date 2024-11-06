using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
//imports unity ui packages
using UnityEngine.UI;

public class PointHUD : MonoBehaviour
{
    //SerializeField allows private fields to be serialized and shown
    [SerializeField] Text pointText;
    public int points = 0;

    private void Awake ()
    {
        //updates points when game starts
        UpdateHUD();
    }

    public int Points
    {
        get
        {
            return points;
        }

        set
        {
            points = value;
            UpdateHUD();
        }
    }
    /// <summary>
    
    //if points are modified, this automatically updates them
    private void UpdateHUD()
    {
        //sets points to latest value and converts it to string
        pointText.text = points.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.tag == "Reset")
        //{
        //    nextCourse = true;
        //    transform.position = startPosition;
        //    bellSound.Play();
        //}

        //if (other.gameObject.tag == "Mouth")
        //{
        //    nextCourse = true;
        //    transform.position = startPosition;
        //    bellSound.Play();
        //}

        //if (other.gameObject.tag == "Knife")
        //{
        //    print("contact");
        //    currentScale *= shrinkPinball;
        //    this.transform.localScale = currentScale;
        //    //currentScale = testScale;
        //}

        if (other.gameObject.tag == "Whisk")
        {
            points += 50;
        }
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
