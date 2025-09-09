using UnityEngine;

public class AutoViejoAtaque : MonoBehaviour
{
    public AutoViejo autoViejo;
    public int danoAEstructura = 20;
    public int danoAJugador = 15;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HurtboxMaterialObjetivo"))
        {
            collision.SendMessageUpwards("InfligirDano", danoAEstructura);
            autoViejo.AplicarKnockback();
        }

        if (collision.gameObject.CompareTag("HurtboxJugador"))
        {
            collision.SendMessageUpwards("ModificarVida", -danoAJugador);
            autoViejo.AplicarKnockback();
        }
    }
}
