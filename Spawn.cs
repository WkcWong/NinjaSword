using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject SpawnObj;
    public float spawnTime = 10f;
    public Transform[] spawnPoints;


    void Start()
    {
        InvokeRepeating("create", spawnTime, spawnTime);
    }


    void create()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(SpawnObj, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
