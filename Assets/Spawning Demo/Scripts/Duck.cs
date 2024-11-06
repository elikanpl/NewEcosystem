using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    //private GameObject score;

    // Start is called before the first frame update
    private DuckManager duckManager;
    private AudioSource source;
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        //score = GameObject.Find("Score");
        //score.GetComponent<TMPro.TextMeshProUGUI>().text = "1";
        //finds the duckmanager without having to use the find 
        duckManager = DuckManager.reference;//GameObject.Find("DuckManager").GetComponent<DuckManager>();
        //spaces out the Quacks. Function that gets called
        //Invoke("Quack", 5);
        //first number is time before first invoked, second number is the time between quacks
        InvokeRepeating("Quack", 1, 3);
    }

    // Update is called once per frame
    private void Quack()
    {
        source.Play();
        //can use this directly without the reference above. If it does need a reference, the duckmanager below would be lowercase
        DuckManager.reference.IncrementScore();
           //GameObject.
    }
}
