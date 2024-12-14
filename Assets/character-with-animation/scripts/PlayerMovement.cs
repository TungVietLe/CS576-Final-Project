using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool IsMoving {
        get {
            return movement != Vector3.zero;
        }
    }
    public bool IsAttachTriggered {
        get {
            return Input.GetKeyDown(KeyCode.H);
        }
    }
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    
    private Rigidbody rb;
    private Vector3 movement;
    private bool isRunning;
    private bool canMove = true;
    private float speedMultiplier = 1f;
    
    public static bool dialogue = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        if (dialogue) return;
        if (!canMove) return;

        // Get input
        float Horizontal = Input.GetAxis("Horizontal") ;
        float Vertical = Input.GetAxis("Vertical") ;

        movement = Camera.main.transform.right * Horizontal + Camera.main.transform.forward * Vertical;
        movement.y = 0f;

        // Check if running (shift key)
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        
        // Update animation
        //animationController.SetMovementAnimation(movement, isRunning);
        
        // Handle attack inputs
        if (Input.GetKeyDown(KeyCode.H))
        {
        }
    }
    
    private void FixedUpdate()
    {
        if (PlayerAnimationController.curState == PlayerAnimationController.Attack) return;
        if (dialogue) return;
        if (!canMove) return;
        
        // Move the character
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        currentSpeed *= speedMultiplier;
        Vector3 moveVector = movement * currentSpeed;
        rb.MovePosition(rb.position + moveVector * Time.fixedDeltaTime);
        
        // Rotate the character to face movement direction
        if (movement != Vector3.zero)
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
            movement = Vector3.zero;
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}