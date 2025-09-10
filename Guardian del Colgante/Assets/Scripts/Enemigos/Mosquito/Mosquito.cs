using UnityEngine;

public class Mosquito : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;

    [Header("Movimiento")]
    public float velocidadBusqueda = 3f;
    public float velocidadMax = 8f;

    [Header("Ataque")]
    public float distanciaHastaComenzarAtaque = 8f;
    public float duracionRetroceso = 1f;
    public float fuerzaRetroceso = 4f;
    public float tiempoCarga = 1.5f;
    public float tiempoMaximoDeAtaque = 2f;

    private GameObject jugador;
    private string estado = "busqueda";
    private Vector2 direccion = Vector2.zero;

    private float retrocesoTimer = 0f;
    private float cargaTimer = 0f;
    private float ataqueTimer = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (jugador == null) return;

        switch (estado)
        {
            case "busqueda":
                animator.SetBool("atacando", false);
                Vector2 objetivo = jugador.transform.position;
                float step = velocidadBusqueda * Time.deltaTime;
                Vector2 nuevaPos = Vector2.MoveTowards(rb.position, objetivo, step);
                rb.MovePosition(nuevaPos);

                // girar sprite según dirección
                if (objetivo.x > transform.position.x)
                    transform.localScale = new Vector3(-1, 1, 1);
                else
                    transform.localScale = new Vector3(1, 1, 1);

                if (Vector2.Distance(transform.position, jugador.transform.position) <= distanciaHastaComenzarAtaque)
                {
                    estado = "ataque";
                    ataqueTimer = 0f;
                    ActualizarDireccion();
                }
                break;

            case "ataque":
                animator.SetBool("atacando", true);
                rb.linearVelocity = direccion * velocidadMax;

                ataqueTimer += Time.deltaTime;

                if (ataqueTimer >= tiempoMaximoDeAtaque)
                {
                    estado = "retroceso";
                    retrocesoTimer = 0f;
                    rb.linearVelocity = Vector2.zero;
                }

                break;

            case "retroceso":
                animator.SetBool("atacando", false);
                retrocesoTimer += Time.deltaTime;
                rb.linearVelocity = -direccion * fuerzaRetroceso;

                if (retrocesoTimer >= duracionRetroceso)
                {
                    estado = "cargando";
                    retrocesoTimer = 0f;
                    rb.linearVelocity = Vector2.zero;
                    cargaTimer = 0f;
                }
                break;

            case "cargando":
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("cargando", true);
                cargaTimer += Time.deltaTime;
                if (cargaTimer >= tiempoCarga)
                {
                    estado = "busqueda";
                    cargaTimer = 0f;
                }
                break;
        }
    }

    private void ActualizarDireccion()
    {
        direccion = (jugador.transform.position - transform.position).normalized;
        if (direccion.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

    }

    public void AplicarKnockback()
    {
        estado = "retroceso";
        retrocesoTimer = 0;
    }
}
