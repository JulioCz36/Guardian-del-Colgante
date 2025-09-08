using UnityEngine;

public class AutoViejo : MonoBehaviour
{

    // El dano es el que le va a infligir al material objetivo
    protected Rigidbody2D rb;
    public float danoAEstructura = 1.0f;
    public int danoAJugador = 15;

    public float velocidadBusqueda = 1.0f;
    public float aceleracionAdelante = 1.0f;
    public float aceleracionBusqueda = 1.0f;
    public float velocidadMaxBusqueda = 10.0f;
    public float velocidadMax = 15.0f;
    public float aceleracionRetroceso = 0.8f;
    public float distanciaHastaComenzarAtaque = 10.0f;
    public float duracionRetroceso = 1.0f;

    private float offset = -1.0f;

    protected GameObject materialObjetivo;
    protected GameObject materialBodyCollider;
    protected GameObject player;

    protected string estado = "busqueda";

    private float retrocesoTimer = 0.0f;

    protected Vector2 direccion = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        materialObjetivo = GameObject.FindGameObjectWithTag("MaterialObjetivo");
        materialBodyCollider = GameObject.FindGameObjectWithTag("MaterialObjetivoCollider");
        player = GameObject.FindGameObjectWithTag("Player");

        // materialBodyCollider = materialObjetivo.transform.Find("Body Collider").gameObject;

        this.transform.position = new Vector3(this.transform.position.x, materialBodyCollider.transform.position.y + offset);


        ActualizarDireccion();
    }

    // Update is called once per frame
    void Update()
    {
        if (materialObjetivo == null) return;
        if (estado == "busqueda")
        {
            if (rb.linearVelocity.magnitude <= velocidadMaxBusqueda) {
                rb.linearVelocity = rb.linearVelocity + (direccion * aceleracionBusqueda * Time.deltaTime);
            }
            if (Vector2.Distance(this.transform.position, materialObjetivo.transform.position) <= distanciaHastaComenzarAtaque) {
                estado = "ataque";
            }
        }

        if (estado == "ataque")
        {
            if (rb.linearVelocity.magnitude <= velocidadMax)
            {
                rb.linearVelocity = rb.linearVelocity + (direccion * aceleracionAdelante * Time.deltaTime);
            }
            
        }
        if (estado == "retroceso") {

            retrocesoTimer += Time.deltaTime;

            if (retrocesoTimer >= duracionRetroceso) {
                this.estado = "busqueda";
                retrocesoTimer = 0;
            }

            if (rb.linearVelocity.magnitude <= velocidadMax)
            {
                rb.linearVelocity = rb.linearVelocity - (direccion * aceleracionRetroceso * Time.deltaTime);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("MaterialObjetivoPadre"))
        {
            // Sinceramente no se si es la mejor manera de resolver esto.

            // Básicamente agarro el script MaterialObjetivo del MaterialObjetivo y ejecuto la función InfligirDano desde acá.
            collision.gameObject.GetComponent<MaterialObjetivo>().InfligirDano(this.danoAEstructura);


            this.estado = "retroceso";
        }
        if (collision.gameObject.CompareTag("Player"))
        {

            if (this.estado != "retroceso")
            {
                collision.gameObject.GetComponent<Jugador>().ModificarVida(-this.danoAJugador);
                //this.estado = "retroceso";
            }
            
        }
    }

    protected void ActualizarDireccion()
    {
        if (materialBodyCollider.transform.position.x > transform.position.x)
        {
            direccion = Vector2.right;
        }
        else {
            direccion = Vector2.left;
        }
    }
}
