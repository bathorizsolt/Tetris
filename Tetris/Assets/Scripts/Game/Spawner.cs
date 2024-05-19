using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public GameObject[] BlockPrefabs;
    private bool gameOver = false;
    private GameManager GameManager;

    void Start()
    {
        GetRandom();
    }

    public void GetRandom()
    {
        if (!gameOver)
        {
            Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)], transform.position, Quaternion.identity);
        }

    }

    public void StopSpawning()
    {
        gameOver = true;
    }
}