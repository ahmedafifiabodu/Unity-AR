using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject gameOverPanel;

    private Player player;
    private EnemyPooling enemyPooling;

    public static UI Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        player = Player.Instance;
        enemyPooling = EnemyPooling.Instance;
    }

    internal void UpdateHealth(int health) => healthText.text = "Health: " + health;

    internal void UpdateScore(int score) => scoreText.text = "Score: " + score;

    internal void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        player.UpdatePlayerData();
        enemyPooling.ResetEnemies();
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}