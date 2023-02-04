using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    
    public GameObject startMenu;
    public GameObject creditsMenu;
    public GameObject controlsMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
    
    public void OnClickExit()
    {
        Application.Quit();
    }
    
    public void OnClickCredits()
    {
        this.startMenu.SetActive(false);
        this.creditsMenu.SetActive(true);
    }

    public void OnClickControls()
    {
        this.startMenu.SetActive(false);
        this.controlsMenu.SetActive(true);
    }
    
    public void OnClickBack()
    {
        this.startMenu.SetActive(true);
        this.creditsMenu.SetActive(false);
        this.controlsMenu.SetActive(false);
    }
}
