using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    //fields: kind of like declaring a variable that will work across start and update
    public int health = 10; //All functions are automatically assumed to be private
    //Public vs private are access modifiers
    //Public scripts are the same as GML where different scripts can interact with each other so GameObjects can interact with each other
    private string name = "Ezio Euditorre"; //to solve the issue of the variable being local to start, move the curly bracket to encompass update
    //Private scripts cannot be accessed from another script or another object with the same script

    //Below this is where functions go. Functions might also be called methods but methods are a subclass of functions. Methods are associated with an object and are therefore only for object oriented programming
    // Start is called once before the first frame update (create)
    void Start()
    {
        health = 100;
        //anything with brackets after it is a function
    }

    // Update is called once per frame (step)
    void Update()
    {
        if (health < 100)
        {
            
        }
    }

    void Jump()
    {

        //jumps the character
    }
    
}
