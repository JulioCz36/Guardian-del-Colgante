using System.Threading;
using UnityEngine;

public class Cascote : MonoBehaviour
{
    
    private Vector2 p1;
    private Vector2 p2;
    private Vector2 gravity;
    private float time_prime;

    private float danoAEstructura;
    int danoAJugador;

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
        Debug.Log(collision.gameObject);
        if (collision.gameObject.CompareTag("MaterialObjetivo"))
        {
            // Sinceramente no se si es la mejor manera de resolver esto.

            // Básicamente agarro el script MaterialObjetivo del MaterialObjetivo y ejecuto la función InfligirDano desde acá.
            collision.gameObject.GetComponent<MaterialObjetivo>().InfligirDano(this.danoAEstructura);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<Jugador>().ModificarVida(-this.danoAJugador);
            Destroy(gameObject);
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
