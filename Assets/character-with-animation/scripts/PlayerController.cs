using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    
    [Header("Ground Check")]
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    
    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isSprinting;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
        // Lock cursor for first-person view
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimations();
    }
    
    private void HandleMovement()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        // Get input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        // Calculate movement direction
        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        
        // Handle sprinting
        isSprinting = Input.GetKey(KeyCode.LeftShift) && vertical > 0;
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
        
        // Apply movement
        controller.Move(direction.normalized * currentSpeed * Time.deltaTime);
        
        // Handle camera rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up * mouseX);
    }
    
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }
        
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
    private void UpdateAnimations()
    {
        if (animator != null)
        {
            // Calculate movement magnitude
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            float movementMagnitude = movement.magnitude;
            
            // Update animator parameters
            animator.SetBool("IsMoving", movementMagnitude > 0.1f);
            animator.SetBool("IsSprinting", isSprinting);
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("MovementSpeed", movementMagnitude);
        }
    }
}