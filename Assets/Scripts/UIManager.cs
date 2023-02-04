using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI levelTimerUI;

    public string timerPrefix = "Time:";

    private string _levelTimerText;
    
    public string LevelTimerText
    {
        set
        {
            _levelTimerText = $"{timerPrefix}: {value}";
            levelTimerUI.text = _levelTimerText;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(levelTimerUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
