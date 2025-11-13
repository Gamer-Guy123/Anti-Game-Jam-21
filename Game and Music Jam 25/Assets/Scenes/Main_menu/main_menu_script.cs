using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script will handle main menu elements including button functions, background music, etc.


public class main_menu_script : MonoBehaviour
{
    // An audio source is specified within the inspector tab for the script under the canvas object. Once the music has been developed
    // it will need to be added from within the editor. The playback is started from the Start() method.
    public AudioSource backgroundAudioSource;

    // Specify index of scenes here
    // We load the level scene corresponding with the referenced integer. Adjust as needed.
    // Recommended that we use the following order: 0 - Main menu, 1 - About, 2 - First Level, 3 and up - Subsequent levels.
    // The reason for this is we was to increment level integer by one everytime the player moves forward. We reserve
    // Levels 1 and 2 for UI purposes.


    [Header("Main Menu Scene Indices")]
    [Tooltip("Default index for main menu is 0.")]
    public int firstLevelScene;
    [Tooltip("Default index for main menu is 1.")]
    public int aboutLevelScene;


    // Custom methods
    // This method is called when first button is pushed
    public void onPlayButtonPush(){
        // We load the level scene corresponding with the referenced integer. Adjust as needed.
        // Recommended that we use the following order: 0 - Main menu, 1 - About, 2 - First Level, 3 and up - Subsequent levels.
        // The reason for this is we was to increment level integer by one everytime the player moves forward. We reserve
        // Levels 1 and 2 for UI purposes.
        SceneManager.LoadScene(firstLevelScene);

    }

    // UI Level switching. Do no change. Instead change the integer from the inspector tab.
    // This method is called when second button is pushed
    public void onContinueButtonPush(){
        // Place holder for load continue level scene. We need to develop a way to save the integer for the last level completed
        // and then when loading, add +1 to this index, and load that index in the scene manager.
        SceneManager.LoadScene(1);
    }
    // This method is called when third button is pushed
    public void onAboutButtonPush(){
        SceneManager.LoadScene(aboutLevelScene);
    }




    // Default methods

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start the playing of background music
        backgroundAudioSource.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
