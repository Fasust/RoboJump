using UnityEngine;

public class KillOnHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RobotController.instance.Kill(collision);
    }


}
