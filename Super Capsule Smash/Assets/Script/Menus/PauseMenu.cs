using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuHolder;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private ScGetInput input;

    private void Awake()
    {
        pauseMenuHolder = GameObject.Find("PauseMenuHolder");
    }

    private void Start()
    {
        pauseMenu = pauseMenuHolder.transform.GetChild(0).gameObject;
    }

    public void Pause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !pauseMenu.activeInHierarchy)
        {
            input.CanGetInput(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        else if (ctx.performed && pauseMenu.activeInHierarchy)
        {
            input.CanGetInput(true);
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
