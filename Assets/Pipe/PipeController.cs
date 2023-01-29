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

    void FixedUpdate()
    {
        MaybeScore();
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

    private void MaybeScore()
    {
        if (player.transform.position.x > transform.position.x && !scored)
        {
            scored = true;
            ScoreManager.instance.Add();
        }
    }
}
