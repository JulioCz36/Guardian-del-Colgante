using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCondicion : MonoBehaviour
{
    public GameObject menuCondicion;

    public void Activar()
    {
        menuCondicion.SetActive(true);
        MenuPausa.juegoPausado = true;
        Time.timeScale = 0f;
    }

    public void ReiniciarNivel()
    {
        MenuPausa.juegoPausado = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VolverAlMenu()
    {
        MenuPausa.juegoPausado = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
