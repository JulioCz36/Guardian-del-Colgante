using UnityEngine;

public class MosquitoAtaque : MonoBehaviour
{
    public Mosquito mosquito;
    public int danoAJugador = 15;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HurtboxJugador"))
        {
            collision.SendMessageUpwards("ModificarVida", -danoAJugador);
            mosquito.AplicarKnockback();
        }
    }
}
