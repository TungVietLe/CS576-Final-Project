using UnityEngine;

public class YellowPill : BasePill
{
    [SerializeField] private float speedMultiplier = 0.5f;

    protected override void ActivateEffect()
    {
        base.ActivateEffect();
        
        PlayerMovement playerMovement = GetComponentInParent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.SetSpeedMultiplier(speedMultiplier);
        }
    }

    protected override void DeactivateEffect()
    {
        PlayerMovement playerMovement = GetComponentInParent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.SetSpeedMultiplier(1f);
        }
        base.DeactivateEffect();
    }
} 