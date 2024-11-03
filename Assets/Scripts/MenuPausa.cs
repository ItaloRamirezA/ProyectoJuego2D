using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuPausa : MonoBehaviour
{
    public GameObject botonPausa;
    public GameObject menuPausa;
    private bool juegoPausado = false;

    private GameObject menu;
    private PlayerScript playerScript;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (juegoPausado) {
                reanudar();
            } else {
                pausa();
            }
        }
    }

    public void pausa() {
        juegoPausado = true;
        Time.timeScale = 0;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void reanudar() {
        juegoPausado = false;
        Time.timeScale = 1;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void reiniciar() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void salir() {
        juegoPausado = false;
        Application.Quit();
    }
}
