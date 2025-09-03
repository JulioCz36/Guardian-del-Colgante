using UnityEngine;
using UnityEngine.UI;

public class BarraProgreso : MonoBehaviour
{
   public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void establecerMaximoProgreso(int maximo) { 
        slider.maxValue = maximo;
        slider.value = maximo;

        gradient.Evaluate(1f);

        fill.color = gradient.Evaluate(1f); 
    }

    public void establecerProgreso(int progreso) { 
        slider.value = progreso;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
