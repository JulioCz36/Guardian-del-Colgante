using System.Collections;
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
    [SerializeField] Animator jugadorAnimator;
    public bool puedeDisparar;

    private float timer;
    public float tiempoEntreDisparos;


    [Header("Daño del proyectil")]
    public float danoBase = 3f;
    private float multiplicadorDano = 1f;

    private void OnEnable()
    {
        mainCamara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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

        Vector3 nuevaPos = pebeteTransform.localPosition;
        nuevaPos.x = Mathf.Clamp(mousePos.x - transform.position.x, -1f, 1f);
        pebeteTransform.localPosition = nuevaPos;

        if (!puedeDisparar)
        {
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


            GameObject nuevoPebete = Instantiate(pebete, pebeteTransform.position, Quaternion.Euler(0, 0, rotZ));

          
            PebeteScript pebeteScript = nuevoPebete.GetComponent<PebeteScript>();
            if (pebeteScript != null)
            {
                pebeteScript.ConfigurarDamage(danoBase * multiplicadorDano);
            }
        }

    }

    public void MejorarDanoTemporal(float multiplicador, float duracion)
    {
        StartCoroutine(MejorarDanoCoroutine(multiplicador, duracion));
    }

    private IEnumerator MejorarDanoCoroutine(float multiplicador, float duracion)
    {
        multiplicadorDano = multiplicador;
        yield return new WaitForSeconds(duracion);
        multiplicadorDano = 1f; // vuelve al normal
    }
}
