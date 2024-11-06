using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class high : MonoBehaviour
{
    public Vector2 target;
    private Rigidbody2D _rb;
    private Vector2 ownLocation;
    private Vector2 direction;
    private float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("high five static").transform.position;
        _rb = this.GetComponent<Rigidbody2D>();
        jumpForce = 20;
        ownLocation = GameObject.Find("high five").transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = target - ownLocation;
        direction = direction.normalized;
        if (Input.GetKeyDown(KeyCode.Space)) ;
        {
            _rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);
        }
    }
}
