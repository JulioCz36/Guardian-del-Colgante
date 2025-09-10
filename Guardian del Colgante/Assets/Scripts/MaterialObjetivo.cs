using UnityEngine;

public class MaterialObjetivo : MonoBehaviour
{

    [Header("Configuración")]
    public int progresoPorSegundo = 5;
    private float contador = 0f;
    public int progresoMax = 100;
    public int progresoActual = 0;
    [SerializeField] private BarraProgreso barraProgreso;
    [SerializeField] private MenuCondicion menuGameOver;
    [SerializeField] private MenuCondicion menuYouWin;
    private Animator animator;

    private void OnEnable()
    {

        barraProgreso.establecerMaximoProgreso(progresoMax);
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        contador += Time.deltaTime;
        if (contador >= 1f) 
        {
            progresoActual += progresoPorSegundo;
            if (progresoActual > progresoMax) {
                progresoActual = progresoMax;
                menuYouWin.Activar();
            }


            barraProgreso.establecerProgreso(progresoActual);

            contador = 0f; 
        }
    }
    public void InfligirDano(int dano)
    {
        progresoActual -= dano;
        barraProgreso.establecerProgreso(progresoActual);
        animator.SetTrigger("dano");

        if (progresoActual <= 0.0f){
            progresoActual = 0;
            Muerte();
        }
    }

    public void Muerte()
    {
        Destroy(gameObject);
        menuGameOver.Activar();
    }
}
