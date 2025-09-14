using UnityEngine;

public class ItemDamage : MonoBehaviour
{
    public float multiplicador = 2f;
    public float duracion = 10f;
    public float tiempoDeVida = 10f;

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HurtboxJugador"))
        {
            Jugador j = other.GetComponentInParent<Jugador>();
            if (j != null)
            {
                j.MejorarDano(multiplicador, duracion);
            }
            Destroy(gameObject);
        }
    }
}
