using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    // This function will be called when the Start Game button is clicked
    public void StartGame() {
        // Load the main game scene
        SceneManager.LoadScene("Ingame");
    }

    // This function will be called when the Options button is clicked
    public void OpenOptions() {
        // Implement your options menu logic here
        Debug.Log("Options menu opened.");
    }

    // This function will be called when the Exit button is clicked
    public void ExitGame() {
        // Quit the application
        Application.Quit();
        Debug.Log("Game is exiting.");
    }
}