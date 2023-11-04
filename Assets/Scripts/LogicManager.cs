using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour
{

    [SerializeField] private GameObject activeScreen; // First declaration is the landing screen
    [SerializeField] private GameObject activeLevelSelectButton; // This is the active button in the Level Select Screen
    [SerializeField] private GameObject[] activeDifficultyButtons; // This defines the current active difficulty

    [SerializeField] private Sprite activeButtonSprite;
    [SerializeField] private Sprite inactiveButtonSprite;
    [SerializeField] private Sprite activeDifficultySprite;

    private string preferredDifficulty;

    // Prevent the use of Magic Values
    private const string DEFAULT_DIFFICULTY = "easy";

    public bool DEBUG_MODE = true; // This is a temporary variable for usage with stuff; remove later

    private void Start()
    {

        if (DEBUG_MODE) {
            Debug.LogWarning("Turn off DEBUG_MODE or remove it before release.");
        }

        // Retrieve difficulty from the PlayerPrefs API
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("difficulty"))) {
            // This means that it is the first time playing the game
            if (DEBUG_MODE) { Debug.LogWarning("Difficulty is not set!"); }

            PlayerPrefs.SetString("difficulty", DEFAULT_DIFFICULTY);
            preferredDifficulty = DEFAULT_DIFFICULTY;
        } else {
            if (DEBUG_MODE) { Debug.LogWarning("Difficulty: '" + PlayerPrefs.GetString("difficulty") + "'"); }

            preferredDifficulty = PlayerPrefs.GetString("difficulty");
        }

        activeScreen.SetActive(true); // Start the UI.
    }

    public void ToggleScreen(GameObject targetScreen)
    {

        if (activeScreen != null)
        {
            activeScreen.SetActive(false);
            activeScreen = targetScreen;
            activeScreen.SetActive(true);

            if (activeScreen.name == "LevelSelect") {
                ToggleDifficulty();
            }
        } else
        {
            activeScreen = targetScreen;
            activeScreen.SetActive(true);
        }
    }

    public void ToggleButton(GameObject targetButton) {

        if (targetButton == activeLevelSelectButton) {
            return;
        }

        Image newButtonImage = targetButton.GetComponent<Image>();
        Image oldButtonImage = activeLevelSelectButton.GetComponent<Image>();

        if (activeLevelSelectButton != null) {
            newButtonImage.sprite = activeButtonSprite;
            oldButtonImage.sprite = inactiveButtonSprite;
            
            activeLevelSelectButton = targetButton;
        } else {
            newButtonImage.sprite = activeButtonSprite;
            activeLevelSelectButton = targetButton;
        }
    }

    // This function has to be called when the difficulty changes. NOTE: Intent may be inefficient.
    public void ToggleDifficulty(GameObject targetDifficulty) {
        if (targetDifficulty == null) {
            return;
        }

        preferredDifficulty = targetDifficulty.GetComponentInChildren<TextMeshProUGUI>().text.ToLower();
        PlayerPrefs.SetString("difficulty", preferredDifficulty);
        ToggleDifficulty();
    }

    // This overloaded function is called when the button has to be set. Run only when the level select screen starts.
    public void ToggleDifficulty() {
        foreach (var btn in activeDifficultyButtons)
        {
            string buttonDifficulty = btn.GetComponentInChildren<TextMeshProUGUI>().text;
            Image image = btn.GetComponent<Image>();

            Debug.Log(buttonDifficulty);

            // Compare the button's label and the preferred difficulty; On Success, change the button's sprite
            if (string.Equals(preferredDifficulty, buttonDifficulty, System.StringComparison.OrdinalIgnoreCase)) {
                image.sprite = activeDifficultySprite;
            } else {
                image.sprite = inactiveButtonSprite;
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
