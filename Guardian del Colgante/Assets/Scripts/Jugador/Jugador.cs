using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private int vidaMax = 100;
    private int vidaActual;
    [SerializeField] private int municionMax = 10;
     private int municionActual;
    [SerializeField] private BarraProgreso vidaBarra;
    [SerializeField] private BarraProgreso municionBarra;

    private void OnEnable()
    {
        municionActual = municionMax;
        vidaActual = vidaMax;
        municionBarra.establecerMaximoProgreso(municionMax);
        vidaBarra.establecerMaximoProgreso(vidaMax);
    }
    public void ModificarVida(int puntos)
    {
        vidaActual += puntos;
        vidaBarra.establecerProgreso(vidaActual);

        if (vidaActual <= 0)
        {
            vidaActual = 0;
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
            municionBarra.establecerProgreso(municionActual);
        }
    }
    public void Recargar(int cantidad)
    {
        municionActual = Mathf.Clamp(municionActual + cantidad, 0, municionMax);
    }
    private void Morir()
    {
        Debug.Log("Jugador murió");
    }
}
