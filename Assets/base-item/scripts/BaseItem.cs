using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] protected bool consumeOnUse = true;
    [SerializeField] protected bool canUseMultipleTimes = false;
    
    protected bool hasBeenUsed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Parent the item to the player before using it
            transform.SetParent(other.transform);
            
            // Use the item
            if (Use())
            {
                // If successfully used and should be consumed, destroy the object
                if (consumeOnUse)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public virtual bool Use()
    {
        if (hasBeenUsed && !canUseMultipleTimes) return false;
        
        if (consumeOnUse)
        {
            hasBeenUsed = true;
        }
        
        return true;
    }

    protected virtual void Update()
    {
        // Base update method for inheritance
    }
} 