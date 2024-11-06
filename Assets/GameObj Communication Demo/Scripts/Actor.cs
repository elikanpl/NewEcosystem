//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Actor : MonoBehaviour
//{
//    public float moveSpeed;

//    public Sprite rockSprite;
//    public Sprite paperSprite;
//    public Sprite scissorsSprite;

//    public string rpsType;

//    //other.gameObject.GetComponent<SpriteRenderer>().sprite = rockSprite. With this, you can change anything that is public
    
//    // Start is called before the first frame update
//    private void Start()
//    {
//        //change the starting sprite to match our type
//        //if (rpsTyype == "rock")
//        //    this.GameObject.GetComponent
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//        float randy = Random.Range(0f, 1f);

//        //string ownSprite = this.GetComponent<SpriteRenderer>()
        
//        //moves the obj around randomly
//        if (randy < 0.25f) //whether you can have floats or ints is determined by the min and max number
//        {
//            this.transform.position += new Vector3(moveSpeed, 0, 0);
//        }
//        else if (randy < 0.5f) //whether you can have floats or ints is determined by the min and max number
//        {
//            this.transform.position += new Vector3(-moveSpeed, 0, 0);
//        }
//        else if (randy < 0.75f) //whether you can have floats or ints is determined by the min and max number
//        {
//            this.transform.position += new Vector3(0, moveSpeed, 0);
//        }
//        else if (randy < 1f) //whether you can have floats or ints is determined by the min and max number
//        {
//            this.transform.position += new Vector3(0, -moveSpeed, 0);
//        }

//        //checks collision and chooses whether to transform
//        //if()
//    }
//}

//private void OnCollisionEnter2D(Collision2D other)
//{
//    other.gameObject.GetComponent<SpriteRenderer>().sprite //access what you collided with
//}