using UnityEngine;

public class Ladron : MonoBehaviour
{
    
    
    protected Rigidbody2D rb;
    public Animator animator;

    [Header("Daño")]
    public int danoAEstructura = 5;
    public int danoAJugador = 15;

    [Header("Movimiento")]
    public float speed = 3.0f;
    public float tiempoDeCaminata = 2.0f; 
    public float tiempoDePausa = 1.0f;

    [Header("Zigzag")]
    public float anguloZigzag = 15f;
    private int direccionZigzag = 1;

    [Header("Robo")]
    public float tiempoRobando = 3f; 
    public int danoPorSegundo = 5;

    [Header("Loot")]
    public GameObject objetoRobadoPrefab;
    public GameObject objetoRobadoTransform;
    public float tiempoMostrandoObjeto = 1f; 

    private float contadorMostrarObjeto = 0f;
    private GameObject objetoInstanciado;

    private float contadorRobo = 0f;
    private float tiempoRobandoTotal = 0f;


    private GameObject materialObjetivo;
    private Vector2 direccion;

    private string estado = "caminar";
    private float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        materialObjetivo = GameObject.FindGameObjectWithTag("MaterialObjetivo");
        ActualizarDireccion();
    }

    void Update()
    {
        if (materialObjetivo == null) return;

        switch (estado)
        {
            case "caminar":
                timer += Time.deltaTime;
                rb.linearVelocity = direccion * speed;
                animator.SetBool("caminando", true);

                if (timer >= tiempoDeCaminata)
                {
                    estado = "parado";
                    rb.linearVelocity = Vector2.zero;
                    timer = 0f;
                }
                break;

            case "parado":
                timer += Time.deltaTime;
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("caminando", false);
                if (timer >= tiempoDePausa)
                {
                    estado = "caminar";
                    timer = 0f;

                    direccionZigzag *= -1;
                    ActualizarDireccion();
                }
                break;

            case "robando":
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("robando", true);

                contadorRobo += Time.deltaTime;
                tiempoRobandoTotal += Time.deltaTime;

                if (contadorRobo >= 1f)
                {
                    materialObjetivo.GetComponent<MaterialObjetivo>()
                                    .RobarMaterial(danoPorSegundo);
                    contadorRobo = 0f;
                }

                if (tiempoRobandoTotal >= tiempoRobando)
                {
                    estado = "encontro";
                    animator.SetBool("robando", false);

                    // mostrar objeto robado
                    if (objetoRobadoPrefab != null && objetoRobadoTransform != null)
                    {
                        objetoInstanciado = Instantiate(objetoRobadoPrefab,objetoRobadoTransform.transform.position,Quaternion.identity,objetoRobadoTransform.transform);
                        objetoInstanciado.transform.localPosition = Vector3.zero;
                    }

                    contadorMostrarObjeto = 0f;
                    rb.linearVelocity = Vector2.zero;
                }
                break;

            case "encontro":
                contadorMostrarObjeto += Time.deltaTime;

                if (contadorMostrarObjeto >= tiempoMostrandoObjeto)
                {

                    estado = "escapando";
                    animator.SetBool("escapando", true);

                    Vector2 dirEscape = (transform.position - materialObjetivo.transform.position).normalized;
                    rb.linearVelocity = dirEscape * speed;

                    if (dirEscape.x > 0)
                        transform.localScale = new Vector3(-1, 1, 1);
                    else
                        transform.localScale = new Vector3(1, 1, 1);
                }
                break;
        }

    }
    private void ActualizarDireccion()
    {
        Vector2 baseDir = (materialObjetivo.transform.position - transform.position).normalized;

        float rad = anguloZigzag * Mathf.Deg2Rad * direccionZigzag;


        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        direccion = new Vector2(
            cos * baseDir.x - sin * baseDir.y,
            sin * baseDir.x + cos * baseDir.y
        ).normalized;


        if (baseDir.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    public void EmpezarRobo()
    {
        rb.linearVelocity = Vector2.zero;
        estado = "robando";
        contadorRobo = 0f;
        tiempoRobandoTotal = 0f;
    }
}
