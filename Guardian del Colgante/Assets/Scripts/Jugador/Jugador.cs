using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private float vida = 100f;
    [SerializeField] private int municionMax = 10; 
    [SerializeField] private int municionActual;

    private void OnEnable()
    {
        municionActual = municionMax;
    }
    public void ModificarVida(float puntos)
    {
        vida += puntos;

        if (vida <= 0)
        {
            vida = 0;
            Morir();
        }
    }
    public bool PuedeDisparar()
    {
        return municionActual > 0;
    }
    public void UsarBala()
    {
        if (municionActual > 0)
        {
            municionActual--;
        }
    }
    public void Recargar(int cantidad)
    {
        municionActual = Mathf.Clamp(municionActual + cantidad, 0, municionMax);
    }

    public int GetMunicionActual()
    {
        return municionActual;
    }
    private void Morir()
    {
        Debug.Log("Jugador murió");
    }
}
