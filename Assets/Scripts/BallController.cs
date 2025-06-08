using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;

    public float clickForce = 6f;
    public float dragForceMultiplier = 4f;

    private Vector2 startDragPos;
    private bool isDragging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        isDragging = true;
        startDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            Vector2 endDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = startDragPos - endDragPos;

            if (dragVector.magnitude < 0.2f)
            {
                // کلیک ساده → ضربه معمولی به بالا
                KickBall(Vector2.up * clickForce);
            }
            else if (dragVector.y > 0f)
            {
                // درگ رو به بالا → پرتاب با شدت متناسب
                KickBall(dragVector * dragForceMultiplier);
            }

            isDragging = false;
        }
    }

    void KickBall(Vector2 force)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);

        if (audioSource != null)
            audioSource.Play();

        if (GameManager.instance != null)
            GameManager.instance.AddScore(1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            if (GameManager.instance != null)
                GameManager.instance.GameOver();
        }
    }
}
