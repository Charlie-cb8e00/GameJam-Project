using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public AudioSource buttonSound;
    public float delay = 0.3f;

    public void VolverMenu()
    {
        StartCoroutine(LoadSceneDelay("Menu"));
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

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Has salido del juego");
    }
}
