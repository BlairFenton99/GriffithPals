using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public BattleManager BattleManager;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public Image playerImage;           // UI Image for the player
    public Sprite deadSprite;
    private Sprite originalSprite;

    public float damageInterval = 3f;   // Time between damage
    public int damageAmount = 5;        // Damage per tick
    private float damageTimer;
    private bool isDead = false;

    private bool canTakeDamage = false;  // <-- control damage start

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        originalSprite = playerImage.sprite;
        damageTimer = damageInterval;
        isDead = false;
    }

    void Update()
    {
        if (isDead || !canTakeDamage) return;  // <-- only take damage if allowed

        damageTimer -= Time.deltaTime;
        if (damageTimer <= 0f)
        {
            TakeDamage(damageAmount);
            damageTimer = damageInterval;
        }
    }

    // Call this to allow the player to start taking damage
    public void StartTakingDamage()
    {
        canTakeDamage = true;
        damageTimer = damageInterval;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
        Debug.Log("Player took damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player died!");
        if (playerImage != null && deadSprite != null)
        {
            playerImage.sprite = deadSprite;
        }
        BattleManager.PlayerDied();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);

        if (playerImage != null && originalSprite != null)
        {
            playerImage.sprite = originalSprite;
        }

        isDead = false;
        canTakeDamage = false;  // reset flag so player doesn't start damaged again immediately
        damageTimer = damageInterval;
    }

    public void StopTakingDamage()
    {
        canTakeDamage = false;
    }

    
}
