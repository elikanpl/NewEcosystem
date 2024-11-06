using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighFiveGoal : MonoBehaviour
{
    public GameObject victoryStar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerhand"))
        {
            StartCoroutine("DoCelebration");
        }
    }

    private IEnumerator DoCelebration()
    {
        //stop simulating physics
        Physics2D.simulationMode = SimulationMode2D.Script;

        //show the victory star
        victoryStar.SetActive(true);

        //wait for a second
        yield return new WaitForSeconds(1f);

        //hide it
        victoryStar.SetActive(false);

        //resume simulating physics
        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
    }
}
