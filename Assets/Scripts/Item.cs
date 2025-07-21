using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Good,
        Bad,
        Special
    };

    public ItemType itemType;

    public float lifetime = 5f; // 🕓 زمان باقی ماندن
    public float blinkDuration = 1f; // 🔁 زمان کل چشمک‌زدن قبل از حذف
    public float blinkInterval = 0.2f; // ✨ فاصله بین هر چشمک
    private bool routineHasEnded;
    public AudioClip specialItemUse;
    public AudioClip bombExplosion;
    private SpriteRenderer sr;
    private AudioSource audioSource;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        Invoke(nameof(StartBlinking), lifetime - blinkDuration); // شروع چشمک‌زدن قبل از حذف
        Destroy(gameObject, lifetime); // حذف نهایی
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (itemType.Equals(ItemType.Good))
            {
                GameManager.instance.AddScore(5);
                Destroy(gameObject);
            }
            else if (itemType.Equals(ItemType.Bad))
            {
                GameManager.instance.audioSource.PlayOneShot(bombExplosion, 1);
                GameManager.instance.GameOver();
                Destroy(gameObject);
            }
            else if (itemType.Equals(ItemType.Special))
            {
                audioSource.PlayOneShot(specialItemUse, 1);
                StartCoroutine(RandomizeSpecialActivity());
                sr.color = new Color(1, 1, 1, 0.1f);
            }
        }
    }

    void Update()
    {
        if (itemType.Equals(ItemType.Special))
        {
            if (routineHasEnded == true)
            {
                Destroy(gameObject);
            }
        }
    }

    void StartBlinking()
    {
        StartCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine()
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

    IEnumerator RandomizeSpecialActivity()
    {
        BallController ball = GameObject.Find("Ball").GetComponent<BallController>();
        int function = Random.Range(0, 1);
        if (function.Equals(0))
        {
            routineHasEnded = false;
            ball.SetFastSpeed();
            print("Fast ball started!");
            yield return new WaitForSeconds(3);
            ball.ResetSpeed();
            print("Fast ball ended");
            routineHasEnded = true;
        }
        else if (function.Equals(1))
        {
            routineHasEnded = false;
            ball.SetSlowSpeed();
            print("Slow ball started!");
            yield return new WaitForSeconds(3);
            ball.ResetSpeed();
            print("Slow ball ended");
            routineHasEnded = true;
        }
    }
}
