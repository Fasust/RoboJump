using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }
    public int score { get; private set; }

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioSource audioSource;

    private bool frozen;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) { instance = this; }
        audioSource = GetComponent<AudioSource>();
    }

    public void Add()
    {
        if (frozen) return;

        score++;
        text.SetText(score.ToString());
        audioSource.Play();
    }

    public void Clear()
    {
        score = 0;
        frozen = false;
        text.SetText(score.ToString());
    }

    public void Freeze()
    {
        frozen = true;
    }
}
