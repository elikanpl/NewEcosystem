using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SokobanWall : MonoBehaviour
{
    private GridObject myGridObjScript;
    private int myGridX;
    private int myGridY;
    public bool active;
    private int originalGridX;
    private int originalGridY;

    // Start is called before the first frame update
    void Start()
    {
        myGridObjScript = this.GetComponent<GridObject>();
        myGridX = myGridObjScript.gridPosition.x;
        myGridY = myGridObjScript.gridPosition.y;
        active = false;
        originalGridX = myGridX;
        originalGridY = myGridY;
    }

    // Update is called once per frame
    void Update()
    {
        active = false;
        myGridX = originalGridX;
        myGridY = originalGridY;
    }
}
