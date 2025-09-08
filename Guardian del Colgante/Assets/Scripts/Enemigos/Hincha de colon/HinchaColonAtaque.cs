using UnityEngine;

public class HinchaColonAtaque : MonoBehaviour
{
    [Header("Configuración")]
    public float offsetPosible = 1.0f;
    private Animator animator;

    [Header("Disparo")]
    public GameObject cascotePrefab;
    public Transform puntoDisparo;
    public float fuerzaDisparo = 10f;
    private GameObject materialObjetivo;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetObjetivo(GameObject objetivo)
    {
        materialObjetivo = objetivo;
    }

    public void Lanzar()
    {
        animator.SetTrigger("lanzar");
    }

    public void DispararCascote()
    {
        if (materialObjetivo == null) return;

        GameObject casc = Instantiate(cascotePrefab, puntoDisparo.position, Quaternion.identity);
        Rigidbody2D cascRB = casc.GetComponent<Rigidbody2D>();

        // dirección hacia el material objetivo con un poco de offset
        Vector2 dir = (materialObjetivo.transform.position - puntoDisparo.position).normalized;
        Vector2 offset = new Vector2(Random.Range(-offsetPosible, offsetPosible), Random.Range(-offsetPosible, offsetPosible));

        cascRB.linearVelocity = (dir + offset * 0.1f).normalized * fuerzaDisparo;
    }
}
