using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;

    // Parts of the continue button
    // public GameObject continueButton;

    // public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {   
        // Parts of the continue button
        // if(PlayerPrefs.HasKey("ContinueLevel")) {
        //     continueButton.SetActive(true);
        // }
        AudioManager.instance.PlayMainMenuMusic();
    }

    public void NewGame() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(newGameScene);
    }

    // public void Continue() {
    //     player.gameObject.SetActive(true);
    //     player.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
    //     SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));
    // }

    public void QuitGame() {
        Application.Quit();
        Debug.Log("Game Quit");
    }

}
