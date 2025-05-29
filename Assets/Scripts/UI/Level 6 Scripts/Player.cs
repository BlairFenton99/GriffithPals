using UnityEngine;
using UnityEngine.UI;

public class PlayerBossHealth : MonoBehaviour
{
    public BossBattleManager BossBattleManager; //calls battleboss file
    public int maxHealth = 100; //set max health
    public int currentHealth; //health
    public HealthBar healthBar; //health bar logic
    public Image playerImage;           // UI Image for the player
    public Sprite deadSprite; //defeated sprite
    private Sprite originalSprite; //original sprite
    public float damageInterval = 3f;   // Time between damage
    public int damageAmount = 5;        // Damage per tick
    private float damageTimer; //set damage timer
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
        if (isDead || !canTakeDamage) return;  // only take damage if allowed/not already 0

        damageTimer -= Time.deltaTime;
        if (damageTimer <= 0f)
        {
            TakeDamage(damageAmount);
            damageTimer = damageInterval;
        }
    }

    public void StartTakingDamage() // Call this to allow the player to start taking damage
    {
        canTakeDamage = true;
        damageTimer = damageInterval;
    }

    public void TakeDamage(int damage) //Handles enemy attacking
    {
        if (isDead) return;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die() //Handle death
    {
        isDead = true;
        if (playerImage != null && deadSprite != null)
        {
            playerImage.sprite = deadSprite;
        }
        BossBattleManager.PlayerDied();
    }

    public void ResetHealth() //handles if 'retry" is hit
    {
        Debug.Log("ResetHealth called on player");
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
