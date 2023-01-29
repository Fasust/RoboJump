using UnityEngine;

public class PipeSpawner : MonoBehaviour
{

    [SerializeField] private GameObject pipe;
    [SerializeField] private float spawnRate = 2;
    [SerializeField] private float positionVarriance = 1;

    private float timer = 0;

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        if (timer > spawnRate)
        {
            Spawn();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void Spawn()
    {
        float high = transform.position.y + positionVarriance;
        float low = transform.position.y - positionVarriance;

        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(low, high), transform.position.z), transform.rotation);
    }
}
