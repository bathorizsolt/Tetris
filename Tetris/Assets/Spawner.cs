using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField] public GameObject[] tetriminoPrefabs;

    void Start()
    {
        GetRandom();
    }

    public void GetRandom()
    {
        Instantiate(tetriminoPrefabs[Random.Range(0, tetriminoPrefabs.Length)], transform.position, Quaternion.identity);
    }
}