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
    
    // Events for UI updates and game state changes
    public event Action<float> OnHealthChanged;
    public event Action OnPlayerDeath;
    
    private void Start()
    {
        currentHealth = maxHealth;
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
        // Disable player controls
        GetComponent<PlayerController>().enabled = false;
        // You might want to show game over screen or restart level here
    }
    
    // Method for environmental interaction
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
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PollutedArea"))
        {
            SetInPollutedArea(false);
        }
    }
}