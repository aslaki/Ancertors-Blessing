using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{

    private readonly Timer _levelEndTimer = new Timer();
    private readonly Timer _spawnEnemyTimer = new Timer();
    
    public UIManager uiManager;
    public EnemySpawner enemySpawner;

    public GameObject[] Enemies;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        Assert.IsNotNull(uiManager);
        Assert.IsNotNull(enemySpawner);
        _levelEndTimer.Start(1.0f * 60.0f);
        _spawnEnemyTimer.Start(5f);
    }

    // Update is called once per frame
    void Update()
    {
        _levelEndTimer.Update(Time.deltaTime);
        _spawnEnemyTimer.Update(Time.deltaTime);
        var minutes = Mathf.FloorToInt(_levelEndTimer.TimeLeft / 60);
        var seconds = Mathf.FloorToInt(_levelEndTimer.TimeLeft % 60);
        uiManager.LevelTimerText = $"{minutes:00}:{seconds:00}";
        if (_spawnEnemyTimer.HasElapsed)
        {
            Debug.Log("Spawning enemy");
            _spawnEnemyTimer.Start(5f);
            var enemyIndex = Random.Range(0, Enemies.Length);
            enemySpawner.SpawnEnemy(Enemies[enemyIndex]);
        }
        
    }
}
