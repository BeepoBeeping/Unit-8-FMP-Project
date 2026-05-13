using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IngameMenuScript : MonoBehaviour
{

    public GameObject ingameMenu;
    InputAction menuAction;
    public bool menu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuAction = InputSystem.actions.FindAction("Menu");
        Time.timeScale = 1f;






        menu = false;
    }

    // Update is called once per frame
    void Update()
    {
        MenuOpen();
    }

    public void MenuOpen()
    {
        if (menu == false && menuAction.IsPressed())
        {
            menu = true;
            ingameMenu.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    public void Resume()
    {
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
        menu = false;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Frontend");
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
        menu = false;
    }
}
