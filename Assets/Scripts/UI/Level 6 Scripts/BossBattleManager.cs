using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossBattleManager : MonoBehaviour
{
    public PlayerBossHealth player;      // Reference to the player
    public Boss boss;               // Reference to the boss
    public bossAttack bossAttackScript;
    public GameObject winPanel;       // UI panel for win
    public GameObject losePanel;      // UI panel for lose
    public Button continueButton;         // Button shown on win
    public Button restartButton;      // Button shown on lose

    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        if (continueButton != null)
            continueButton.onClick.AddListener(OnNextButton);

        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartButton);
    }

    public void PlayerDied()
    {
        if (losePanel != null)
            losePanel.SetActive(true);
    }

    public void BossDied()
    {
        if (winPanel != null)
            winPanel.SetActive(true);

        if (player != null)
            player.StopTakingDamage();
    }

    void OnNextButton()
    {
        Debug.Log("insert end scene here");
        SceneManager.LoadScene("");
    }

    public void OnRestartButton()
    {
        Debug.Log("OnRestartButton() called");
        if (player != null)
        {
            player.ResetHealth();
            player.StartTakingDamage();
        }
        if (boss != null) boss.ResetHealth();

        if (bossAttackScript != null)
            bossAttackScript.ResetAttackTimer();

        if (losePanel != null)
            losePanel.SetActive(false);
        if (winPanel != null)
            winPanel.SetActive(false);
    }
}