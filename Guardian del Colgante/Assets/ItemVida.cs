using UnityEngine;

public class ItemVida : MonoBehaviour
{
    public int cantidad = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HurtboxJugador"))
        {
            other.SendMessageUpwards("ModificarVida", cantidad);
            Destroy(gameObject);
        }
    }
}
