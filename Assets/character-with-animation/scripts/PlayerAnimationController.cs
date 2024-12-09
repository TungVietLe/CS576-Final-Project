using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    private PlayerHealth health;
    private PlayerMovement movement;
    private float _lockUntil;

    // animation states
    private int curState;
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Attack = Animator.StringToHash("AttackHor");
    private static readonly int WalkBackward = Animator.StringToHash("WalkBackward");
    private static readonly int CrouchForward = Animator.StringToHash("CrouchForward");
    private static readonly int CrouchBackward = Animator.StringToHash("CrouchBackward");
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Death = Animator.StringToHash("Death");
    void Start() {
        anim = GetComponent<Animator>();
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        foreach (var c in anim.GetCurrentAnimatorClipInfo(0)) {
            print(c.clip.name);
        }
    }
    // Update is called once per frame
    void Update()
    {
        int state = GetState();
        if (curState != state)
        {
            anim.CrossFade(state, 0.15f, 0);
            curState = state;
        }
    }

    private bool isLocking = false;
    private int GetState()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) {
            isLocking = false;
        }
        if (isLocking) return curState;
        if (health.isDead) return Death;
        if (movement.IsAttachTriggered) return LockState(Attack);
        if (movement.IsMoving) return Running;
        return Idle;
    }

    // states that triggered and you want to play the animation all the way till the end
    int LockState(int s)
    {
        isLocking = true;
        return s;
    }
} 