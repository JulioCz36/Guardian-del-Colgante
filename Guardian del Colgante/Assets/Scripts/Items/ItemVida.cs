using UnityEngine;

public class ItemVida : MonoBehaviour
{
    public int cantidad = 20;

    public float tiempoDeVida = 10f;

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HurtboxJugador"))
        {
            other.SendMessageUpwards("ModificarVida", cantidad);
            Destroy(gameObject);
        }
    }
}
