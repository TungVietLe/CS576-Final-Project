using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    
    // Animation parameter names
    private readonly string IS_WALKING = "IsWalking";
    private readonly string IS_RUNNING = "IsRunning";
    private readonly string ATTACK = "Attack";
    private readonly string MOVEMENT_X = "MovementX";
    private readonly string MOVEMENT_Y = "MovementY";
    private readonly string IS_DEAD = "IsDead";
    private readonly string IS_IDLE = "IsIdle";
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }
    
    public void SetMovementAnimation(Vector2 movement, bool isRunning)
    {
        animator.SetFloat(MOVEMENT_X, movement.x);
        animator.SetFloat(MOVEMENT_Y, movement.y);
        animator.SetBool(IS_IDLE, movement.magnitude == 0);
        animator.SetBool(IS_WALKING, movement.magnitude > 0);
        animator.SetBool(IS_RUNNING, isRunning && movement.magnitude > 0);
    }
    
    public void TriggerAttack()
    {
        animator.SetTrigger(ATTACK);
    }
    
    public void SetDeathState()
    {
        animator.SetBool(IS_DEAD, true);
    }
} 