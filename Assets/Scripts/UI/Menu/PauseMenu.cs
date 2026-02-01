using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPausa;  

    private bool estaEnPausa = false; 

    void Start()
    {
        menuPausa.SetActive(false);   
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!estaEnPausa)
                Pausa();
            else
                Reanudar();
        }
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
        estaEnPausa = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
        estaEnPausa = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    

    public void VolverMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitarJuego()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Se ha cerrado");
    }
}