using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{

    public GameObject activeScreen; // First declaration is the landing screen

    private void Start()
    {
        activeScreen.SetActive(true); // Start the UI.
    }

    public void ToggleScreen(GameObject targetScreen)
    {

        if (activeScreen != null)
        {
            activeScreen.SetActive(false);
            activeScreen = targetScreen;
            activeScreen.SetActive(true);
        } else
        {
            activeScreen = targetScreen;
            activeScreen.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
