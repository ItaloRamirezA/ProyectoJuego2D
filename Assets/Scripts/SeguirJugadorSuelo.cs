
using UnityEngine;

public class SeguirJugadorSuelo : MonoBehaviour
{
    public float radioBusqueda;
    public LayerMask capaJugador;
    Transform transformJugador;
    public float velocidadMovimiento;
    public float distanciaMaxima;

    public Vector3 puntoInicial;

    public bool mirandoDerecha;

    public Rigidbody2D rb;
    public Animator animator;
    public PlayerScript playerScript;
    
    public EstadosMovimiento estadoActual;
    public enum EstadosMovimiento {Esperando, Siguiendo, Volviendo};

    void Start()
    {  
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        puntoInicial = transform.position;
    }

    void Update()
    {
        estados();
    }

    void estados() {
        switch (estadoActual) {
                    case EstadosMovimiento.Esperando:
                        estadoEsperando();
                        break;
                    case EstadosMovimiento.Siguiendo:
                        if (playerScript.haMuerto) {
                            estadoVolviendo();
                        } else {
                            estadoSiguiendo();
                        }
                        break;
                    case EstadosMovimiento.Volviendo:
                        estadoVolviendo();
                        break;
                }
    }

    void estadoEsperando() {
        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);

        if (jugadorCollider) {
            transformJugador = jugadorCollider.transform;

            estadoActual = EstadosMovimiento.Siguiendo;
        }
    }

    void estadoSiguiendo() {
        animator.SetBool("corriendo", true);

        if (transformJugador == null) {
            estadoActual = EstadosMovimiento.Volviendo;
            return;
        }
        
        if (transform.position.x < transformJugador.position.x) {
            rb.velocity = new Vector2(velocidadMovimiento, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(-velocidadMovimiento, rb.velocity.y);
        }

        girarAObjetivo(transformJugador.position);

        if (Vector2.Distance(transform.position, puntoInicial) > distanciaMaxima ||
            Vector2.Distance(transform.position, transformJugador.position) > distanciaMaxima) {
            estadoActual = EstadosMovimiento.Volviendo;
            transformJugador = null;
        }
    }

    void estadoVolviendo() {
        if (transform.position.x < puntoInicial.x) {
            rb.velocity = new Vector2(velocidadMovimiento, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(-velocidadMovimiento, rb.velocity.y);
        }
        
        girarAObjetivo(puntoInicial);

        if (Vector2.Distance(transform.position, puntoInicial) < 0.2f) {
            rb.velocity = Vector2.zero;

            animator.SetBool("corriendo", false);
            
            estadoActual = EstadosMovimiento.Esperando;
        }
    }

    private void girarAObjetivo(Vector3 objetivo)
    {
        if (objetivo.x > transform.position.x && !mirandoDerecha)
        {
            girar();
        }
        else if (objetivo.x < transform.position.x && mirandoDerecha)
        {
            girar();
        }
    }

    private void girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }


    // -------------------------- GIZMOS INICIO --------------------------
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        Gizmos.DrawWireSphere(puntoInicial, distanciaMaxima);
    }
    // -------------------------- GIZMOS FINAL --------------------------
}
