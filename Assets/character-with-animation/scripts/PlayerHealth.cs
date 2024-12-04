using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float environmentalDamageRate = 5f;
    [SerializeField] private float healingRate = 10f;
    
    private float currentHealth;
    private bool isInPollutedArea;
    private PlayerController playerController;
    private PlayerAnimationController animationController;
    
    // Events for UI updates and game state changes
    public event Action<float> OnHealthChanged;
    public event Action OnPlayerDeath;
    
    private void Start()
    {
        currentHealth = maxHealth;
        playerController = GetComponent<PlayerController>();
        animationController = GetComponent<PlayerAnimationController>();
    }
    
    private void Update()
    {
        if (isInPollutedArea)
        {
            TakeDamage(environmentalDamageRate * Time.deltaTime);
        }
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        OnHealthChanged?.Invoke(currentHealth / maxHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        OnHealthChanged?.Invoke(currentHealth / maxHealth);
    }
    
    public void SetInPollutedArea(bool inPollutedArea)
    {
        isInPollutedArea = inPollutedArea;
    }
    
    private void Die()
    {
        OnPlayerDeath?.Invoke();
        playerController.SetCanMove(false);
        animationController.SetDeathState();
        // You might want to show game over screen or restart level here
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PollutedArea"))
        {
            SetInPollutedArea(true);
        }
        else if (other.CompareTag("HealingArea"))
        {
            Heal(healingRate * Time.deltaTime);
        }
        else if (other.CompareTag("Enemy"))
        {
            // EnemyAttack enemyAttack = other.GetComponent<EnemyAttack>();
            // if (enemyAttack != null)
            // {
            //     TakeDamage(enemyAttack.GetDamageAmount());
            // }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PollutedArea"))
        {
            SetInPollutedArea(false);
        }
    }
}