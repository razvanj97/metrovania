using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public Slider healthSlider;

    public Image fadeScreen;

    public float fadeSpeed = 1.5f;
    private bool fadingToBlack, fadingFromBlack;

    public string mainMenuScene;
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(PlayerHealthController.instance.currentHealth, PlayerHealthController.instance.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(fadingToBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f) {
                fadingToBlack = false;
            }
        } else if (fadingFromBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f) {
                fadingFromBlack = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            pauseUnpause();
        }
    }

    public void UpdateHealth(int currentHealth, int maxHealth) {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    //Enalbe the loading screen transition
    public void StartFadeToBlack() {
        fadingToBlack = true;
        fadingFromBlack = false;
    }

    public void StartFadeFromBlack() {
        fadingFromBlack = true;
        fadingToBlack = false;
    }

    public void pauseUnpause() {
        if(!pauseScreen.activeSelf) {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        } else {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void goToMainMenu() {

        Time.timeScale = 1f;

        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;

        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;

        instance = null;
        Destroy(gameObject);

        SceneManager.LoadScene(mainMenuScene);
    }
}
