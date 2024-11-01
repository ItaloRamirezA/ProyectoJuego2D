using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    public float vida;
    private PlayerScript movimientoJugador;
    public float tiempoPerdidaControl;
    private Animator animator;

    void Start()
    {
        movimientoJugador = GetComponent<PlayerScript>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void tomarDano(float dano) {
        vida -= dano;
    }

    public void tomarDano(float dano, Vector2 posicion) {
        vida -= dano;
        animator.SetTrigger("Golpe");
        StartCoroutine(perderControl());
        movimientoJugador.rebote(posicion);
    }

    private IEnumerator perderControl() {
        movimientoJugador.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        movimientoJugador.sePuedeMover = true;
    }
}
