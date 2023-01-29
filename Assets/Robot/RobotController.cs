using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    [Header("Debugging")]
    [SerializeField] private bool undying = false;

    [Header("Movment")]
    [SerializeField] private float jumpFoce = 80f;
    [SerializeField] private float killKnockBackFoce = 80f;
    [SerializeField] private float jumpCancleFactor = 2f;

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

        if (undying) return;
        animator.SetBool("Dead", true);
        state = RobotState.Dead;
        onStateChange?.Invoke(state);
        audioSource.PlayOneShot(deathSound);
        rigidbody.AddForce(collision.contacts[0].normal * -1 * killKnockBackFoce);
    }

    void OnJump(InputValue inputValue)
    {
        if (state == RobotState.Dead) return;

        if (inputValue.isPressed)
        {
            audioSource.PlayOneShot(jumpSound);
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(Vector2.up * jumpFoce);
            animator.SetTrigger("Jump");
        }
        else
        {

            bool movingUp = rigidbody.velocity.y > 0;
            if (movingUp)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / jumpCancleFactor);

            }
        }

    }

    /// <summary>
    /// Reloads Screen if Robot is not Alive.
    /// 
    /// I tired to handle this on a different GameObject, but the new input system is struggeling if 2 objects contain the 
    /// same player controlls component
    /// </summary>
    void OnReset()
    {
        if (state == RobotState.Alive) return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}