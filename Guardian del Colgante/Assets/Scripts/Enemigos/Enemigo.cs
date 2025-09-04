using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Configuraciones")]
    [SerializeField] float vida;
    [SerializeField] float vidaMax = 10f;
    [SerializeField] BarraDeSaludFlotante barraDeSalud;

    private void OnEnable()
    {
        vida = vidaMax;
        barraDeSalud = GetComponentInChildren<BarraDeSaludFlotante>();
        barraDeSalud.actualizarBarraSalud(vida, vidaMax);
    }

    public void recibirDano(float cantDano)
    {

        vida -= cantDano;
        barraDeSalud.actualizarBarraSalud(vida, vidaMax);
        if (vida <= 0) { 
            Destroy(gameObject);
        }
    }
}
