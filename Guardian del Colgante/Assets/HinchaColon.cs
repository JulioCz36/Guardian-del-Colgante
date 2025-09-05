using UnityEngine;

public class HinchaColon : MonoBehaviour
{

    // El dano es el que le va a infligir al material objetivo
    protected Rigidbody2D rb;

    public float distanciaHastaEmpezarAAtacar = 15.0f;

    public float cadenciaDeTiroDeCascotes = 1.0f;

    public float tiempoTiro = 1.0f;

    public float gravedad = 5.0f;

    public float offsetPosible = 1.0f;

    public float danoAEstructura = 1.0f;
    public int danoAJugador = 15;
    public float speed = 5.0f;

    protected float timerCascote = 0.0f;

    public GameObject cascote;

    protected GameObject materialObjetivo;
    protected GameObject player;

    protected string estado = "busqueda";
    protected float dist = 0.0f;


    protected Vector2 direccion = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        materialObjetivo = GameObject.FindGameObjectWithTag("MaterialObjetivo");
        player = GameObject.FindGameObjectWithTag("Player");
        //timerCascote = cadenciaDeTiroDeCascotes;
        ActualizarDireccion();
    }

    // Update is called once per frame
    void Update()
    {
        if (materialObjetivo == null) {
            return;
        }
        // Máquina de estados.
        if (estado == "ataque")
        {
            if (timerCascote >= cadenciaDeTiroDeCascotes) {
                GameObject casc = Instantiate(cascote);
                Cascote cascScript = casc.GetComponent<Cascote>();
                Vector2 offset = new Vector2(Random.Range(-offsetPosible, offsetPosible), Random.Range(-offsetPosible, offsetPosible));
                Vector2 posicionFinal = new Vector2(this.materialObjetivo.transform.position.x, this.materialObjetivo.transform.position.y) + offset;

                cascScript.Init(this.transform.position, posicionFinal, -gravedad, tiempoTiro, this.danoAEstructura, this.danoAJugador);
                timerCascote = 0.0f;
            }

            timerCascote += Time.deltaTime;
            
            rb.linearVelocity *= 0.99f;
           
        }

        if (estado == "busqueda")
        {
            //busquedaTimer += Time.deltaTime;
            rb.linearVelocity = direccion * speed;
            if (Vector2.Distance(materialObjetivo.transform.position, this.transform.position) <= distanciaHastaEmpezarAAtacar)
            {
                estado = "ataque";
            }
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("MaterialObjetivo"))
        {
            // Sinceramente no se si es la mejor manera de resolver esto.

            // Básicamente agarro el script MaterialObjetivo del MaterialObjetivo y ejecuto la función InfligirDano desde acá.
            collision.gameObject.GetComponent<MaterialObjetivo>().InfligirDano(this.danoAEstructura);
            this.estado = "escape";
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("GOLPEANDO A JUGADOR!");
            collision.gameObject.GetComponent<Jugador>().ModificarVida(-this.danoAJugador);
        }
    }

    protected void ActualizarDireccion()
    {
        direccion = (materialObjetivo.transform.position - transform.position).normalized;
    }
}
