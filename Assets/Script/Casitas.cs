using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casitas : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enabled = true;
            FindObjectOfType<GameManager>().HomeOccupied1();
        }
    }
}
