using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public BossBattleManager BattleManager;
    public int maxHealth = 250;
    public int currentHealth;
    public HealthBar healthBar;

    public Image bossImage;
    public Sprite DefeatedBossSprite;
    public bool isDead = false;
    private Sprite originalSprite;

    void Start() 
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        isDead = false;
        originalSprite = bossImage.sprite; 
    }

    void Update()
    {
        if (isDead) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || //touch recognitcian 
            Input.GetMouseButtonDown(0)) // Used to test for debugging with mouse
        {
            int damage = Random.value < 0.25f ? 10 : 5; // damage value randomized
            TakeDamage(damage);
        }
    }

    void TakeDamage(int damage) //Handles damage
    {
        if (isDead) return;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Boss took damage. Current Health: " + currentHealth);
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die() //handles boss dying
    {
        isDead = true;
        Debug.Log("Boss died!");

        if (bossImage != null && DefeatedBossSprite != null)
        {
            bossImage.sprite = DefeatedBossSprite;
        }

        BattleManager.BossDied();
    }

    public void ResetHealth() //if retry button is hit
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);

        bossImage.sprite = originalSprite;
        isDead = false;
    }
}
