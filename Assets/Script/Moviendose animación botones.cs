using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moviendoseanimaciónbotones : MonoBehaviour
{
    public Animator Animador;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cambiarAnimaciones();
    }

    public void cambiarAnimaciones()
    {
        Animador.SetTrigger("Derecha");
    }
    
}
