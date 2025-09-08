using UnityEngine;

public class Ladron : MonoBehaviour
{
    
    // El dano es el que le va a infligir al material objetivo
    protected Rigidbody2D rb;
    public int danoAEstructura = 1;
    public int danoAJugador = 15;
    public float speed = 5.0f;
    public float tiempoFreno = 1f;
    public float tiempoBusqueda = 1f;
    public float velocidadDeEscape = 7.0f;
    

    protected GameObject materialObjetivo;
    protected GameObject player;

    protected string estado = "busqueda";
    protected float dist = 0.0f;
    protected float rAngle = 0.0f;
    protected float frenadoTimer = 0.0f;
    protected float busquedaTimer = 0.0f;

    protected Vector2 direccion = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        materialObjetivo = GameObject.FindGameObjectWithTag("MaterialObjetivo");
        player = GameObject.FindGameObjectWithTag("Player");

        ActualizarDireccion();
    }

    // Update is called once per frame
    void Update()
    {
        // Máquina de estados.
        if (estado == "frenado") { 
            frenadoTimer += Time.deltaTime;

            rb.linearVelocity *= 0.99f;
            if (frenadoTimer >= tiempoFreno) {
                ActualizarDireccion();
                frenadoTimer = 0.0f;
                estado = "busqueda";
            }
        }

        if (estado == "busqueda") {
            busquedaTimer += Time.deltaTime;
            rb.linearVelocity = rb.linearVelocity + (direccion * speed * Time.deltaTime);
            if (busquedaTimer >= tiempoBusqueda) {
                busquedaTimer = 0.0f;
                estado = "frenado";
            }
        }

        if (estado == "escape") {
            rb.linearVelocity = Vector2.right * velocidadDeEscape;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("MaterialObjetivoPadre")) {
            // Sinceramente no se si es la mejor manera de resolver esto.

            // Básicamente agarro el script MaterialObjetivo del MaterialObjetivo y ejecuto la función InfligirDano desde acá.
            collision.gameObject.GetComponent<MaterialObjetivo>().InfligirDano(this.danoAEstructura);
            if (materialObjetivo.transform.position.x - transform.position.x > 0) {
                velocidadDeEscape *= -1;
            }
            this.estado = "escape";
        }
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("GOLPEANDO A JUGADOR!");
            collision.gameObject.GetComponent<Jugador>().ModificarVida(-this.danoAJugador);
        }
    }
    // Boludeces para que apunte para un lado mas o menos aleatorio.
    protected float sigmoid(float x)
    {
        float factor = 15.0f; // Para retocar.
        return 1 / (1 + Mathf.Exp(-x + factor));
    }
    protected float randomAngle(float distance)
    {
        float maxAngle = (Mathf.PI / 4);
        //Debug.Log("Sigmoide de la distancia: " + sigmoid(distance));
        return sigmoid(distance) * Random.Range(-maxAngle, maxAngle);
    }

    protected Vector2 rotatedVector(Vector2 v, float angle)
    {
        float c = Mathf.Cos(angle);
        float s = Mathf.Sin(angle);
        return new Vector2(c * v.x - s * v.y, s * v.x + c * v.y);
    }

    protected void ActualizarDireccion()
    {
        dist = Vector2.Distance(materialObjetivo.transform.position, transform.position);
        rAngle = randomAngle(dist);
        direccion = rotatedVector((materialObjetivo.transform.position - transform.position).normalized, rAngle);
    }
}
