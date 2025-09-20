using UnityEngine;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField]
    private SceneController sceneController;
    public void Jugar()
    {
        sceneController.LoadScene("Nivel 1");
    }

    public void Opciones()
    {
        //SceneManager.LoadScene("Opciones");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
