using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject botonPausa;
    public GameObject menuPausa;
    private bool juegoPausado = false;

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

    public void salir() {
        juegoPausado = false;
        Application.Quit();
    }
}
