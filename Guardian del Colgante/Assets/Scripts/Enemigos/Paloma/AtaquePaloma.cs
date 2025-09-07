using UnityEngine;

public class AtaquePaloma : MonoBehaviour
{
    [Header("Configuracion")]
    public PalomaVuelo paloma;
    public GameObject cacaPrefab;
    public Transform puntoDeCaca;
    public float tiempoEntreCacas = 2f;
    private float contadorCaca;

    void Start()
    {
        contadorCaca = tiempoEntreCacas;
    }

    void Update()
    {
        if (paloma != null && paloma.enZonaAtaque)
        {
            contadorCaca -= Time.deltaTime;
            if (contadorCaca <= 0f)
            {
                Instantiate(cacaPrefab, puntoDeCaca.position, Quaternion.identity);
                contadorCaca = tiempoEntreCacas;
            }
        }
    }
}
