using UnityEngine;

public class Cascote : MonoBehaviour
{
    [Header("Configuración")]
    public int danoAEstructura =  10;
    public int danoAJugador = 15;
    public float tiempoDeVida = 5f;

    void Start()
    {
        Destroy(gameObject, tiempoDeVida); 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HurtboxJugador"))
        {
            collision.SendMessageUpwards("ModificarVida",-this.danoAJugador);
            Destroy(gameObject);
            return;
        }
        if (collision.gameObject.CompareTag("HurtboxMaterialObjetivo"))
        {
            collision.SendMessageUpwards("InfligirDano", danoAEstructura);
            Destroy(gameObject);
            return;
        }
    }
}
