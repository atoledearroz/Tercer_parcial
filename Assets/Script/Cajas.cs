using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cajas : MonoBehaviour
{
    public GameObject home;

    private void OnEnable()
    {
        home.SetActive(true);
    }

    private void OnDisable()
    {
        home.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enabled = true;

            FindObjectOfType<GameManager>().HomeOccupied1();
        }
    }

        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
