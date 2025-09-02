using UnityEngine;

public class movimiento : MonoBehaviour
{
    [Header("Configuraciones")]
    [SerializeField] float velocidad = 5f;
    private SpriteRenderer spriteRenderer;

    private float moverHorizontal;
    private float moverVertical;
    private Vector2 direccion;

    private Rigidbody2D mi_rb2d;

    private Animator mi_animator;

    // Codigo que es ejecutado cuand el objeto se activa en el nivel
    private void OnEnable()
    {
        mi_rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mi_animator = GetComponent<Animator>();
    }

    // Codigo ejecutado en cada frame del juego
    private void Update()
    {

        moverHorizontal = Input.GetAxis("Horizontal");
        moverVertical = Input.GetAxis("Vertical");
        direccion = new Vector2(moverHorizontal, moverVertical);

        if (direccion.magnitude > 1){
            direccion.Normalize();
        }

        if (moverVertical != 0 || moverHorizontal != 0){

            mi_animator.SetBool("inactivo", false);
        }else
        {
            mi_animator.SetBool("inactivo", true);
        }
    }

    private void FixedUpdate()
    {
        mi_rb2d.MovePosition(mi_rb2d.position + direccion * (velocidad * Time.fixedDeltaTime));
    }

    public Vector2 GetUltimaDireccion()
    {
        return direccion;
    }

    public void VoltearSegunMouse(Vector3 mousePos)
    {
        if (mousePos.x < transform.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

}
