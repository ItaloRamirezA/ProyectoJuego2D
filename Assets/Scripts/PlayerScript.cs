using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{   
    Transform transform;

    // Varibales para la velocidad del jugador
    public float velocidadLateral = 0.001f;
    public float velocidadVertical = 0.001f;
    Rigidbody rb2d;
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        movimiento();
    }

    void movimiento() {
        Vector2 position = transform.position;
        position.x = position.x + Input.GetAxis("Horizontal") * velocidadLateral;
        position.y = position.y + Input.GetAxis("Vertical") * velocidadVertical;
        transform.position = position;
    }
}
