using System;
using UnityEngine;

public class EnemigoMoveScript : MonoBehaviour
{
    // Distancia que se mueve en cada direccion
    public float distanciaMovimiento = 0.5f;
    // Velocidad de movimiento
    public float velocidad = 0.1f;
    // Tiempo que se queda quieto al llegar a cada extremo
    public float tiempoQuieto = 2.5f;
    // Fuerza del empuje hacia atras
    public float knockbackForce = 5f;
    // Duracion del knockback
    public float knockbackDuration = 0.5f;

    private Vector2 posicionInicial;
    // Contola la direccion del movimiento
    private bool moviendoIzquierda;
    // Temporizador de espera
    private float tiempoEsperaActual;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        posicionInicial = transform.position;
        // Empieza quieto
        tiempoEsperaActual = tiempoQuieto;
        // Empezara moviendose a la izquierda
        moviendoIzquierda = true;
    }

    void Update()
    {
        gestionarGiro();
        movimientoJugador();
    }

    void movimientoJugador() {
        // Si esta esperando, avanza el temporizador
        if (tiempoEsperaActual > 0) {
            tiempoEsperaActual -= Time.deltaTime;
            //Sale hasta que termine el tiempo de espera
            return;
        }

        // Define la posicion de destino en funcion de la direccion
        Vector2 destino = moviendoIzquierda
            ? posicionInicial + Vector2.left * distanciaMovimiento
            : posicionInicial + Vector2.right * distanciaMovimiento;

        // Mueve el enemigo hacia el destino
        transform.position = Vector2.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

        // Si el enemigo ha alcanzado el destino, cambia de direccion y empieza el tiempo de espera
        if (Vector2.Distance(transform.position, destino) < 0.01f) {
            // Cambia la direccion
            moviendoIzquierda = !moviendoIzquierda;
            // Reinicia el temporizador de espera
            tiempoEsperaActual = tiempoQuieto;
        }
    }

    void gestionarGiro() {
        // Si se mueve
        if (tiempoEsperaActual <= 0) {
            animator.SetBool("corriendo", true);
            if (moviendoIzquierda) {
                transform.localScale = new Vector3(-1, 1, 1);
            } else {
                transform.localScale = new Vector3(1, 1, 1);
            }
        } else {
            animator.SetBool("corriendo", false);
        }
        // Si esta quieto lo dejo como este
    }
}
