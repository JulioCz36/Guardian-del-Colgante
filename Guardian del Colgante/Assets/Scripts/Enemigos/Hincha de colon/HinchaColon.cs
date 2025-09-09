using UnityEngine;

public class HinchaColon : MonoBehaviour
{

    // El dano es el que le va a infligir al material objetivo
    protected Rigidbody2D rb;

    [Header("Configuración")]
    public float distanciaHastaEmpezarAAtacar = 15.0f;
    public float cadenciaDeTiroDeCascotes = 1.0f;
    public float speed = 5.0f;

    [Header("Daño")]
    public int danoAJugador = 15;

    protected GameObject materialObjetivo;
    protected string estado = "busqueda";
    protected Vector2 direccion = Vector2.zero;

    protected float timerCascote = 0f;
    public HinchaColonAtaque ataque;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        materialObjetivo = GameObject.FindGameObjectWithTag("MaterialObjetivo");
      
        if (ataque != null)
            ataque.SetObjetivo(materialObjetivo);
        ActualizarDireccion();
    }

    void Update()
    {
        if (materialObjetivo == null) return;

        // Máquina de estados.
        if (estado == "ataque")
        {
            if (timerCascote >= cadenciaDeTiroDeCascotes)
            {
                ataque.Lanzar();
                timerCascote = 0f;
            }

            timerCascote += Time.deltaTime;
            rb.linearVelocity *= 0.98f;
        }

        if (estado == "busqueda")
        {
            rb.linearVelocity = direccion * speed;
            if (Vector2.Distance(materialObjetivo.transform.position, this.transform.position) <= distanciaHastaEmpezarAAtacar)
            {
                estado = "ataque";
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Jugador>().ModificarVida(-danoAJugador);
        }
    }
    protected void ActualizarDireccion()
    {
        direccion = (materialObjetivo.transform.position - transform.position).normalized;
        if (direccion.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); 
        }
        else if (direccion.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }
    }
}
