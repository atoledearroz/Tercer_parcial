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
        Derecha();
        Izquierda();
        Arriba();
        Abajo();
    }

    public void Derecha()
    {
        Animador.SetTrigger("Derecha");
    }

    public void Izquierda()
    {
        Animador.SetTrigger("Izquierda");
    }

    public void Arriba()
    {
        Animador.SetTrigger("Frente");
    }
    
    public void Abajo()
    {
        Animador.SetTrigger("Frente");
    }
}
