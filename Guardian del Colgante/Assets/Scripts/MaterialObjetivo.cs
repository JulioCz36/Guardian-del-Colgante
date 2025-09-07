using UnityEngine;

public class MaterialObjetivo : MonoBehaviour
{
    public float vidaMax = 5.0f;
    public float vidaActual = 0.0f;
    void Start()
    {
        vidaActual = vidaMax;
    }

    public void InfligirDano(float dano)
    {
        vidaActual -= dano;
        Debug.Log("Material recibi� da�o. Vida: " + vidaActual);

        if (vidaActual <= 0.0f)
        {
            Muerte();
        }
    }

    public void RobarMaterial(float cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Enemigo rob� materiales. Vida: " + vidaActual);

        if (vidaActual <= 0.0f)
        {
            Muerte();
        }
    }

    public void Muerte()
    {
        Debug.Log("El Material fue destruido.");
        Destroy(gameObject);
    }
}
