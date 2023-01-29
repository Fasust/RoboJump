using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        RobotController.instance.onStateChange += EnableGameOverScreenOnState;

        gameOverScreen.SetActive(false);
    }

    void OnReset(InputValue inputValue)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void EnableGameOverScreenOnState(RobotState state)
    {
        gameOverScreen.SetActive(state == RobotState.Dead);
    }
}
