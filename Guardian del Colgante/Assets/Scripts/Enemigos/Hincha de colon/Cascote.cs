using UnityEngine;

public class Cascote : MonoBehaviour
{
    
    private Vector2 p1;
    private Vector2 p2;
    private Vector2 gravity;
    private float time_prime;

    private float danoAEstructura;
    private int danoAJugador;

    private float time = 0.0f;
    private float timeVivoCascote = 5.0f;
    private float timerCascoteVivo = 0.0f;
    private Vector2 v0;

    public void Init(Vector2 p1, Vector2 p2, float gravity, float time_prime, float danoAEstructura, int danoAJugador) {
        this.p1 = p1;
        this.p2 = p2;
        this.gravity = new Vector2(0f, -gravity);
        this.time_prime = time_prime;

        this.danoAEstructura = danoAEstructura;
        this.danoAJugador = danoAJugador;

        this.transform.position = p1;
        this.v0 = (p2 - p1 + this.gravity * (time_prime*time_prime)) * (1 / time_prime);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HurtboxJugador"))
        {
            collision.SendMessageUpwards("ModificarVida",-this.danoAJugador);
            Destroy(gameObject);
            return;
        }
        if (collision.gameObject.CompareTag("MaterialObjetivo"))
        {
            collision.SendMessageUpwards("InfligirDano", danoAEstructura);
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        if (this.time <= this.time_prime)
        {
            time += Time.deltaTime;
            this.transform.position = -gravity * (time * time) + this.v0 * time + this.p1;
        }
        else { 
            timerCascoteVivo += Time.deltaTime;
            if (timerCascoteVivo >= timeVivoCascote) { 
                Destroy(gameObject);
            }
        }

    }
}
