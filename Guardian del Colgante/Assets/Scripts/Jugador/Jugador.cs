using System.Collections;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private int vidaMax = 100;
    [SerializeField] private int vidaActual;
    [SerializeField] private int municionMax = 10;
    private int municionActual;
    [SerializeField] private BarraProgreso vidaBarra;
    [SerializeField] private BarraProgreso municionBarra;
    [SerializeField] Animator mi_animator;
    [SerializeField] private MenuCondicion menuGameOver;

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
            StartCoroutine(MorirCoroutine());
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
        municionBarra.establecerProgreso(municionActual);
    }

    private IEnumerator MorirCoroutine()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        mi_animator.SetTrigger("muerto");

        AnimatorStateInfo state = mi_animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length);

        Destroy(gameObject);

        menuGameOver.Activar();
    }
}
