using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {

    [Header("Game Stats")]
    [SerializeField] public Slider healthBar;
    //[SerializeField] Text scoreText;
    //[SerializeField] public int score = 0;
    [SerializeField] int enemiesCount;
    PlayerController player;

    [Header("UI Level Complete")]
    [SerializeField] GameObject levelCompleteScreen;
    [SerializeField] Text scoreTextWinScreen;
    [SerializeField] GameObject gameoverScreen;


    // Use this for initialization
    void Awake()
    {
        //Singleton pattern
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        Time.timeScale = 1.0f;
        //scoreText.text = score.ToString();
        //scoreTextWinScreen.text = score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (healthBar.value == 0)
        {
            PlayerDeath();
        }
    }


    //// --- PLAYER WIN AND GAMEOVER --- ////

    public void CountEnemies()
    {
        enemiesCount++;
    }

    public void EnemyDestroyed()
    {
        enemiesCount--;
        if (enemiesCount <= 0)
        {
            PlayerWin();
        }
    }

    public void PlayerWin()
    {
        Time.timeScale = 0f;
        levelCompleteScreen.SetActive(true);
    }

    public void PlayerDeath()
    {
        Time.timeScale = 0f;
        gameoverScreen.SetActive(true);
    }


    //// --- PLAYER'S SCORE --- ////

    //public int ScoreManager(int scoreValue)
    //{
    //    return score += scoreValue;
    //}

    //public void UpdateScore()
    //{
    //    scoreText.text = score.ToString();
    //    scoreTextWinScreen.text = score.ToString();
    //}


    ////--- LEVEL MANAGER ---////

    public IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(0.5f);
        levelCompleteScreen.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void RestartLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
        Destroy(gameObject);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel);
        Destroy(gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
