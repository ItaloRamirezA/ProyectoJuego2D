
using UnityEngine;

public class SeguirJugadorVolador : MonoBehaviour
{
    public float radioBusqueda;
    public LayerMask capaJugador;
    Transform transformJugador;
    public float velocidadMovimiento;
    public float distanciaMaxima;
    public Vector3 puntoInicial;
    public bool mirandoDerecha;
    public PlayerScript playerScript;
    public EstadosMovimiento estadoActual;
    public enum EstadosMovimiento {Esperando, Siguiendo, Volviendo};

    void Start()
    {   
        // Punto inicial es la posicion del enemigo
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
        if (transformJugador == null) {
            estadoActual = EstadosMovimiento.Volviendo;
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, transformJugador.position, velocidadMovimiento * Time.deltaTime);

        girarAObjetivo(transformJugador.position);

        if (Vector2.Distance(transform.position, puntoInicial) > distanciaMaxima ||
            Vector2.Distance(transform.position, transformJugador.position) > distanciaMaxima) {
            estadoActual = EstadosMovimiento.Volviendo;
            transformJugador = null;
        }
    }

    void estadoVolviendo() {
        transform.position = Vector2.MoveTowards(transform.position, puntoInicial, velocidadMovimiento * Time.deltaTime);
        
        girarAObjetivo(puntoInicial);

        if (Vector2.Distance(transform.position, puntoInicial) < 0.01f) {
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