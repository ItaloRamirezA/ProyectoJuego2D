using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CorazonesUI : MonoBehaviour
{
    public List<Image> listaCorazones;
    public GameObject corazonPrefab;
    public PlayerScript playerScript;
    public int indexActual;
    public Sprite corazonLLeno;
    public Sprite corazonVacio;

    private void Awake() {
        playerScript.cambioVida.AddListener(cambiarCorazones);
    }

    private void cambiarCorazones(int vidaActual)
    {
        if (!listaCorazones.Any()) {
            crearCorazones(vidaActual);
        } else {
            cambiarVida(vidaActual);
        }
    }

    private void crearCorazones(int vidaActual)
    {
        for (int i = 0; i < vidaActual; i++)
        {
            GameObject corazon = Instantiate(corazonPrefab, transform);

            listaCorazones.Add(corazon.GetComponent<Image>());
        }

        indexActual = vidaActual - 1;
    }

    private void cambiarVida(int vidaActual)
    {
        for (int i = indexActual; i >= vidaActual; i--)
        {
            indexActual = i;
            listaCorazones[indexActual].sprite = corazonVacio;
        }
    }

}
