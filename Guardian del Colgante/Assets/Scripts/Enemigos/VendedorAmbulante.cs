using UnityEngine;

public class VendedorAmbulante : HinchaColon
{

    [Header("Configuración")]
    public float distanciaMinima = 7.0f;

    private GameObject player;

    void Start()
    {
        estado = "mover";
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (ataque != null)
            ataque.SetObjetivo(player);

        timerCascote = 0f;
    }

    void Update()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(player.transform.position, transform.position);

        if (estado == "mover") {
            if (distancia > distanciaMinima)
            {
                estado = "mover";
                direccion = (player.transform.position - transform.position).normalized;
                rb.linearVelocity = direccion * speed;
            }
            else if (distancia < distanciaMinima - 1f)
            {
                estado = "mover";
                direccion = (transform.position - player.transform.position).normalized;
                rb.linearVelocity = direccion * (speed * 0.5f);
            }
        
            else {
                rb.linearVelocity = Vector2.zero;
                estado = "ataque";
    
                if (ataque != null)
                {
                    ataque.Lanzar();
                }
                timerCascote = 0f;
            } 
        }
        else if (estado == "ataque")
        {
            rb.linearVelocity *= 0.98f;

            timerCascote += Time.deltaTime;
            if (timerCascote >= cadenciaDeTiroDeCascotes)
            {
                if (ataque != null)
                    ataque.Lanzar();
                timerCascote = 0f;
            }

            if (distancia > distanciaMinima || distancia < distanciaMinima - 1f)
            {
                estado = "mover";
            }
        }


        Vector2 direccionMirar = (player.transform.position - transform.position).normalized;
        if (direccionMirar.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (direccionMirar.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Jugador>().ModificarVida(-danoAJugador);
        }
    }
}
