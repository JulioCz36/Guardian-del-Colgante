using UnityEngine;
using UnityEngine.UI;

public class BarraDeSaludFlotante : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public void actualizarBarraSalud(float valorActual, float valorMax)
    {
        slider.value = valorActual / valorMax;
    }
}
