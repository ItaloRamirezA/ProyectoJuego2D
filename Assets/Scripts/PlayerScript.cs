using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Varibales para la velocidad del jugador
    public float velocidadHorizontal = 0.01f;
    public float velocidadVertical = 0.01f;
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
    }

    void movimientoLateral(float movHorizontal) {
        rb.velocity = new Vector2(movHorizontal * velocidadHorizontal, rb.velocity.y);
    }
}
