using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    private PlayerHealth health;
    private PlayerMovement movement;
    private float _lockUntil;

    // animation states
    public static readonly string Idle = "Idle";
    public static readonly string Attack = "AttackHor";
    public static readonly string WalkBackward = "WalkBackward";
    public static readonly string CrouchForward = "CrouchForward";
    public static readonly string CrouchBackward = "CrouchBackward";
    public static readonly string Running = "Running";
    public static readonly string Jump = "Jump";
    public static readonly string Death = "Death";
    public static string curState;
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
        string state = GetState();
        if (curState != state)
        {
            anim.CrossFade(state, 0.15f, 0);
            curState = state;
        }
    }

    private bool isLocking = false;
    private string GetState()
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
    string LockState(string s)
    {
        isLocking = true;
        return s;
    }
} 