using System.Collections;
using UnityEngine;

public class ProyectilPaloma : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private int dano = 1;
    [SerializeField] private Animator mi_animator;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("HurtboxMaterialObjetivo"))
        {
            collision.SendMessageUpwards("InfligirDano", dano);
            StartCoroutine(ColisionCoroutine());
        }
      
    }

    private IEnumerator ColisionCoroutine()
    {
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        if (mi_animator != null)
            mi_animator.SetTrigger("colision");


        AnimatorStateInfo state = mi_animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length);

        Destroy(gameObject);
    }
}
