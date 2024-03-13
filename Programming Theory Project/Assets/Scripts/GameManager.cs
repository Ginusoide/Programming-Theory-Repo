using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> ballsPrefabs;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float timerValue = 60;
    //public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public bool isGameActive;
    
    private int score;
    private float timer;
    private bool isPaused;
    private float spawnRate;

    public float spawnRateTest = 1.0f; // public per test
    private Vector3 playerStartPos = new Vector3(0, 2, 0);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive)
        {
            ChangePauseGame();
        }

        // Quando si gioca il timer diminuisce
        CountDown(timerValue);
    }

    public void UpdateScore(int addScore)
    {
        if (isGameActive)
        {
            if (score > 0 || addScore > 0)
            {
                score += addScore;
                scoreText.text = "Cestinate: " + score;
            }
        }
        // Altrimenti non fai una minchia
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "Cestinate: " + score;
    }

    public void GameOver()
    {
        // Impostazione UI di game over
        gameOverScreen.SetActive(true);
        isGameActive = false;
        timerText.gameObject.GetComponent<AudioSource>().Stop();
    }

    public void RestartGame()
    {
        StartGame();
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        // Modifiche alla UI e audio
        titleScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        gameOverScreen.GetComponent<AudioSource>().Stop();
        // scoreText.gameObject.SetActive(true); FIX per aggiunta di background
        scoreText.gameObject.transform.parent.gameObject.SetActive(true);
        // timerText.gameObject.SetActive(true); FIX per aggiunta di background
        timerText.gameObject.transform.parent.gameObject.SetActive(true);
        timerText.gameObject.GetComponent<AudioSource>().Play();
        player.SetActive(true);
        player.transform.position = playerStartPos;

        // Impostazione variabili
        isGameActive = true;
        isPaused = false;
        timer = timerValue;
        spawnRate = spawnRateTest;

        // Lancio del gioco
        StartCoroutine(SpawnRandomBall());
        ResetScore();

        // Impostazione timer al tempo massimo della partita (60 secondi attualmente)
        timerText.text = Mathf.Round(timerValue).ToString();
    }

    IEnumerator SpawnRandomBall()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int randomIndex = Random.Range(0, ballsPrefabs.Count);
            
            Instantiate(ballsPrefabs[randomIndex]);
        }
    }

    public void ChangePauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            isPaused = true;
        }
        else
        {
            // Sono già in pausa riprendo
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            isPaused = false;
        }
        
    }

    void CountDown(float timerValue)
    {
        if (isGameActive)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                // A 30 secondi mancanti aumento le palle generate
                if (timer <= 30)
                {
                    spawnRate = 0.8f;
                    if (timer <= 20)
                    {
                        spawnRate = 0.6f;
                        if (timer <= 10)
                        {
                            spawnRate = 0.4f;
                        }
                    }
                }
            }
            else
            {
                // Tempo scaduto Game Over
                timer = 0;
                GameOver();
            }
            timerText.text = Mathf.Round(timer).ToString();
        }
    }
}
