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

    public int currentRound;
    public bool gameIsPaused;

    public GameObject[] levelUpSkills;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<levelUpSkills.Length; i++){
            levelUpSkills[i].SetActive(false);
        }
        currentRound = 1;
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
        if (currentRound == 1 ){
            minutes +=1;
        }
        var seconds = Mathf.FloorToInt(_levelEndTimer.TimeLeft % 60);
        uiManager.LevelTimerText = $"{minutes:00}:{seconds:00}";
        if (_spawnEnemyTimer.HasElapsed)
        {
            Debug.Log("Spawning enemy");
            _spawnEnemyTimer.Start(5f);
            var enemyIndex = Random.Range(0, Enemies.Length);
            enemySpawner.SpawnEnemy(Enemies[enemyIndex]);
        }

        if (_levelEndTimer.HasElapsed){
            if(currentRound == 1){
                LevelUp();
            } else if(currentRound == 2){
                GameOver(true);
            }
        }
        
    }

    private void LevelUp(){
        Time.timeScale = 0;
        gameIsPaused = true;
        for (int i=0; i<levelUpSkills.Length; i++){
            levelUpSkills[i].SetActive(true);
        }
    }

    public void GoToRound2(){

        for (int i=0; i<levelUpSkills.Length; i++){
            levelUpSkills[i].SetActive(false);
        }
        currentRound = 2;
        _levelEndTimer.Start(1.0f * 60.0f);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void ChooseSkillHeal(){
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Heal();
        GoToRound2();
    }

    public void ChooseSkillFireRate(){
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IncreaseFireRate();
        GoToRound2();
    }

    public void GameOver(bool isVictory){
        if (isVictory){
            Victory();
        } else {
            Loss();
        }
    }

    private void Victory(){
        Time.timeScale = 0;
        gameIsPaused = true;
        StartCoroutine(LoadVictoryDeath("Victory"));
    }

    private void Loss(){
        Time.timeScale = 0;
        gameIsPaused = true;
        StartCoroutine(LoadVictoryDeath("Loss"));
    }

    private IEnumerator LoadVictoryDeath(string levelName)
    {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}
