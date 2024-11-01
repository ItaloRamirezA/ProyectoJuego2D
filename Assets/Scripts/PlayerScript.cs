using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Variables para las velocidades del jugador
    public float velocidadHorizontal = 0.5f;
    public float jumpForce = 2f;

    // Definir los layers para el suelo y las paredes
    public LayerMask Suelo;
    public LayerMask Paredes;

    // Longitud de los raycast
    public float rayLength = 0.1f;
    public float wallRayLength = 0.1f;
    
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

        // Verifica si el personaje está en el suelo
        verificarSuelo();
        
        // MOVIMIENTO
        movimientoLateral(inputMovimientoHorizontal);
        salto();
        gestionarGiro(inputMovimientoHorizontal);
    }

    void verificarSuelo() {
        // Posición desde donde lanzo el raycast hacia abajo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, Suelo);
        // Si el hit.collider es null, no detecta nada, por ende no está en el suelo
        estaEnSuelo = hit.collider != null;
    }

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

        // También verifica si puede saltar de una pared
        if (!estaEnSuelo && Input.GetButtonDown("Jump") && puedeSaltarDePared()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Activa la animación de salto si el personaje no está en el suelo
        animator.SetBool("saltando", !estaEnSuelo);
    }

    bool puedeSaltarDePared()
    {
        // Raycast a la izquierda y a la derecha para verificar si hay una pared
        RaycastHit2D hitIzquierda = Physics2D.Raycast(transform.position, Vector2.left, wallRayLength, Paredes);
        RaycastHit2D hitDerecha = Physics2D.Raycast(transform.position, Vector2.right, wallRayLength, Paredes);

        // Si detecta una colisión en la izquierda o en la derecha, retorna true
        return hitIzquierda.collider != null || hitDerecha.collider != null;
    }

    void gestionarGiro(float inputMovimiento) {
        if (inputMovimiento > 0) {
            transform.localScale = new Vector3(1, 1, 1); // Escala normal en X
        } else if (inputMovimiento < 0) {
            transform.localScale = new Vector3(-1, 1, 1); // Invertir la escala en X
        }
    }

    // -------------------------- GIZMOS INICIO -------------------------- 
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * wallRayLength); // Gizmo para el raycast de la izquierda
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallRayLength); // Gizmo para el raycast de la derecha
    }
    // -------------------------- GIZMOS FINAL -------------------------- 
}
