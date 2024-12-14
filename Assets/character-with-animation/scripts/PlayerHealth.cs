using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public bool isDead {get; private set;} = false;
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float environmentalDamageRate = 5f;
    [SerializeField] private float healingRate = 10f;
    
    private float currentHealth;
    private bool isInPollutedArea;
    private PlayerMovement playerController;
    
    // Events for UI updates and game state changes
    public event Action<float> OnHealthChanged;
    public event Action OnPlayerDeath;
    
    private void Start()
    {
        currentHealth = maxHealth;
        playerController = GetComponent<PlayerMovement>();
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
    
    
    private void Die()
    {
        OnPlayerDeath?.Invoke();
        playerController.SetCanMove(false);
        isDead = true;
        // You might want to show game over screen or restart level here
    } 
}