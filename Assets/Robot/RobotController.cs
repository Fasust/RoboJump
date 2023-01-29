using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum RobotState
{
    Alive,
    Dead,
}

public class RobotController : MonoBehaviour
{
    public static RobotController instance { get; private set; }
    public RobotState state { get; private set; } = RobotState.Alive;
    public event Action<RobotState> onStateChange;

    [Header("Movment")]
    [SerializeField] private float jumpFoce = 80f;
    [SerializeField] private float killKnockBackFoce = 80f;
    [SerializeField] private float fallFoce = 10f;
    [SerializeField] private float fallFelvocityCutOff = 10f;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;


    private new Rigidbody2D rigidbody;
    private Animator animator;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void Kill(Collision2D collision)
    {
        Debug.Log("Robot Killed by: " + collision.otherCollider.gameObject.name);
        state = RobotState.Dead;
        onStateChange?.Invoke(state);
        audioSource.PlayOneShot(deathSound);
        rigidbody.AddForce(collision.contacts[0].normal * -1 * killKnockBackFoce);
        ScoreManager.instance.Freeze();
    }

    void OnJumpStarted(InputValue inputValue)
    {
        if (state == RobotState.Dead) return;

        audioSource.PlayOneShot(jumpSound);
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(Vector2.up * jumpFoce);
        animator.SetTrigger("Jump");

    }

    void OnJumpStopped(InputValue inputValue)
    {
        if (state == RobotState.Dead) return;

        if (rigidbody.velocity.y > fallFelvocityCutOff)
        {
            rigidbody.AddForce(Vector2.down * fallFoce);
        }
    }
}