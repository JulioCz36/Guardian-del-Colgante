using UnityEngine;

public class ItemVelocidad : MonoBehaviour
{
    public float extraVelocidad = 3f;
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
            movimiento mov = other.GetComponentInParent<movimiento>();
            if (mov != null)
            {
                mov.MejorarVelocidadTemporal(extraVelocidad, duracion);
            }
            Destroy(gameObject);
        }
    }
}
