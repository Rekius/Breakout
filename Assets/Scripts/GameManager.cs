using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private int startingLives = 3;

    [SerializeField] private BallScript ball;

    [SerializeField] private BrickSpawnerScript spawner;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private int penaltyScore = -50;

    [SerializeField] private int maxLives = 5;

    private int currentLevel = 0;
    private int lives;

    private int score = 0;
    private int brickRemaining;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        lives = startingLives;
        UpdateLivesUI();
        StartLevel();
    }

    private void StartLevel()
    {
        levelText.text = "Level: " + (currentLevel + 1);
        spawner.SpawnLevel(currentLevel);
        ball.SetBallSpeed(spawner.GetLevelSpeed(currentLevel));
        ball.ResetBall();
    }

    private void LevelCompleted()
    {
        currentLevel++;

        if (currentLevel >= spawner.LevelCount)
        {
            EndGame("You Win!");
        }
        else
        {
            StartLevel();
        }
    }

    private void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;
    }

    public void LoseLife()
    {
        lives--;
        AudioManager.Instance.PlayLoseLife();
        UpdateLivesUI();

        if (lives <= 0)
        {
            EndGame("Game Over!");
        }
        else
        {
            ball.ResetBall();
            AddScore(penaltyScore);
        }
    }

    public void RegisterBrick()
    {
        brickRemaining++;
    }

    public void AddScore(int amount)
    {
        score = Mathf.Max(0, score + amount);
        UpdateScore();
    }

    public void BrickDestroyed(int points)
    {
        AddScore(points);
        brickRemaining--;

        if (brickRemaining <= 0)
        {
            LevelCompleted();
        }
    }

    private void EndGame(string message)
    {
        endText.text = message;
        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void AddLife()
    {
        lives = Mathf.Min(lives + 1, maxLives);
        UpdateLivesUI();
    }
}
