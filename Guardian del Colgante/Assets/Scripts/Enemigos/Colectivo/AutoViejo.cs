using UnityEngine;

public class AutoViejo : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocidadBusqueda = 5f;
    public float velocidadMax = 15f;
    public float aceleracionRetroceso = 5f;
    public float distanciaHastaComenzarAtaque = 10.0f;
    public float duracionRetroceso = 1.0f;
    public float fuerzaRetroceso = 5f;
    public Animator animator;
    public float tiempoCarga = 2.0f;

    protected GameObject materialObjetivo;
    [HideInInspector] public string estado = "busqueda";

    private float retrocesoTimer = 0.0f;
    private float cargaTimer = 0.0f;
    protected Vector2 direccion = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        materialObjetivo = GameObject.FindGameObjectWithTag("MaterialObjetivo");
    }

    // Update is called once per frame
    void Update()
    {
        if (materialObjetivo == null) return;

        switch (estado)
        {
            case "busqueda":
                ActualizarDireccion();
                rb.linearVelocity = direccion * velocidadBusqueda;

                animator.SetBool("atacando", true);

                if (Vector2.Distance(transform.position, materialObjetivo.transform.position) <= distanciaHastaComenzarAtaque)
                {
                    estado = "ataque";
                }
                break;

            case "ataque":
                ActualizarDireccion();
                rb.linearVelocity = direccion * velocidadMax;
                animator.SetBool("atacando", true);
                break;

            case "retroceso":
                retrocesoTimer += Time.deltaTime;
                rb.linearVelocity = -direccion * fuerzaRetroceso;
                animator.SetBool("enRetroceso", true);
                if (retrocesoTimer >= duracionRetroceso)
                {
                    estado = "cargando";
                    retrocesoTimer = 0f;
                    cargaTimer = 0f;
                }
                break;

            case "cargando":
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("enRetroceso", false);
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
        direccion = (materialObjetivo.transform.position - transform.position).normalized;
        if (direccion.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }
    public void AplicarKnockback()
    {
        estado = "retroceso";
        retrocesoTimer = 0;
    }
}
