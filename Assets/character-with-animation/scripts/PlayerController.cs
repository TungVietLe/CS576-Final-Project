using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    
    private PlayerAnimationController animationController;
    private Rigidbody rb;
    private Vector2 movement;
    private bool isRunning;
    private bool canMove = true;
    private float speedMultiplier = 1f;
    
    public static bool dialogue = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animationController = GetComponent<PlayerAnimationController>();
    }
    
    private void Update()
    {
        if (dialogue) return;
        if (!canMove) return;
        
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        
        // Check if running (shift key)
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        
        // Update animation
        animationController.SetMovementAnimation(movement, isRunning);
        
        // Handle attack inputs
        if (Input.GetKeyDown(KeyCode.H))
        {
            animationController.TriggerAttack();
    }
    }
    
    private void FixedUpdate()
    {
        if (dialogue) return;
        if (!canMove) return;
        
        // Move the character
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 moveVector = new Vector3(movement.x, 0, movement.y) * currentSpeed;
        rb.MovePosition(rb.position + moveVector * Time.fixedDeltaTime);
        
        // Rotate the character to face movement direction
        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
    }
    
    public void SetCanMove(bool value)
    {
        canMove = value;
        if (!canMove)
        {
            movement = Vector2.zero;
            animationController.SetMovementAnimation(movement, false);
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}