using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuInicial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void jugar() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void salir() {
        Console.Write("SALIR DEL PROGRAMA");
        Application.Quit();
    }
}
