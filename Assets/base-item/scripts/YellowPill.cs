using UnityEngine;

public class YellowPill : BasePill
{
    [SerializeField] private float speedMultiplier = 0.5f;

    protected override void ActivateEffect()
    {
        base.ActivateEffect();
        
        PlayerController playerController = GetComponentInParent<PlayerController>();
        if (playerController != null)
        {
            playerController.SetSpeedMultiplier(speedMultiplier);
        }
    }

    protected override void DeactivateEffect()
    {
        PlayerController playerController = GetComponentInParent<PlayerController>();
        if (playerController != null)
        {
            playerController.SetSpeedMultiplier(1f);
        }
        base.DeactivateEffect();
    }
} 