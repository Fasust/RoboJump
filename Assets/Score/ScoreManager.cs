using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }
    public int score { get; private set; }
    public int deadScore { get; private set; }

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI deadText;
    [SerializeField] private AudioSource audioSource;

    private bool dead;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) { instance = this; }
        audioSource = GetComponent<AudioSource>();
        RobotController.instance.onStateChange += AssertDeath;
    }

    private void AssertDeath(RobotState state)
    {
        if (state == RobotState.Dead)
        {
            dead = true;
        }
        else
        {
            dead = false;
        }
    }

    public void Add()
    {
        if (dead)
        {
            ScoreDeadPoint();
        }
        else
        {
            ScorePoint();

        }
        audioSource.Play();
    }

    private void ScorePoint()
    {
        score++;
        text.SetText(score.ToString());
    }

    private void ScoreDeadPoint()
    {
        deadScore++;
        deadText.SetText("+" + deadScore.ToString());
    }
}
