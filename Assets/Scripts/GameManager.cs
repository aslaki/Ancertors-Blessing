using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{

    private readonly Timer _levelEndTimer = new Timer();
    
    public UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        Assert.IsNotNull(uiManager);
        _levelEndTimer.Start(1.0f * 60.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _levelEndTimer.Update(Time.deltaTime);
        var minutes = Mathf.FloorToInt(_levelEndTimer.TimeLeft / 60);
        var seconds = Mathf.FloorToInt(_levelEndTimer.TimeLeft % 60);
        uiManager.LevelTimerText = $"{minutes:00}:{seconds:00}";
    }
}
