using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Bomb,
        DoubleScore,
        SpeedUp,
        BonusPoints
    }

    public ItemType itemType;
    public float effectDuration = 3f;
    private  float lifeTime = 2f;          // زمان قبل از شروع چشمک زدن
    private  float blinkDuration = 1f;      // مدت زمان چشمک زدن
    private  float blinkInterval = 0.3f;    // سرعت چشمک زدن

    private SpriteRenderer sr;
    private bool isPickedUp = false;
    void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            StartCoroutine(LifetimeRoutine());
        }

        IEnumerator LifetimeRoutine()
        {
            // صبر کن تا زمان زندگی تموم بشه
            yield return new WaitForSeconds(lifeTime);

            // شروع چشمک زدن
            float elapsed = 0f;
            bool visible = true;

            while (elapsed < blinkDuration)
            {
                visible = !visible;
                sr.color = new Color(1f, 1f, 1f, visible ? 1f : 0.3f);
                yield return new WaitForSeconds(blinkInterval);
                elapsed += blinkInterval;
            }

            // حذف آیتم
            Destroy(gameObject);
        }
    public void StartLifetimeCountdown(float lifetime)
    {
        StartCoroutine(FadeAndDestroy(lifetime));
    }

    private System.Collections.IEnumerator FadeAndDestroy(float duration)
    {
        float blinkStartTime = duration - 1.5f;
        float elapsed = 0f;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;

        while (elapsed < duration)
        {
            if (elapsed >= blinkStartTime)
            {
                sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.PingPong(Time.time * 1, 1f));
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (!isPickedUp)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;
        isPickedUp = true;

        switch (itemType)
        {
            case ItemType.Bomb:
                GameManager.instance?.GameOver();
                break;

            case ItemType.DoubleScore:
                GameManager.instance?.ActivateSpecialItem("DoubleScore", effectDuration);
                break;

            case ItemType.SpeedUp:
                GameManager.instance?.ActivateSpecialItem("SpeedUp", effectDuration);
                break;

            case ItemType.BonusPoints:
                GameManager.instance?.AddScore(10);
                break;
        }

        Destroy(gameObject);
    }

    
}
