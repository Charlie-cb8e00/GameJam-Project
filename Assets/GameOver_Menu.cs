using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver_Menu : MonoBehaviour
{
    public float delay = 4f;

    public void Start()
    {
        StartCoroutine(LoadSceneDelay("Menu"));
        Debug.Log("cargando el menú");
    }

    IEnumerator LoadSceneDelay(string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);

    }
}
