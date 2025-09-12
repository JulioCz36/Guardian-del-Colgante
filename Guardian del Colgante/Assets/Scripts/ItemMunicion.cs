using UnityEngine;

public class ItemMunicion : MonoBehaviour
{
    public int cantidad = 5;
    public float tiempoDeVida = 10f;

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HurtboxJugador"))
        {
            other.SendMessageUpwards("Recargar", cantidad);
            Destroy(gameObject);
        }
    }
}
