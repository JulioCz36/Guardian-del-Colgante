using UnityEngine;

public class MaterialObjetivo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float vidaMax = 5.0f;
    public float vidaActual = 0.0f;
    void Start()
    {
        vidaActual = vidaMax;
    }
    public void InfligirDano(float dano) {
        vidaActual -= dano;
        if (vidaActual <= 0.0f) {
            this.Muerte();
        }
    }
    public void Muerte() {
        // Por si se quiere hacer algo más complejo.
        Destroy(this.gameObject);
    }
}
