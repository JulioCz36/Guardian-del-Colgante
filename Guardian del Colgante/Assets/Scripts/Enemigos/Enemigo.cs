using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Configuraciones")]
    [SerializeField] float vida;
    [SerializeField] float vidaMax = 10f;
    [SerializeField] BarraDeSaludFlotante barraDeSalud;

    [SerializeField] Animator animator;
    [SerializeField] GameObject barraDeVida;

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
        if (vida <= 0)
        {
            Destroy(barraDeVida);
            StartCoroutine(Morir());
        }
    }

    private IEnumerator Morir()
    {
        animator.SetTrigger("muerto");
        foreach (var script in GetComponents<MonoBehaviour>())
        {
            if (script != this) script.enabled = false;
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length);

        Destroy(gameObject);
    }
}
