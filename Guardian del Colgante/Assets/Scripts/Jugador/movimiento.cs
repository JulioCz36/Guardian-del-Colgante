using System.Collections;
using UnityEngine;

public class movimiento : MonoBehaviour
{
    [Header("Configuraciones")]
    [SerializeField] float velocidadBase = 5f;
    [SerializeField] Animator mi_animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    private float velocidadActual;
    private float moverHorizontal;
    private float moverVertical;
    private Vector2 direccion;

    private Rigidbody2D mi_rb2d;


    private Coroutine boostCoroutine;
    private void OnEnable()
    {
        mi_rb2d = GetComponent<Rigidbody2D>();
        velocidadActual = velocidadBase;
    }

    // Codigo ejecutado en cada frame del juego
    private void Update()
    {

        moverHorizontal = Input.GetAxis("Horizontal");
        moverVertical = Input.GetAxis("Vertical");
        direccion = new Vector2(moverHorizontal, moverVertical);

        if (direccion.magnitude > 1)
        {
            direccion.Normalize();
        }

        mi_animator.SetBool("inactivo", direccion == Vector2.zero);
    }

    private void FixedUpdate()
    {
        mi_rb2d.MovePosition(mi_rb2d.position + direccion * (velocidadActual * Time.fixedDeltaTime));
    }

    public Vector2 GetUltimaDireccion()
    {
        return direccion;
    }

    public void VoltearSegunMouse(Vector3 mousePos)
    {
        spriteRenderer.flipX = mousePos.x < transform.position.x;
    }

    public void MejorarVelocidadTemporal(float extraVelocidad, float duracion)
    {
        if (boostCoroutine != null)
            StopCoroutine(boostCoroutine);

        boostCoroutine = StartCoroutine(VelocidadBoost(extraVelocidad, duracion));
    }

    private IEnumerator VelocidadBoost(float extraVelocidad, float duracion)
    {
        velocidadActual = velocidadBase + extraVelocidad;
        yield return new WaitForSeconds(duracion);
        velocidadActual = velocidadBase;
    }

}
