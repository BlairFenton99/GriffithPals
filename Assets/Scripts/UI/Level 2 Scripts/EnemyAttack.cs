using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public PlayerHealth player;         // Reference to PlayerHealth script
    public Boss boss;                 // Reference to boss script
    public int damage = 15;             // Damage per hit
    public float attackInterval = 2f;   // Time between attacks

    private float nextAttackTime;

    void Update()
    {
        // Only attack if enemy is alive and player is alive
        if (boss != null && !boss.isDead && Time.time >= nextAttackTime)
        {
            AttackPlayer();
            nextAttackTime = Time.time + attackInterval;
        }
    }

    void AttackPlayer()
    {
        if (player != null && player.currentHealth > 0)
        {
            player.TakeDamage(damage);
        }
    }
}