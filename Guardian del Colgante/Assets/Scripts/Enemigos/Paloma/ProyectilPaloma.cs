using UnityEngine;

public class ProyectilPaloma : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private int dano = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("MaterialObjetivo"))
        {
            collision.SendMessageUpwards("InfligirDano", dano);
            Destroy(gameObject);
            return;
        }
    }
}
