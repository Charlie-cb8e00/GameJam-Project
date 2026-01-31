using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public InputActionReference clicAction;
    public float transitionTime = 1f;
    void Update()
    {
        if (clicAction.action.IsPressed())
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int index)
    {
        //Play animation
        transition.SetTrigger("Start");
        //wait for animation
        yield return new WaitForSeconds(transitionTime);
        //Load scene
        SceneManager.LoadScene(index);
    }
}
