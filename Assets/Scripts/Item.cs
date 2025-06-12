using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isGood = true;
    public float lifetime = 5f; // 🕓 زمان باقی ماندن
    public float blinkDuration = 1f; // 🔁 زمان کل چشمک‌زدن قبل از حذف
    public float blinkInterval = 0.2f; // ✨ فاصله بین هر چشمک

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Invoke(nameof(StartBlinking), lifetime - blinkDuration); // شروع چشمک‌زدن قبل از حذف
        Destroy(gameObject, lifetime); // حذف نهایی
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (isGood)
            {
                GameManager.instance.AddScore(5);
            }
            else
            {
                GameManager.instance.GameOver();
            }

            Destroy(gameObject);
        }
    }

    void StartBlinking()
    {
        StartCoroutine(BlinkCoroutine());
    }

    System.Collections.IEnumerator BlinkCoroutine()
    {
        float timer = 0f;
        while (timer < blinkDuration)
        {
            sr.enabled = !sr.enabled; // روشن و خاموش
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }
        sr.enabled = true; // مطمئن شو آخرش روشن بمونه
    }
}
