using UnityEngine;

public class MaterialObjetivo : MonoBehaviour
{
    public int progresoMax = 100;
    public int progresoActual = 0;
    [SerializeField] private BarraProgreso barraProgreso;

    [Header("Configuración")]
    public int progresoPorSegundo = 5;
    private float contador = 0f;

    private void OnEnable()
    {

        barraProgreso.establecerMaximoProgreso(progresoMax);

    }

    void Update()
    {
        contador += Time.deltaTime;
        if (contador >= 1f) // cada 1 segundo
        {
            progresoActual += progresoPorSegundo;
            if (progresoActual > progresoMax)
                progresoActual = progresoMax;

            barraProgreso.establecerProgreso(progresoActual);

            contador = 0f; // reiniciamos el contador
        }
    }
    public void InfligirDano(int dano)
    {
        progresoActual -= dano;
        barraProgreso.establecerProgreso(progresoActual);

        if (progresoActual <= 0.0f){
            progresoActual = 0;
            Muerte();
        }
    }

    public void RobarMaterial(int cantidad)
    {
        progresoActual -= cantidad;
        barraProgreso.establecerProgreso(progresoActual);

        if (progresoActual <= 0.0f)
        {
            progresoActual = 0;
            Muerte();
        }
    }

    public void Muerte()
    {
        Debug.Log("El Material fue destruido.");
        Destroy(gameObject);
    }
}
