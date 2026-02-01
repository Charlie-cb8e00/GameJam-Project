using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMenu : MonoBehaviour
{
    public AudioSource buttonSound;
    public float delay = 0.3f;

    public void Atras()
    {
        StartCoroutine(LoadSceneDelay("Menu"));
    }


    IEnumerator LoadSceneDelay(string sceneName)
    {
        buttonSound.Play();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
