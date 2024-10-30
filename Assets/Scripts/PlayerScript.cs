using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Varibales para las velocidades del jugador
    public float velocidadHorizontal = 0.5f;
    public float velocidadVertical = 0.5f;
    public float jumpForce = 2f;

    // Definir el layer para el suelo
    public LayerMask Suelo;

    // Longitud del rayCast
    public float rayLength = 0.1f;
    Rigidbody2D rb;
    void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        float inputMovimientoHorizontal = Input.GetAxis("Horizontal");
        float inputMovimientoVertical = Input.GetAxis("Vertical");

        movimientoLateral(inputMovimientoHorizontal);
        salto();
    }

    void movimientoLateral(float movHorizontal) {
        rb.velocity = new Vector2(movHorizontal * velocidadHorizontal, rb.velocity.y);
    }

    void salto() {
        // Posición desde donde lanzo el raycast hacia abajo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, Suelo);
        
        // Si el raycast golpea algo en el layer del suelo, permite el salto
        if (hit.collider != null && Input.GetAxis("Jump") != 0) { // 1 cuando se presiona y 0 cuando no
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }


    //Metodo para visualizar el Raycast en la escena (opcional)
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
    }
}
