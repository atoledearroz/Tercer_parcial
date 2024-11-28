using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casitas : MonoBehaviour
{
    public GameObject casitaIxtab;

    private void OnEnable()
    {
        casitaIxtab.SetActive(true);
    }

    private void OnDisable()
    {
        casitaIxtab.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enabled = true;
            FindObjectOfType<GameManager>().HomeOccupied1();



        }
    }
}
