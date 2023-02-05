using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryLossMenu : MonoBehaviour
{
    public void OnClickMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }
}
