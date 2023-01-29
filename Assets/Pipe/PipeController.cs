using UnityEngine;

public class PipeController : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private double deadzone = -1.7;

    private bool scored = false;

    private RobotController player;


    // Start is called before the first frame update
    void Start()
    {
        player = RobotController.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MaybeScore(collision);
    }

    void FixedUpdate()
    {
        MaybeDie();
    }

    private void MaybeDie()
    {
        transform.position = transform.position + Time.deltaTime * speed * Vector3.left;
        if (transform.position.x <= deadzone)
        {
            Destroy(gameObject);
        }
    }

    private void MaybeScore(Collider2D collision)
    {
        bool isPlayer = collision.gameObject.Equals(player.gameObject);
        if (isPlayer && !scored)
        {
            scored = true;
            ScoreManager.instance.Add();
        }
    }
}
