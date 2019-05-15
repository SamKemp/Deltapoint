using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    
    // =================================
    // Variables
    // =================================
    
    public GameObject SecretButton;
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject DevMenu;
    
    // =================================
    // Main Menu buttons
    // =================================

    public void PlayBtn(string map)
    {
        SceneManager.LoadScene(map);
    }

    public void SettingsBtn()
    {
        SwitchMenu(SettingsMenu);
    }

    public void SecretBtn()
    {
        SwitchMenu(DevMenu);
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
    
    // =================================
    // Settings buttons
    // =================================

    public void ApplyBtn()
    {
        SwitchMenu(MainMenu);
    }

    public void VolumeChanged()
    {
        AudioListener.volume = GameObject.Find("Volume").GetComponent<Slider>().value;
    }

    public void GraphicsChanged()
    {
        //Debug.Log("Graphics changed to: " + GameObject.Find("Graphics").GetComponent<Dropdown>().value);
        QualitySettings.SetQualityLevel(GameObject.Find("Graphics").GetComponent<Dropdown>().value, true);
    }

    public void ResolutionChange()
    {
        switch (GameObject.Find("Resolution").GetComponent<Dropdown>().value)
        {
            case 0:
                Screen.SetResolution(720, 480, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(3840, 2160, Screen.fullScreen);
                break;
            case 4:
                Screen.SetResolution(7680, 4320, Screen.fullScreen);
                break;
            case 5:
                Screen.SetResolution(15360, 8640, Screen.fullScreen);
                break;
        }
    }
    
    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    
    // =================================
    // Dev buttons
    // =================================
    
    public void BackBtn()
    {
        SwitchMenu(MainMenu);
    }
    
    // =================================
    // General Functions
    // =================================
    
    void Start()
    {
        SwitchMenu(MainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            SecretButton.SetActive(!SecretButton.activeSelf);
        }
    }
    
    private void SwitchMenu(GameObject Menu)
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        DevMenu.SetActive(false);
        
        SecretButton.SetActive(false);
        
        Menu.SetActive(true);
    }
}
