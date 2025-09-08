using UnityEngine;

public class PalomaVuelo : MonoBehaviour
{
    [Header("Configuracion")]

    [Header("Movimiento hacia el objetivo")]
    public float velocidadX = -3.5f;
    public float waveAmplitude = 0.3f;
    public float waveFrequency = 2f;

    [Header("Vuelo en círculos")]
    public float radioX = 1f;         
    public float radioY = 0.5f;
    public float velocidadCircular = 2f;

    [Header("Zona de Ataque")]
    public Transform zonaPaloma;


    private float yInicial;
    private Rigidbody2D rb;

    public bool enZonaAtaque = false;
    private Vector2 centroVuelo;
    private float tiempoCircular;
    private bool vieneDesdeIzquierda;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        yInicial = transform.position.y;
        if (zonaPaloma != null)
        {
            velocidadX = (zonaPaloma.position.x > transform.position.x) ? Mathf.Abs(velocidadX) : -Mathf.Abs(velocidadX);
        }
        vieneDesdeIzquierda = zonaPaloma.position.x > transform.position.x;
        if (vieneDesdeIzquierda)
            transform.localScale = new Vector3(-.15f, .15f, 1); 
        else
            transform.localScale = new Vector3(.15f, .15f, 1);
    }

    void FixedUpdate()
    {



        if (!enZonaAtaque)
        {
            float movimientoX = velocidadX;

            float movimientoY = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;

            rb.MovePosition(new Vector2(transform.position.x + movimientoX * Time.fixedDeltaTime, yInicial + movimientoY));
        }
        else
        {

            // volando alrededor del centro de vuelo
            tiempoCircular += Time.fixedDeltaTime * velocidadCircular;

            float x, y;

            if (vieneDesdeIzquierda)
            {
                x = centroVuelo.x - Mathf.Cos(tiempoCircular) * radioX;
                y = centroVuelo.y + Mathf.Sin(tiempoCircular) * radioY;
            }
            else // viene desde derecha
            {
                x = centroVuelo.x + Mathf.Cos(tiempoCircular) * radioX;
                y = centroVuelo.y + Mathf.Sin(tiempoCircular) * radioY;
            }

            rb.MovePosition(new Vector2(x, y));

            if (x < centroVuelo.x)
                transform.localScale = new Vector3(-.15f, .15f, 1); // derecha
            else
                transform.localScale = new Vector3(.15f, .15f, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ZonaPaloma"))
        {
            enZonaAtaque = true;
            centroVuelo = collision.bounds.center;
            tiempoCircular = 0f;
           rb.gravityScale = 0f;
        }
    }
}
