using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource buttonSound;
    public float delay = 0.3f;
    public void PrimerNivel()
    {
        StartCoroutine(LoadSceneDelay("Interior_1"));
    }

    public void Jugar()
    {
        StartCoroutine(LoadSceneDelay("Computer"));
    }

    public void Tutorial()
    {
        StartCoroutine(LoadSceneDelay("Tutorial"));
    }

    public void Creditos()
    {
        StartCoroutine(LoadSceneDelay("Creditos"));
    }

    public void Opciones()
    {
        StartCoroutine(LoadSceneDelay("Opciones"));
    }

    public void Salir()
    {
        buttonSound.Play();
        Invoke(nameof(QuitGame), delay);
    }

    IEnumerator LoadSceneDelay(string sceneName)
    {
        buttonSound.Play();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Has salido del juego");
    }
}
