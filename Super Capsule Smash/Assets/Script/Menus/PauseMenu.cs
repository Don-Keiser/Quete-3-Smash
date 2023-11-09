using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseManager;
    [SerializeField] private GameObject pauseMenu;

    public void Pause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            pauseMenu.SetActive(true);
            pauseManager.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void Resume(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            pauseMenu.SetActive(false);
            pauseManager.SetActive(true);
            Time.timeScale = 1f;
        }
    }
}
