using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour
{
    public float jumpForce;
    
    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        //this uses the physics system instead of just manually moving it around with += speed
        _rb = this.GetComponent<Rigidbody2D>();
        jumpForce = 20;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //There is no left AND right only right and negative right. Same for back front up and down
        //Works better with something that has explosive force instead of a held key
        //Update applies physics on every frame, not every physics frame instead of being independent. Whereas everytime physics updates are called, fixedupdate will run
        if(Input.GetKeyDown(KeyCode.W))
        {
            //Debug.Log("added a force!");
            //print("debug");
            //this.transform only affects the objects up down left right instead of the game's up down left right with vector3.up
            _rb.AddForce(this.transform.up*jumpForce, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Debug.Log("added a force!");
            //print("debug");
            _rb.AddForce(-this.transform.up * jumpForce, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //Debug.Log("added a force!");
            //print("debug");
            _rb.AddForce(this.transform.right * jumpForce, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Debug.Log("added a force!");
            //print("debug");
            _rb.AddForce(-this.transform.right * jumpForce, ForceMode2D.Impulse);
        }

        //if (Input.GetKey(KeyCode.W))
        //    _rb.MovePosition(Vector3(0, moveSpeed, 0));
        //if (Input.GetKey(KeyCode.S))
        //    _rb.MovePosition(Vector3(0, -moveSpeed, 0));
        //if (Input.GetKey(KeyCode.A))
        //    _rb.MovePosition(Vector3(moveSpeed, 0, 0));
        //if (Input.GetKey(KeyCode.D))
        //    _rb.MovePosition(Vector3(-moveSpeed, 0, 0));
    }
}
