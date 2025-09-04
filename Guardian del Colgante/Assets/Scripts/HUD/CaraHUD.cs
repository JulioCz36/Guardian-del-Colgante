using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CaraHUD : MonoBehaviour
{
    [Header("Imagen del personaje")]
    [SerializeField] private Image caraImage;

    [Header("Sprites por estado")]
    [SerializeField] private Sprite normal;
    [SerializeField] private Sprite herido;
    [SerializeField] private Sprite feliz;
    [SerializeField] private Sprite asustado;

    public enum Estado { Normal, Herido, Feliz, Asustado }

    private Coroutine caraTemporalCoroutine = null;
    private Estado estadoFijo = Estado.Normal;

    public void CambiarEstado(Estado estado)
    {
        // Si hay un estado fijo activo
        if (estado == Estado.Normal && estadoFijo != Estado.Normal) return;

        caraImage.sprite = GetSprite(estado);
    }
    public void MostrarEstadoTemporal(Estado estado, float duracion)
    {
        // Cancelamos corutina anterior si hay
        if (caraTemporalCoroutine != null) StopCoroutine(caraTemporalCoroutine);
        caraTemporalCoroutine = StartCoroutine(EstadoTemporalCoroutine(estado, duracion));
    }

    private IEnumerator EstadoTemporalCoroutine(Estado estado, float duracion)
    {
        CambiarEstado(estado);
        yield return new WaitForSeconds(duracion);

        // Volver al estado fijo
        CambiarEstado(estadoFijo);
        caraTemporalCoroutine = null;
    }
    public void EstablecerEstadoFijo(Estado estado)
    {
        estadoFijo = estado;
        CambiarEstado(estadoFijo);
    }

    public void QuitarEstadoFijo()
    {
        estadoFijo = Estado.Normal;
        CambiarEstado(estadoFijo);
    }

    private Sprite GetSprite(Estado estado)
    {
        switch (estado)
        {
            case Estado.Normal: return normal;
            case Estado.Herido: return herido;
            case Estado.Feliz: return feliz;
            case Estado.Asustado: return asustado;
            default: return normal;
        }
    }
}
