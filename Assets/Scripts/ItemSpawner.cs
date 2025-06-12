using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject goodItemPrefab;
    public GameObject badItemPrefab;

    public float spawnInterval = 3f;
    public Vector2 spawnRangeX = new Vector2(-3f, 3f);
    public float spawnY = 4f;

    void Start()
    {
        InvokeRepeating("SpawnItem", 2f, spawnInterval);
    }

    void SpawnItem()
    {
        Vector3 spawnPos = new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), spawnY, 0);
        GameObject prefab = (Random.value < 0.7f) ? goodItemPrefab : badItemPrefab;
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
