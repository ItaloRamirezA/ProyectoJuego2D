using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuInicial : MonoBehaviour
{
    void Start()
    {
        
    }

    public void jugar() {
        SceneManager.LoadScene("Nivel1");
    }

    public void salir() {
        Console.Write("SALIR DEL PROGRAMA");
        Application.Quit();
    }
}
