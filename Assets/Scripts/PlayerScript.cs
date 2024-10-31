using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Variables para las velocidades del jugador
    public float velocidadHorizontal = 0.5f;
    public float jumpForce = 2f;

    // Definir el layer para el suelo
    public LayerMask Suelo;

    // Longitud del raycast
    public float rayLength = 0.1f;
    Rigidbody2D rb;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private bool estaEnSuelo;

    void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {   
        float inputMovimientoHorizontal = Input.GetAxis("Horizontal");

        // Verifica si el personaje esta en el suelo
        verificarSuelo();
        
        // MOVIMIENTO
        movimientoLateral(inputMovimientoHorizontal);
        salto();
        gestionarGiro(inputMovimientoHorizontal);
    }

    void verificarSuelo()
    {
        // Posición desde donde lanzo el raycast hacia abajo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, Suelo);
        // Si el hit.collider es null, no detecta nada por ende no esta en el suelo
        estaEnSuelo = hit.collider != null;
    }

    // -------------------------- MOVIMIENTO INICIO --------------------------
    void movimientoLateral(float movHorizontal) {
        rb.velocity = new Vector2(movHorizontal * velocidadHorizontal, rb.velocity.y);
        
        // Activa la animación de correr solo si el personaje está en el suelo
        if (movHorizontal != 0 && estaEnSuelo) { 
            animator.SetBool("corriendo", true);
        } else {
            animator.SetBool("corriendo", false);
        }
    }

    void salto() {
        // Si el personaje está en el suelo y se presiona el botón de salto
        if (estaEnSuelo && Input.GetButtonDown("Jump")) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Activa la animación de salto si el personaje no está en el suelo
        animator.SetBool("saltando", !estaEnSuelo);
    }

    void gestionarGiro(float inputMovimiento) {
        if (inputMovimiento > 0) {
            transform.localScale = new Vector3(1, 1, 1); // Escala normal en X
        } else if (inputMovimiento < 0) {
            transform.localScale = new Vector3(-1, 1, 1); // Invertir la escala en X
        }
    }
    // -------------------------- MOVIMIENTO FINAL --------------------------

    // -------------------------- GIZMOS INICIO --------------------------
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
    }
    // -------------------------- GIZMOS FINAL --------------------------
}
