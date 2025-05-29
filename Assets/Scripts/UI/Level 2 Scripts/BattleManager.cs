using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public PlayerHealth player;       // Reference to the player
    public Enemy enemy;               // Reference to the enemy
    public GameObject winPanel;       // UI panel for win
    public GameObject losePanel;      // UI panel for lose
    public Button nextButton;         // Button shown on win
    public Button restartButton;      // Button shown on lose

    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextButton);

        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartButton);
    }

    public void PlayerDied()
    {
        if (losePanel != null)
            losePanel.SetActive(true);
    }

    public void EnemyDied()
    {
        if (winPanel != null)
            winPanel.SetActive(true);

        if (player != null)
            player.StopTakingDamage();
    }

    void OnNextButton()
    {
        Debug.Log("Loading next level: level_03_portal");
        SceneManager.LoadScene("level_03_portal");
    }

    public void OnRestartButton()
    {
        if (player != null) 
        {
            player.ResetHealth();
            player.StartTakingDamage();  // <-- enable damage again
        }
        if (enemy != null) enemy.ResetHealth();

        if (losePanel != null)
            losePanel.SetActive(false);
    }

}
