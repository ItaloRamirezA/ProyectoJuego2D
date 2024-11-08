using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaScript : MonoBehaviour
{
    private float cantidadPuntos;
    public PuntajeScript puntaje;
    public AudioClip recoger;
    void Start()
    {
        cantidadPuntos = 1;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            ControladorSonidos.Instance.ejecutarSonido(recoger);
            puntaje.sumarPuntos(cantidadPuntos);

            Destroy(gameObject);
        }
    }
}
