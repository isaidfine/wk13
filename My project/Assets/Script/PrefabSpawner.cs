using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Vector2 spawnPosition = new Vector2(9f, -0.23f);
    public float spawnInterval = 1f; // 每隔1秒生成一个Prefab
    public float moveSpeed = 12f; // Prefab移动速度

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnAndMovePrefab();
            timer = spawnInterval;
        }
    }

    void SpawnAndMovePrefab()
    {
        GameObject newPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        newPrefab.GetComponent<PrefabMover>().SetMoveSpeed(moveSpeed);
    }
}