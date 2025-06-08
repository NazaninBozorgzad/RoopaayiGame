using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;

    public float force = 6f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        KickBall();
    }

    void KickBall()
    {
        // صفر کردن سرعت قبلی
        rb.velocity = Vector2.zero;

        // اعمال نیروی بالا
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        // پخش صدای ضربه
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // افزودن امتیاز
        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
        }
    }
}
