using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // آرایه آیتم‌ها برای اسپاون

    public float spawnInterval = 3f; // فاصله زمانی بین اسپاون‌ها

    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    void Start()
    {
        // گرفتن ابعاد دوربین اصلی (Orthographic)
        Camera cam = Camera.main;
        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        // تنظیم محدوده اسپاون در نصف بالایی صفحه
        spawnAreaMin = new Vector2(-width / 2f, 0f);
        spawnAreaMax = new Vector2(width / 2f, height / 2f);

        // شروع اسپاون دوره‌ای آیتم‌ها
        InvokeRepeating(nameof(SpawnRandomItem), 2f, spawnInterval);
    }

    void SpawnRandomItem()
    {
        if (itemPrefabs.Length == 0)
            return;

        // انتخاب تصادفی آیتم
        int randomIndex = Random.Range(0, itemPrefabs.Length);

        // موقعیت تصادفی داخل محدوده نصف بالا
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);

        Vector2 spawnPos = new Vector2(randomX, randomY);

        Instantiate(itemPrefabs[randomIndex], spawnPos, Quaternion.identity);
    }
}
