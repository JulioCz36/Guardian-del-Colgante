using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;



public class Ladron : MonoBehaviour
{
    
    // El dano es el que le va a infligir al material objetivo
    Rigidbody2D rb;
    public float dano = 1.0f;
    public float speed = 5.0f;
    public float tiempoFreno = 1f;
    public float tiempoBusqueda = 1f;
    public float velocidadDeEscape = 7.0f;
    

    private GameObject materialObjetivo;
    private GameObject player;

    private string estado = "busqueda";
    private float dist = 0.0f;
    private float rAngle = 0.0f;
    private float frenadoTimer = 0.0f;
    private float busquedaTimer = 0.0f;

    private Vector2 direccion = Vector2.zero;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("MaterialObjetivo")) {
            // Sinceramente no se si es la mejor manera de resolver esto.

            // Básicamente agarro el script MaterialObjetivo del MaterialObjetivo y ejecuto la función InfligirDano desde acá.
            collision.gameObject.GetComponent<MaterialObjetivo>().InfligirDano(this.dano);
            this.estado = "escape";
        }
    }
    // Boludeces para que apunte para un lado mas o menos aleatorio.
    private float sigmoid(float x)
    {
        float factor = 15.0f; // Para retocar.
        return 1 / (1 + Mathf.Exp(-x + factor));
    }
    private float randomAngle(float distance)
    {
        float maxAngle = (Mathf.PI / 4);
        //Debug.Log("Sigmoide de la distancia: " + sigmoid(distance));
        return sigmoid(distance) * Random.Range(-maxAngle, maxAngle);
    }

    private Vector2 rotatedVector(Vector2 v, float angle)
    {
        float c = Mathf.Cos(angle);
        float s = Mathf.Sin(angle);
        return new Vector2(c * v.x - s * v.y, s * v.x + c * v.y);
    }

    private void ActualizarDireccion() {
        dist = Vector2.Distance(materialObjetivo.transform.position, transform.position);
        rAngle = randomAngle(dist);
        direccion = rotatedVector((materialObjetivo.transform.position - transform.position).normalized, rAngle);
    }
}
