using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    public BattleManager BattleManager;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public Image enemyImage;        
    public Sprite deadSprite;
    public bool isDead = false;
    private Sprite originalSprite;
    private Color originalColor;

    public float flashDuration = 0.1f;
    public Color flashColor = Color.white;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        isDead = false;

        if (enemyImage != null)
        {
            originalSprite = enemyImage.sprite;
            originalColor = enemyImage.color;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDead) return;

        Debug.Log("Enemy clicked!");

        int damage = Random.value < 0.25f ? 10 : 5;
        TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
        Debug.Log("Enemy took damage: " + damage + ", Health left: " + currentHealth);

        StartCoroutine(Flash());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator Flash()
    {
        if (enemyImage == null) yield break;

        // Flash to white instantly
        enemyImage.CrossFadeColor(flashColor, 0f, true, true);
        yield return new WaitForSeconds(flashDuration);

        // Fade back to original color
        enemyImage.CrossFadeColor(originalColor, 0f, true, true);
    }


    void Die()
    {
        isDead = true;
        Debug.Log("Enemy died!");
        if (enemyImage != null && deadSprite != null)
        {
            enemyImage.sprite = deadSprite;
        }
        BattleManager.EnemyDied();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        isDead = false;

        if (enemyImage != null)
        {
            enemyImage.sprite = originalSprite;
            enemyImage.color = originalColor;
        }
    }
}
