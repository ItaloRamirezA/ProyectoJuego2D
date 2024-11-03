using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    public AudioClip damage;

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            ControladorSonidos.Instance.ejecutarSonido(damage);
            other.gameObject.GetComponent<PlayerScript>().tomarDano(1, other.GetContact(0).normal);
        }
    }
}
