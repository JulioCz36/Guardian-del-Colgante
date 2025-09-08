using UnityEngine;

public class AtaquePaloma : MonoBehaviour
{
    [Header("Configuracion")]
    public PalomaVuelo paloma;
    public GameObject cacaPrefab;
    public Transform puntoDeCaca;
    public float tiempoEntreCacas = 2f;
    
    private Animator animator;
    private float contadorCaca;

    void Start()
    {
        contadorCaca = tiempoEntreCacas;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (paloma != null && paloma.enZonaAtaque)
        {
            contadorCaca -= Time.deltaTime;
            if (contadorCaca <= 0f)
            {
                animator.SetTrigger("lanzar");
                contadorCaca = tiempoEntreCacas;
            }
        }
    }

    // Llamado desde Animation Event
    public void LanzarCaca()
    {
        Instantiate(cacaPrefab, puntoDeCaca.position, Quaternion.identity);
    }
}
