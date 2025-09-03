using UnityEngine;

public class disparo : MonoBehaviour
{

    private Camera mainCamara;
    private Vector3 mousePos;

    [Header("Configuracion")]
    [SerializeField] private Jugador jugador;
    [SerializeField] private movimiento playerMovimiento;
    [SerializeField] private GameObject pebete;
    [SerializeField] private Transform pebeteTransform;

    private Animator jugadorAnimator;
    public bool puedeDisparar;

    private float timer;
    public float tiempoEntreDisparos;


    private void OnEnable()
    {
        mainCamara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        jugadorAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void Update()
    {
        mousePos = mainCamara.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (playerMovimiento != null)
        {
            playerMovimiento.VoltearSegunMouse(mousePos);
        }

        if (!puedeDisparar) { 
            timer += Time.deltaTime;
            if (timer > tiempoEntreDisparos)
            {
                puedeDisparar = true;
                timer = 0;
            }
        }
        if (Input.GetButton("Fire1") && puedeDisparar && jugador.PuedeDisparar())
        {
            puedeDisparar = false;
            jugadorAnimator.SetTrigger("lanzar");
            jugador.UsarBala();
            Instantiate(pebete, pebeteTransform.position, Quaternion.identity);
        }

    }
}
