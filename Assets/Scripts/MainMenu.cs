using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Computer");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Has salido del juego");
    }
    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }
}
