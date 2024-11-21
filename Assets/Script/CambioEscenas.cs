using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscenas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void instrucciones()
    {
        SceneManager.LoadScene(1);
    }

    public void menu()
    {
        SceneManager.LoadScene(0);
    }

    public void primeraescena()
    {
        SceneManager.LoadScene(2);
    }

    public void nivel1()
    {
        SceneManager.LoadScene(3);
    }

    public void nivel2()
    {
        SceneManager.LoadScene(4);
    }

    public void nivel3()
    {
        SceneManager.LoadScene(5);
    }

    public void escenafinal()
    {
        SceneManager.LoadScene(6);
    }

    public void salir()
    {
        Application.Quit();
    }

    public void conejonyemuere()
    {
        SceneManager.LoadScene(7);
    }

}
