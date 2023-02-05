using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VictoryLossMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject victoryLossImage;

    private void Start()
    {
        victoryLossImage.SetActive(false);
        StartCoroutine(AppearText());
    }

    public void OnClickMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }

    public IEnumerator AppearText()
    {
        yield return new WaitForSeconds(1.5f);
        victoryLossImage.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }
    
    
}
